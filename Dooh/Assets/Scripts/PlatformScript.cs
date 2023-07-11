using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformScript : Character
{
    public float impulsionStrength;

    private bool isJumping;
    private bool isGrounded;
    private bool impulsionBeingApplied;

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
        printDebug = true;
    }

    public override void ChangeBehaviorToControllable(CharacterState otherData)
    {
        animator.SetBool("IsControlled", true);
        animator.SetBool("IsAutoMoving", false);
        animator.SetBool("IsStill", false);
        rbody.bodyType = RigidbodyType2D.Kinematic;
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

        float horizontalMovement = move.x * defaultControllableVelocity * Time.deltaTime;
        if (defaultDirection == Direction.Vertical)
            horizontalMovement *= -getAlphaRotation();

        // TODO: impulse
        // if (jump.action.IsPressed() && false)
        // {
        //     impulsionBeingApplied = true;
        //     rbody.bodyType = RigidbodyType2D.Dynamic;
        //     float sign = -1;
        //     // TODO: What if velocity is 0?
        //     if (velocity.x > 0)
        //         sign = 1;
        //     rbody.AddForce(sign * (new Vector2(impulsionStrength, 0)), ForceMode2D.Impulse);
        // }

        // TODO: dampen the impulsion
        if (impulsionBeingApplied && false)
        {
        }

        MovePlayer(horizontalMovement);

        float vel = rbody.velocity.x;
        if (defaultDirection == Direction.Vertical)
            vel = rbody.velocity.y;
        Flip(vel);

        float characterVelocity = Mathf.Abs(vel);
        animator.SetFloat("Speed", characterVelocity);

        float pos = transform.position.x;
        float vel_ = rbody.velocity.x;
        if (defaultDirection == Direction.Vertical) {
            pos = transform.position.y;
            vel_ = rbody.velocity.y;
        }
        if (pos <= minBoundary && vel < 0)
            rbody.velocity = Vector2.zero;
        if (pos >= maxBoundary && vel > 0)
            rbody.velocity = Vector2.zero;
    }

    void MovePlayer(float _horizontalMovement)
    {

        Vector2 targetVelocity = Vector2.zero;
        if (defaultDirection == Direction.Horizontal)
            targetVelocity = new Vector2(_horizontalMovement, rbody.velocity.y);
        else
            targetVelocity = new Vector2(rbody.velocity.x, _horizontalMovement);
        rbody.velocity = Vector3.SmoothDamp(rbody.velocity, targetVelocity, ref velocity, .05f);
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public override void ChangeBehaviorToAutoMoving(CharacterState otherData)
    {
        animator.SetBool("IsControlled", false);
        animator.SetBool("IsAutoMoving", true);
        animator.SetBool("IsStill", false);
        rbody.bodyType = RigidbodyType2D.Kinematic;
        SetVelocity(defaultAutoMoveVelocity);

        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW AUTO-MOVING!!!");
        }
    }

    public override void FixedUpdateAutoMoving()
    {
        float pos;
        if (defaultDirection == Direction.Horizontal)
        {
            animator.SetBool("IsDirectionX", true);
            animator.SetBool("IsDirectionY", false);
            pos = transform.position.x;
        }
        else
        {
            animator.SetBool("IsDirectionX", false);
            animator.SetBool("IsDirectionY", true);
            pos = transform.position.y;
        }
        if (pos <= minBoundary || pos >= maxBoundary)
        {
            rbody.velocity *= -1;
            invertCachedSens();
        }
    }

    public override void ChangeBehaviorToStill(CharacterState otherData)
    {
        animator.SetBool("IsControlled", false);
        animator.SetBool("IsAutoMoving", false);
        animator.SetBool("IsStill", true);
        rbody.bodyType = RigidbodyType2D.Static;

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
