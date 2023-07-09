using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    private bool hasStartJump;
    private bool hasLanded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    public Vector2 move;


    [SerializeField]
    private InputActionReference movement, jump;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        hasStartJump = false;
        hasLanded = true;
    }

    void FixedUpdate()
       {
        if (hasStartJump)
        {
            hasStartJump = false;
        }
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move = movement.action.ReadValue<Vector2>();

        float horizontalMovement = move.x * moveSpeed * Time.deltaTime;
           if (jump.action.IsPressed() && isGrounded)

           {
               isJumping = true;
               hasStartJump = true;
               hasLanded = false;
               animator.SetBool("StartJump", true);
               animator.SetBool("JustLanded", false);
           }

        MovePlayer(horizontalMovement);

        if (!hasStartJump && !hasLanded && isGrounded)
           {

            Debug.Log("I Landed");
            hasLanded = true;
               animator.SetBool("StartJump", false);
               animator.SetBool("JustLanded", true);
           }


        Flip(rb.velocity.x);

           float characterVelocity = Mathf.Abs(rb.velocity.x);
           animator.SetFloat("Speed", characterVelocity);
     
    }

    void MovePlayer(float _horizontalMovement)
       {
          Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
           rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

          if(isJumping == true) 
          {
              rb.AddForce(new Vector2(0f, jumpForce));
              isJumping = false;
            
          }
    }
    void Flip(float _velocity)
    {
           if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }

    }


} 

// using UnityEngine;
// using UnityEngine.InputSystem;
//
// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     public float jumpForce;
//
//     private bool isJumping;
//     private bool isGrounded;
//
//     public Transform groundCheckLeft;
//     public Transform groundCheckRight;
//
//     public Rigidbody2D rb;
//     public Animator animator;
//     public SpriteRenderer spriteRenderer;
//
//     private Vector3 velocity = Vector3.zero;
//     public Vector2 move;
//
//
//     [SerializeField]
//     private InputActionReference movement, jump;
//
//
//     void FixedUpdate()
//        {
//         
//         isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
//         move = movement.action.ReadValue<Vector2>();
//
//         float horizontalMovement = move.x * moveSpeed * Time.deltaTime;
//
//            if (jump.action.IsPressed() && isGrounded)
//            {
//                isJumping = true;
//            }
//
//            MovePlayer(horizontalMovement);
//
//            Flip(rb.velocity.x);
//
//            float characterVelocity = Mathf.Abs(rb.velocity.x);
//            animator.SetFloat("Speed", characterVelocity);
//
//     }
//
//     void MovePlayer(float _horizontalMovement)
//        {
//           Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
//            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
//
//           if(isJumping == true)
//           {
//               rb.AddForce(new Vector2(0f, jumpForce));
//               isJumping = false;
//           }
//        }
//     void Flip(float _velocity)
//     {
//            if (_velocity > 0.1f)
//         {
//             spriteRenderer.flipX = false;
//         }else if(_velocity < -0.1f)
//         {
//             spriteRenderer.flipX = true;
//         }
//
//     }
//
//
// } 

// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UIElements;
//
// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     public float jumpForce;
//
//     public bool isMoving;
//     public bool isGrounded;
//     private bool isJumping;
//
//     public Transform groundCheckLeft;
//     public Transform groundCheckRight;
//
//     public Rigidbody2D rb;
//     public Animator animator;
//     public SpriteRenderer spriteRenderer;
//
//     private Vector3 velocity = Vector3.zero;
//     public Vector2 move;
//
//
//     [SerializeField]
//     private InputActionReference movement, jump;
//
//     private void Start()
//     {
//         isMoving = true;
//         // Cursor.lockState = CursorLockMode.Confined;
//     }
//
//     void FixedUpdate()
//        {
//         
//         isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
//         move = movement.action.ReadValue<Vector2>();
//
//         float horizontalMovement = move.x * moveSpeed * Time.deltaTime;
//
//            if (jump.action.IsPressed() && isGrounded)
//            {
//                isJumping = true;
//            }
//
//            MovePlayer(horizontalMovement);
//
//            Flip(rb.velocity.x);
//
//            float characterVelocity = Mathf.Abs(rb.velocity.x);
//            animator.SetFloat("Speed", characterVelocity);
//
//     }
//
//     void MovePlayer(float _horizontalMovement)
//        {
//           Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
//            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
//
//           if(isJumping == true)
//           {
//               rb.AddForce(new Vector2(0f, jumpForce));
//               isJumping = false;
//           }
//        }
//     void Flip(float _velocity)
//     {
//            if (_velocity > 0.1f)
//         {
//             spriteRenderer.flipX = false;
//         }else if(_velocity < -0.1f)
//         {
//             spriteRenderer.flipX = true;
//         }
//
//     }
//
//
// } 


