using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// irgendwas
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    
    #region Private Variables

    public GameInput inputActions;
    
    private InputAction moveAction;
    
    private Vector2 moveInput;

    private Rigidbody2D rb;

    private Animator anim;
    #endregion

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        
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
        rb.velocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);
        anim.SetFloat("MovementValue", MathF.Abs(rb.velocity.x));
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
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
    

}
