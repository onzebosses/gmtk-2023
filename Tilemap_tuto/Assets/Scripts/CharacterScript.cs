using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Behavior {Controllable, AutoMoving, Still, Bounce};
public enum Direction {Vertical, Horizontal};

public class CharacterState
{
    public Vector2 vel;
}


// Character = lines of table
// Behavior = column of table
public abstract class Character : MonoBehaviour
{
    // BEHAVIOR
    public Behavior behavior;
    public CharacterState currentState;
    public CharacterState previousState;

    public float defaultControllableVelocity;
    public float defaultAutoMoveVelocity;
    public float jumpForce;

    [SerializeField]
    public InputActionReference movement, jump;

    public bool isFrozen;
    public bool isSwappable;

    // RELATIVE TO MOVING BOUNDARIES
    // When switching to auto-move, these set the boundaries
    public Transform minBoundaryTransform;
    public Transform maxBoundaryTransform;
    public float minBoundary;
    public float maxBoundary;
    public Direction defaultDirection;
    private float cachedSens;
    public void invertCachedSens() { cachedSens *= -1; }
    public float getCachedSens() { return cachedSens;}

    // PHYSICS
    public Rigidbody2D rbody;
    public Vector2 velBeforeFreeze;
    public float gravScaleBeforeFreeze;

    // Start is called before the first frame update
    public virtual void Start()
    {
        cachedSens = 1;
        getMinMaxBoundaries();
        CharacterState dummyState = new CharacterState();
        ChangeBehavior(behavior, dummyState);
    }

    // TODO: make sure we call that when we switch to auto-move
    public void getMinMaxBoundaries()
    {
        switch(defaultDirection){
            case Direction.Horizontal:
                minBoundary = minBoundaryTransform.position.x;
                maxBoundary = maxBoundaryTransform.position.x;
                break;
            case Direction.Vertical:
                minBoundary = minBoundaryTransform.position.y;
                maxBoundary = maxBoundaryTransform.position.y;
                break;
            default:
                break;
        }
    }

    public void SetVelocity(float vel)
    {
        if (defaultDirection == Direction.Horizontal)
            rbody.velocity = new Vector2(cachedSens * vel, 0);
        if (defaultDirection == Direction.Vertical)
            rbody.velocity = new Vector2(0, cachedSens * vel);
    }

    //
    // public void setAutoMoveDefaultVelocity()
    // {
    //     if (defaultDirection == Direction.Horizontal)
    //         rbody.velocity = new Vector2(cachedSens * defaultAutoMoveVelocity, 0);
    //     if (defaultDirection == Direction.Vertical)
    //         rbody.velocity = new Vector2(0, cachedSens * defaultAutoMoveVelocity);
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (behavior){
            case Behavior.Controllable:
                FixedUpdateControllable();
                break;
            case Behavior.AutoMoving:
                FixedUpdateAutoMoving();
                break;
            case Behavior.Still:
                FixedUpdateStill();
                break;
            case Behavior.Bounce:
                FixedUpdateBounce();
                break;
            default:
                break;

        }
    }

    public void ChangeBehavior(Behavior otherBehavior, CharacterState otherState)
    {
        switch (otherBehavior){
            case Behavior.Controllable:
                ChangeBehaviorToControllable(otherState);
                break;
            case Behavior.AutoMoving:
                ChangeBehaviorToAutoMoving(otherState);
                break;
            case Behavior.Still:
                ChangeBehaviorToStill(otherState);
                break;
            case Behavior.Bounce:
                ChangeBehaviorToBounce(otherState);
                break;
            default:
                break;
        }
        behavior = otherBehavior;
        previousState = currentState;

        // TODO: update state, maybe it's a copy?
        // currentState = otherState;
    }

    public abstract void ChangeBehaviorToControllable(CharacterState otherState);

    public abstract void ChangeBehaviorToAutoMoving(CharacterState otherState);

    public abstract void ChangeBehaviorToStill(CharacterState otherState);

    public abstract void ChangeBehaviorToBounce(CharacterState otherState);

    public abstract void FixedUpdateControllable();

    public abstract void FixedUpdateAutoMoving();

    public abstract void FixedUpdateStill();

    public abstract void FixedUpdateBounce();

    public void freezeCharacter() 
    {
        if (rbody.bodyType != RigidbodyType2D.Static)
        {
            velBeforeFreeze = rbody.velocity;
            gravScaleBeforeFreeze = rbody.gravityScale;

            rbody.velocity = Vector2.zero;
            rbody.gravityScale = 0;
        }
    }

    public void unfreezeCharacter(bool hasNotBeenSwapped)
    {
        if (rbody.bodyType != RigidbodyType2D.Static) {
            // TODO: if has been swapped, do we need to set back to velBeforeFreeze
            if (hasNotBeenSwapped)
                rbody.velocity = velBeforeFreeze;
            rbody.gravityScale = gravScaleBeforeFreeze;
        }
    }
}
