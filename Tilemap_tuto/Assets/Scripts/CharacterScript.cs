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
        switch (behavior){
            case Behavior.Controllable:
                UpdateControllable();
                break;
            case Behavior.AutoMoving:
                UpdateAutoMoving();
                break;
            case Behavior.Still:
                UpdateStill();
                break;
            default:
                break;

        }
    }

    void ChangeBehavior(Behavior otherBehavior, CharacterData otherData)
    {
        switch (behavior){
            case Behavior.Controllable:
                InitControllable(otherData);
                break;
            case Behavior.AutoMoving:
                InitAutoMoving(otherData);
                break;
            case Behavior.Still:
                InitStill(otherData);
                break;
            default:
                break;

        }
    }

    public void InitControllable(CharacterData otherData)
    {

    }

    public void InitAutoMoving(CharacterData otherData)
    {

    }

    public void InitStill(CharacterData otherData)
    {

    }

    public void UpdateControllable()
    {

    }

    public void UpdateAutoMoving()
    {

    }

    public void UpdateStill()
    {

    }
}
