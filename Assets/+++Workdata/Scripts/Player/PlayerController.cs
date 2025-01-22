using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static readonly int Hash_MovementValue = Animator.StringToHash("MovementValue");
    public static readonly int Hash_GroundValue = Animator.StringToHash("isGrounded");
    public static readonly int Hash_JumpTrigger = Animator.StringToHash("isJumping");
    public static readonly int Hash_ActionTriggerValue = Animator.StringToHash("ActionTrigger");
    public static readonly int Hash_ActionIdValue = Animator.StringToHash("ActionId");
    
    #region Inspector

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    
    [SerializeField] private float jumpPower= 5f;
    [SerializeField] private float jumpPowerLimit = 8f;
    
    [SerializeField] private float rollPower= 5f;
    [SerializeField] private float rollPowerLimit = 8f;
    
    [Header("GroundCheck")] 
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 boxxOffset;
    [SerializeField] private LayerMask groundLayer;
    
    #endregion
    
    #region Private Variables

    public Interactable selectedInteractable;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    
    public float movementSpeed;
    
    private bool isFacingRight = true;
    private bool jumpFix;
    
    public bool isGrounded;
    public bool isJumping;
    public bool isRolling;
    public bool isAttacking;
    public bool canJump;
    public bool canRoll;

    private int jumpCount;
    private int rollCount;
    
    #region Input Variables
    
    public GameInput inputActions;
    
    #region InputActions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction rollAction;
    private InputAction interactAction;
    private InputAction runAction;
    #endregion
    
    #endregion

    #endregion
    
    #region Unity Event Functions

    private void Awake()
    {
        canJump = true;
        canRoll = true;
        
        inputActions = new GameInput();
        moveAction = inputActions.Player.Move;
        jumpAction = inputActions.Player.Jump;
        rollAction = inputActions.Player.Roll;
        runAction = inputActions.Player.Run;
        interactAction = inputActions.Player.Interact;
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        movementSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        EnableInput();

        moveAction.performed += Move;
        moveAction.canceled += Move;

        jumpAction.performed += Jump;

        rollAction.performed += Roll;

        runAction.performed += Run;
        runAction.canceled += Run;

        interactAction.performed += Interact;
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!isRolling)
        {
            rb.velocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);

            if (rb.velocity.y > jumpPowerLimit)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPowerLimit);
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > rollPowerLimit)
        {
            rb.velocity = new Vector2(isFacingRight ? rollPowerLimit : -rollPowerLimit, rb.velocity.y);
        }
        

        UpdateAnimator();
    }

    private void OnDisable()
    {
        DisableInput();
        
        moveAction.performed -= Move;
        moveAction.canceled -= Move;
        
        jumpAction.performed -= Jump;
        
        rollAction.performed -= Roll;
        
        runAction.performed -= Run;
        runAction.canceled -= Run;
        
        interactAction.performed -= Interact;

    }
    #endregion
    
    #region Input Methods

    public void EnableInput()
    {
        inputActions.Enable();
    }

    public void DisableInput()
    {
        inputActions.Disable();
    }
    
    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        if (moveInput.x < 0) //left
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(moveInput.x > 0) //right
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (jumpCount < 2 && canJump)
        {
            jumpCount++;
            
            canJump = false;
            isJumping = true;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            
            anim.SetTrigger(Hash_JumpTrigger);

            print(jumpCount);
            
            if (jumpCount == 2)
            {
                Invoke("ReleaseJumpTrigger", .1f);
            }
        }
    }
    
    void ReleaseJumpTrigger()
    {
        anim.ResetTrigger(Hash_JumpTrigger);
    }
    
    private void Roll(InputAction.CallbackContext ctx)
    {
        if (rollCount < 2 && canRoll)
        {
            rollCount++;
            
            canRoll = false;
            isRolling = true;
            rb.AddForce((isFacingRight ? Vector2.right : Vector2.left) * rollPower, ForceMode2D.Impulse);
            
            AnimAction(1);
        }
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        movementSpeed = ctx.performed ? runSpeed : walkSpeed;
    }

    #endregion

    #region Physics

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrySelectInteractable(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TryDeselectInteractable(other);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(boxxOffset + (Vector2)transform.position, boxSize, 0, groundLayer);

        if (isGrounded && !canJump && !jumpFix)
        {
            jumpFix = true;
            Invoke("AnimEvent_EndJump", .1f);
        }

        if (isGrounded && rollCount > 0)
        {
            rollCount = 0;
        }
    }
    
    #endregion

    #region Interaction

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    private void TrySelectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null) return;

        if (selectedInteractable != null)
        {
            selectedInteractable.Deselect();
        }

        selectedInteractable = interactable;
        selectedInteractable.Select();
    }

    private void TryDeselectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null) return;

        if (interactable == selectedInteractable)
        {
            selectedInteractable.Deselect();
            selectedInteractable = null;
        }
    }

    #endregion
    
    #region Animations Methods

    void UpdateAnimator()
    {
        anim.SetFloat(Hash_MovementValue, Mathf.Abs(rb.velocity.x));
        anim.SetBool(Hash_GroundValue, isGrounded);
    }

    void AnimAction(int actionId)
    {
        anim.SetTrigger(Hash_ActionTriggerValue);
        anim.SetInteger(Hash_ActionIdValue, actionId);
    }
    
    public void AnimEvent_EndJump()
    {
        isJumping = false;
        canJump = true;
        jumpFix = false;

        ReleaseJumpTrigger();
        
        jumpCount = 0;

        if (isRolling)
        {
            AninmEvent_EndRoll();
        }
    }
    
    public void AninmEvent_EndRoll()
    {
        isRolling = false;
        canRoll = true;
        
        if(!isGrounded)
            anim.Play("player_inAir");
    }
    
    #endregion
    
    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        
        Gizmos.DrawWireCube(boxxOffset + (Vector2)transform.position, boxSize);
    }

    #endregion
}
