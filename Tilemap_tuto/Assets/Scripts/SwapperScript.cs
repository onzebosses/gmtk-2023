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

    private GameObject[] allCharacterGameObjects;
    private Character[] allCharacters;
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
        if (clickedCharacter == null)
            return;

        switch(status)
        {
            case SwapperStatus.Off:
                break;
            case SwapperStatus.OnZeroClicked:
                character1 = clickedCharacter;
                status = SwapperStatus.OnOneClicked;
                break;
            case SwapperStatus.OnOneClicked:
                character2 = clickedCharacter;
                swapBehaviors();
                character1 = null;
                character2 = null;
                status = SwapperStatus.Off;
                // QUESTION: does the swap happens immediatly?
                unfreezeGame();
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
            character.unfreezeCharacter();
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
            Debug.Log(character2.name);
        }
    }

}
