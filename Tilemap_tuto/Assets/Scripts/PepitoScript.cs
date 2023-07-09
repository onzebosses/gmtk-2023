using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PepitoScript : Character
{
    private bool isJumping;
    private bool isGrounded;
    private bool isAirborneAndStill;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    public Vector2 move;

    public bool printDebug;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rbody.bodyType = RigidbodyType2D.Dynamic;
        behavior = Behavior.Controllable; 
        printDebug = true;
    }

    public override void ChangeBehaviorToControllable(CharacterState otherData)
    {
        rbody.bodyType = RigidbodyType2D.Dynamic;
        rbody.velocity = Vector2.zero;

        if (printDebug) {
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW CONTROLLABLE!!!");
        }
    }

    public override void FixedUpdateControllable()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move = movement.action.ReadValue<Vector2>();

        float horizontalMovement = move.x * moveSpeed * Time.deltaTime;

        if (jump.action.IsPressed() && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement);

        Flip(rbody.velocity.x);

        float characterVelocity = Mathf.Abs(rbody.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rbody.velocity.y);
        rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rbody.AddForce(new Vector2(0f, jumpForce));
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

    public override void ChangeBehaviorToAutoMoving(CharacterState otherData)
    {
        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW AUTO-MOVING!!!");
        }
    }

    public override void FixedUpdateAutoMoving()
    {

    }

    public override void ChangeBehaviorToStill(CharacterState otherData)
    {
        rbody.velocity = Vector2.zero;
        isAirborneAndStill = !isGrounded;
        if (isGrounded)
            rbody.bodyType = RigidbodyType2D.Static;

        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW STILL!!!");
        }
    }

    public override void FixedUpdateStill()
    {
        if (isGrounded && isAirborneAndStill)
        {
            isAirborneAndStill = false;
            rbody.bodyType = RigidbodyType2D.Static;
        }
    }

    public override void ChangeBehaviorToBounce(CharacterState otherData)
    {
        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW STILL!!!");
        }
    }

    public override void FixedUpdateBounce()
    {

    }

    // public override void freezeCharacter()
    // {
    //     rbody.gravityScale = 0;
    //
    //     if (printDebug){
    //         Debug.Log(gameObject.name);
    //         Debug.Log("I AM FROZEN!!!");
    //     }
    // }

    // public override void unfreezeCharacter()
    // {
    //     Debug.Log(gameObject.name);
    //     Debug.Log("I AM LIBREEEEEEEEEE!!!");
    // }
}
