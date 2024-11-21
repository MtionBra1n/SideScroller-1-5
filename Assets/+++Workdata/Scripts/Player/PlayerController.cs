using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// irgendwas
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    public LayerMask groundLayer;
    public Vector2 overlapBoxPos;
    public Vector2 overlapBoxSize;
    #region Private Variables

    public GameInput inputActions;
    
    private InputAction moveAction;
    
    private Vector2 moveInput;

    private Rigidbody2D rb;

    private Animator anim;

    public bool isGrounded;
    #endregion
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        inputActions = new GameInput();
        moveAction = inputActions.Player.Move;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        moveAction.performed += Move;
        moveAction.canceled += Move;
    }
    private void FixedUpdate()
    {
        GroundCheck();
        
        rb.velocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);
        anim.SetFloat("MovementValue", Mathf.Abs(rb.velocity.x));
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapBox(overlapBoxPos + (Vector2)transform.position, overlapBoxSize, 0, groundLayer);
    }

    private void OnDisable()
    {
        //
        inputActions.Disable();
        //
        moveAction.performed -= Move;
        
        //
        moveAction.canceled -= Move;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        if (moveInput.x > 0)
        {
            //right
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            print("No Movement");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        /*
        if (isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        */
        Gizmos.DrawWireCube(overlapBoxPos + (Vector2)transform.position, overlapBoxSize);
        
    }
}
