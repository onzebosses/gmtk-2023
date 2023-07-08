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

    public override void InitControllable(CharacterData otherData)
    {

    }

    public override void InitAutoMoving(CharacterData otherData)
    {

    }

    public override void InitStill(CharacterData otherData)
    {

    }

    public override void UpdateControllable()
    {
        rbody.angularVelocity = 50;
    }

    public override void UpdateAutoMoving()
    {

    }

    public override void UpdateStill()
    {

    }
}
