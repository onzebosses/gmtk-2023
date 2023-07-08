using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behavior {Controllable, AutoMoving, Still};

public class CharacterState
{

}


// Character = lines of table
// Behavior = column of table
public abstract class Character : MonoBehaviour
{
    public Behavior behavior;
    public bool isFrozen;
    public CharacterState previousState;
    public CharacterState currentState;

    // Start is called before the first frame update
    public abstract void Start();

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

    public abstract void FixedUpdateControllable();

    public abstract void FixedUpdateAutoMoving();

    public abstract void FixedUpdateStill();

    public abstract void freezeCharacter();

    public abstract void unfreezeCharacter();
}
