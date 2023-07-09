using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChapScript : Character
{
    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    public Vector2 move;
    public float sign;

    public bool printDebug;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        CharacterState dummyState = new CharacterState();
        sign = 1;
        ChangeBehaviorToAutoMoving(dummyState);
        getMinMaxBoundaries();

        printDebug = true;
    }

    public override void ChangeBehaviorToControllable(CharacterState otherData)
    {
        rbody.bodyType = RigidbodyType2D.Kinematic;
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
        rbody.bodyType = RigidbodyType2D.Kinematic;
        setDefaultVelocity(sign);

        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW AUTO-MOVING!!!");
        }
    }

    public override void FixedUpdateAutoMoving()
    {
        if (transform.position.x <= minBoundary || transform.position.x >= maxBoundary)
        {
            rbody.velocity *= -1;
            sign *= -1;
        }
    }

    public override void ChangeBehaviorToStill(CharacterState otherData)
    {
        rbody.bodyType = RigidbodyType2D.Static;
        currentState.vel = rbody.velocity;

        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW STILL!!!");
        }
    }

    public override void FixedUpdateStill()
    {

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
    //     Debug.Log(gameObject.name);
    //     Debug.Log("I AM FROZEN!!!");
    // }

    // public override void unfreezeCharacter()
    // {
    //     Debug.Log(gameObject.name);
    //     Debug.Log("I AM LIBREEEEEEEEEE!!!");
    // }
}
