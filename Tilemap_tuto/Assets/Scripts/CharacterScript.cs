using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behavior {Controllable, AutoMoving, Still};

public class CharacterData
{

}


// Character = lines of table
// Behavior = column of table
public class Character : MonoBehaviour
{
    public Behavior behavior;
    public bool isFrozen;
    public CharacterData previousState;
    public CharacterData currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (behavior == Behavior.Controllable)
        {
            UpdateControllable();
        }
        if (behavior == Behavior.AutoMoving)
        {
            UpdateMoving();
        }
        if (behavior == Behavior.Still)
        {
            UpdateStill();
        }
    }

    void ChangeBehavior(Behavior otherBehavior, CharacterData otherData)
    {
        if (otherBehavior == Behavior.Controllable)
        {
            InitControllable(otherData);
        }
        if (otherBehavior == Behavior.AutoMoving)
        {
            InitMoving(otherData);
        }
        if (otherBehavior == Behavior.Still)
        {
            InitStill(otherData);
        }
        behavior = otherBehavior;
    }

    public void InitControllable(CharacterData otherData)
    {

    }

    public void InitMoving(CharacterData otherData)
    {

    }

    public void InitStill(CharacterData otherData)
    {

    }

    public void UpdateControllable()
    {

    }

    public void UpdateMoving()
    {

    }

    public void UpdateStill()
    {

    }
}
