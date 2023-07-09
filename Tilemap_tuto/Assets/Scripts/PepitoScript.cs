using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PepitoScript : Character
{
    private bool isJumping;
    private bool isGrounded;
    private bool isAirborneAndStill;
    private bool isAirborneAndAutoMoving;

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
        rbody.bodyType = RigidbodyType2D.Dynamic;
        rbody.velocity = Vector2.zero;

        if (printDebug) {
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW CONTROLLABLE!!!");
        }
    }

    private Vector2 getGroundVelocity()
    {
        Vector2 res = Vector2.zero;
        if (!isGrounded)
            return res;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(groundCheckLeft.position, groundCheckRight.position);
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Character"))
            {
                if(col.gameObject.GetComponent<PlatformScript>() != null)
                {
                    if(col.gameObject.GetComponent<PlatformScript>().rbody.bodyType != RigidbodyType2D.Static)
                        res += col.gameObject.GetComponent<PlatformScript>().rbody.velocity; 
                }
            }
        }
        return res;
    }

    public override void FixedUpdateControllable()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move = movement.action.ReadValue<Vector2>();

        float horizontalMovement = move.x * defaultControllableVelocity * Time.deltaTime;

        if (jump.action.IsPressed() && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement);

        Flip(rbody.velocity.x);

        float characterVelocity = Mathf.Abs(rbody.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

        Vector2 groundVel = getGroundVelocity();
        // rbody.velocity += groundVel;
        transform.position += new Vector3(groundVel.x * Time.deltaTime, groundVel.y * Time.deltaTime, 0);
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
        rbody.bodyType = RigidbodyType2D.Dynamic;

        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        if (!isGrounded)
            isAirborneAndAutoMoving = true;
        else {
            isAirborneAndAutoMoving = false;
            rbody.bodyType = RigidbodyType2D.Kinematic;
            SetVelocity(defaultAutoMoveVelocity);
            getMinMaxBoundaries();
        }

        if (printDebug){
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW AUTO-MOVING!!!");
        }
    }

    public override void FixedUpdateAutoMoving()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        bool isOnPlatform = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckLeft.position) && Physics2D.OverlapArea(groundCheckRight.position, groundCheckRight.position);
        if (isGrounded && isAirborneAndAutoMoving)
        {
            isAirborneAndAutoMoving = false;
            rbody.bodyType = RigidbodyType2D.Kinematic;
            SetVelocity(defaultAutoMoveVelocity);
            getMinMaxBoundaries();
        } else if (transform.position.x <= minBoundary || transform.position.x >= maxBoundary || !isOnPlatform)
        {
            rbody.velocity *= -1;
            invertCachedSens();
        }
        Vector2 groundVel = getGroundVelocity();
        // rbody.velocity += groundVel;
        transform.position += new Vector3(groundVel.x * Time.deltaTime, groundVel.y * Time.deltaTime, 0);
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
        Vector2 groundVel = getGroundVelocity();
        transform.position += new Vector3(groundVel.x * Time.deltaTime, groundVel.y * Time.deltaTime, 0);
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
        Vector2 groundVel = getGroundVelocity();
        rbody.velocity += groundVel;
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
