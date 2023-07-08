using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behavior {Controllable, Moving, Still};

public class CharacterData
{

}


// Character = lines of table
// Behavior = column of table
public class Character : MonoBehaviour
{
    public Behavior behavior;
    public bool is_frozen;
    public CharacterData previous_state;
    public CharacterData current_state;

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
        if (behavior == Behavior.Moving)
        {
            UpdateMoving();
        }
        if (behavior == Behavior.Still)
        {
            UpdateStill();
        }
    }

    void ChangeBehavior(Behavior other_behavior, CharacterData other_data)
    {
        if (other_behavior == Behavior.Controllable)
        {
            InitControllable(other_data);
        }
        if (other_behavior == Behavior.Moving)
        {
            InitMoving(other_data);
        }
        if (other_behavior == Behavior.Still)
        {
            InitStill(other_data);
        }
        behavior = other_behavior;
    }

    public void InitControllable(CharacterData other_data)
    {

    }

    public void InitMoving(CharacterData other_data)
    {

    }

    public void InitStill(CharacterData other_data)
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