// using UnityEngine;
// using UnityEngine.InputSystem;
//
// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     public float jumpForce;
//
//     public bool isMoving;
//     public bool isGrounded;
//     private bool isJumping;
//     private bool hasStartJump;
//     private bool hasLanded;
//
//     public Transform groundCheckLeft;
//     public Transform groundCheckRight;
//
//     public Rigidbody2D rb;
//     public Animator animator;
//     public SpriteRenderer spriteRenderer;
//
//     private Vector3 velocity = Vector3.zero;
//     public Vector2 move;
//
//
//     [SerializeField]
//     private InputActionReference movement, jump;
//
//     private void Start()
//     {
//         Cursor.lockState = CursorLockMode.Confined;
//         hasStartJump = false;
//         hasLanded = true;
//     }
//
//     void FixedUpdate()
//        {
//         if (hasStartJump)
//         {
//             hasStartJump = false;
//         }
//         isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
//         move = movement.action.ReadValue<Vector2>();
//
//         float horizontalMovement = move.x * moveSpeed * Time.deltaTime;
//            if (jump.action.IsPressed() && isGrounded)
//
//            {
//                isJumping = true;
//                hasStartJump = true;
//                hasLanded = false;
//                animator.SetBool("StartJump", true);
//                animator.SetBool("JustLanded", false);
//            }
//
//         MovePlayer(horizontalMovement);
//
//         if (!hasStartJump && !hasLanded && isGrounded)
//            {
//
//             Debug.Log("I Landed");
//             hasLanded = true;
//                animator.SetBool("StartJump", false);
//                animator.SetBool("JustLanded", true);
//            }
//
//
//         Flip(rb.velocity.x);
//
//            float characterVelocity = Mathf.Abs(rb.velocity.x);
//            animator.SetFloat("Speed", characterVelocity);
//      
//     }
//
//     void MovePlayer(float _horizontalMovement)
//        {
//           Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
//            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
//
//           if(isJumping == true) 
//           {
//               rb.AddForce(new Vector2(0f, jumpForce));
//               isJumping = false;
//             
//           }
//     }
//     void Flip(float _velocity)
//     {
//            if (_velocity > 0.1f)
//         {
//             spriteRenderer.flipX = false;
//         }else if(_velocity < -0.1f)
//         {
//             spriteRenderer.flipX = true;
//         }
//
//     }
//
//
// } 
//
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UIElements;
//
// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     public float jumpForce;
//
//     public bool isMoving;
//     public bool isGrounded;
//     private bool isJumping;
//     private bool hasStartJump;
//     private bool hasLanded;
//
//     public Transform groundCheckLeft;
//     public Transform groundCheckRight;
//
//     public Rigidbody2D rb;
//     public Animator animator;
//     public SpriteRenderer spriteRenderer;
//
//     private Vector3 velocity = Vector3.zero;
//     public Vector2 move;
//
//
//     [SerializeField]
//     private InputActionReference movement, jump;
//
//     private void Start()
//     {
//         UnityEngine.Cursor.lockState = CursorLockMode.Confined;
//         hasStartJump = false;
//         hasLanded = true;
//     }
//
//     void FixedUpdate()
//        {
//         if (hasStartJump)
//         {
//             hasStartJump = false;
//         }
//         isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
//         move = movement.action.ReadValue<Vector2>();
//
//         float horizontalMovement = move.x * moveSpeed * Time.deltaTime;
//            if (jump.action.IsPressed() && isGrounded)
//
//            {
//                isJumping = true;
//                hasStartJump = true;
//                hasLanded = false;
//                animator.SetBool("StartJump", true);
//                animator.SetBool("JustLanded", false);
//            }
//
//         MovePlayer(horizontalMovement);
//
//         if (!hasStartJump && !hasLanded && isGrounded)
//            {
//
//             Debug.Log("I Landed");
//             hasLanded = true;
//                animator.SetBool("StartJump", false);
//                animator.SetBool("JustLanded", true);
//            }
//
//
//         Flip(rb.velocity.x);
//
//            float characterVelocity = Mathf.Abs(rb.velocity.x);
//            animator.SetFloat("Speed", characterVelocity);
//      
//     }
//
//     void MovePlayer(float _horizontalMovement)
//        {
//           Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
//            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
//
//           if(isJumping == true) 
//           {
//               rb.AddForce(new Vector2(0f, jumpForce));
//               isJumping = false;
//             
//           }
//     }
//     void Flip(float _velocity)
//     {
//            if (_velocity > 0.1f)
//         {
//             spriteRenderer.flipX = false;
//         }else if(_velocity < -0.1f)
//         {
//             spriteRenderer.flipX = true;
//         }
//
//     }
//
//
// } 
//
