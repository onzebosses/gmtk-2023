using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorSwapper : MonoBehaviour
{
    public enum SwapperStatus {Off, OnZeroClicked, OnOneClicked};
    public SwapperStatus status = SwapperStatus.Off;
    public bool printDebug;

    private Character character1;
    private Character character2;
    private Camera mainCam;

    private GameObject clickedGameObject;
    private Character clickedCharacter;
    public float defaultCharactersRadius;

    private GameObject[] allCharacterGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        clickedGameObject = null;
        clickedCharacter = null;
        // TODO: turn that off
        printDebug = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If swapper is off, check if right click:
        //     if so, the game freezes
        // If swapper is on, swapper can "increment" or "decrement":
        //     increment with left click -> select an character or if two characters selected, goes back to unfreeze
        //     decrement with right click -> de-select an character or goes back to unfreeze with no characters selected
        // TODO: Graphical updates if necessary
        switch(status)
        {
            case SwapperStatus.Off:
                break;
            case SwapperStatus.OnZeroClicked:
                break;
            case SwapperStatus.OnOneClicked:
                break;
            default:
                break;
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        switch(status)
        {
            case SwapperStatus.Off:
                freezeGame();
                status = SwapperStatus.OnZeroClicked;
                break;
            case SwapperStatus.OnZeroClicked:
                status = SwapperStatus.Off;
                unfreezeGame();
                break;
            case SwapperStatus.OnOneClicked:
                character1 = null;
                status = SwapperStatus.OnZeroClicked;
                break;
            default:
                break;
        }
        if (printDebug)
            Debug.Log(status);
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (status == SwapperStatus.Off)
        {
            if (printDebug)
                Debug.Log(status);
            return;
        }

        clickedGameObject = getGameObjectUnderCursor();
        if (clickedGameObject == null)
            return;

        clickedCharacter = clickedGameObject.GetComponent<Character>();
        if (clickedCharacter == null || !clickedCharacter.isSwappable)
            return;

        switch(status)
        {
            case SwapperStatus.Off:
                break;
            case SwapperStatus.OnZeroClicked:
                character1 = clickedCharacter;
                // TODO: show radius around character 1
                // TODO: animation cursor 1
                status = SwapperStatus.OnOneClicked;
                break;
            case SwapperStatus.OnOneClicked:
                character2 = clickedCharacter;
                float dist = Vector2.SqrMagnitude(character1.gameObject.transform.position - character2.gameObject.transform.position);
                // TODO: animation cursor 2
                if (dist > defaultCharactersRadius)
                {
                    character2 = null;
                    break;
                }
                swapBehaviors();
                status = SwapperStatus.Off;
                // QUESTION: does the swap happens immediatly?
                unfreezeGame();
                character1 = null;
                character2 = null;
                break;
            default:
                break;

        }
        if (printDebug)
            Debug.Log(status);
    }

    public GameObject getGameObjectUnderCursor()
    {
        var rayHit = Physics2D.GetRayIntersection(mainCam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (rayHit.collider == null)
            return null;
        return rayHit.collider.gameObject;
    }

    public void freezeGame()
    {
        // Get all by tag and if has component character, we freeze them
        allCharacterGameObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject gameObject in allCharacterGameObjects)
        {
            Character character = gameObject.GetComponent<Character>();
            // TODO: Visual change to handle!!!
            character.freezeCharacter();
        }

        if (printDebug)
            Debug.Log("FREEZED GAME!!!");
    }

    public void unfreezeGame()
    {
        // Get all by tag and if has component character, we freeze them
        allCharacterGameObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject gameObject in allCharacterGameObjects)
        {
            Character character = gameObject.GetComponent<Character>();
            bool hasNotBeenSwapped = !(character == character1 || character == character2);
            character.unfreezeCharacter(hasNotBeenSwapped);
        }

        if (printDebug)
            Debug.Log("UNFREEZE GAME!!!");
    }

    public void swapBehaviors()
    {
        // TODO
        Behavior behaviorTemp = character1.behavior;
        character1.ChangeBehavior(character2.behavior, character2.currentState);
        character2.ChangeBehavior(behaviorTemp, character1.previousState);

        if (printDebug)
        {
            Debug.Log("SWAPPED BEHAVIORS!!!");
            Debug.Log(character1.name);
            Debug.Log(character1.behavior);
            Debug.Log(character2.name);
            Debug.Log(character2.behavior);
        }
    }

}
