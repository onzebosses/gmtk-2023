using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croco : Character
{
    public Rigidbody2D rbody;
    // Start is called before the first frame update
    public override void Start()
    {
       behavior = Behavior.Controllable; 
    }

    public override void ChangeBehaviorToControllable(CharacterState otherData)
    {

    }

    public override void ChangeBehaviorToAutoMoving(CharacterState otherData)
    {

    }

    public override void ChangeBehaviorToStill(CharacterState otherData)
    {

    }

    public override void FixedUpdateControllable()
    {
        rbody.angularVelocity = 50;
    }

    public override void FixedUpdateAutoMoving()
    {

    }

    public override void FixedUpdateStill()
    {

    }

    public override void freezeCharacter()
    {
        Debug.Log(gameObject.name);
        Debug.Log("I AM FROZEN!!!");
    }

    public override void unfreezeCharacter()
    {
        Debug.Log(gameObject.name);
        Debug.Log("I AM LIBREEEEEEEEEE!!!");
    }
}
