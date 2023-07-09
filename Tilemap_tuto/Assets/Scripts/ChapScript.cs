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

    // SIZE AUGMENTATION
    public Transform transformBottomHitBoxWhenElongated;
    public float rateElongationOffsetHitbox;
    public float deltaOffsetHitbox;
    private bool isElongating;
    public float maxOffsetHitbox;
    public float defaultOffsetHitbox;
    public float otherDefaultOffsetHitbox;

    public BoxCollider2D proxyCollider;
    public bool printDebug;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        deltaOffsetHitbox = 0;
        if (defaultDirection == Direction.Horizontal)
        {
            defaultOffsetHitbox = proxyCollider.offset.y;
            Debug.Log(transformBottomHitBoxWhenElongated.position.y - transform.position.y);
            maxOffsetHitbox = (transformBottomHitBoxWhenElongated.position.y - transform.position.y) - defaultOffsetHitbox;
            otherDefaultOffsetHitbox = proxyCollider.offset.x;
        }
        if (defaultDirection == Direction.Vertical)
        {
            defaultOffsetHitbox = proxyCollider.offset.x;
            maxOffsetHitbox = getAlphaRotation() * (transformBottomHitBoxWhenElongated.position.x - transform.position.x) - defaultOffsetHitbox;
            otherDefaultOffsetHitbox = proxyCollider.offset.y;
        }
        // maxOffsetHitbox = 0;

        printDebug = true;
    }

    public override void ChangeBehaviorToControllable(CharacterState otherData)
    {
        rbody.bodyType = RigidbodyType2D.Kinematic;
        rbody.velocity = Vector2.zero;
        isElongating = false;
        deltaOffsetHitbox = 0;

        if (printDebug) {
            Debug.Log(gameObject.name);
            Debug.Log("I AM NOW CONTROLLABLE!!!");
        }
    }

    public override void FixedUpdateControllable()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        move = movement.action.ReadValue<Vector2>();

        // float horizontalMovement = 0;
        float horizontalMovement = move.x * defaultControllableVelocity * Time.deltaTime;
        if (defaultDirection == Direction.Vertical)
            horizontalMovement *= -getAlphaRotation();

        // if (defaultDirection == Direction.Horizontal)
        // else
        //     horizontalMovement = move.y * defaultControllableVelocity * Time.deltaTime;

        if (jump.action.IsPressed()) {
            if (!isElongating) {
                isElongating = true;
            }
            if (deltaOffsetHitbox < maxOffsetHitbox)
            {
               deltaOffsetHitbox += rateElongationOffsetHitbox;
            }
        } else {
            if (deltaOffsetHitbox > 0)
            {
                deltaOffsetHitbox -= rateElongationOffsetHitbox;
            }
        }

        if (deltaOffsetHitbox < 0)
        {
            deltaOffsetHitbox = 0;
        }
        if (deltaOffsetHitbox > maxOffsetHitbox)
        {
            deltaOffsetHitbox = maxOffsetHitbox;
        }

        updateColliderWithOffset();

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
        if ((pos <= minBoundary && vel_ < 0) || jump.action.IsPressed())
            rbody.velocity = Vector2.zero;
        if ((pos >= maxBoundary && vel_ > 0) || jump.action.IsPressed())
            rbody.velocity = Vector2.zero;
    }

    private void updateColliderWithOffset()
    {
        if (defaultDirection == Direction.Horizontal)
            proxyCollider.offset = new Vector2(otherDefaultOffsetHitbox, defaultOffsetHitbox + deltaOffsetHitbox);
        if (defaultDirection == Direction.Vertical)
            proxyCollider.offset = new Vector2(defaultOffsetHitbox + deltaOffsetHitbox, otherDefaultOffsetHitbox);
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
        // if (_velocity > 0.1f)
        // {
        //     if (defaultDirection == Direction.Horizontal)
        //         spriteRenderer.flipX = false;
        //     else
        //         spriteRenderer.flipY = false;
        // } else if(_velocity < -0.1f)
        // {
        //     if (defaultDirection == Direction.Horizontal)
        //         spriteRenderer.flipX = true;
        //     else
        //         spriteRenderer.flipY = true;
        // }
    }

    public override void ChangeBehaviorToAutoMoving(CharacterState otherData)
    {
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
            pos = transform.position.x;
        else
            pos = transform.position.y;
        if (pos <= minBoundary || pos >= maxBoundary)
        {
            rbody.velocity *= -1;
            invertCachedSens();
        }
    }

    public override void ChangeBehaviorToStill(CharacterState otherData)
    {
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
