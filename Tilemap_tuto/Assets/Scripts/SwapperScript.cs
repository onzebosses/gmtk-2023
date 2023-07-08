using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorSwapper : MonoBehaviour
{
    public enum SwapperStatus {Off, OnZeroClicked, OnOneClicked};
    public SwapperStatus status = SwapperStatus.Off;
    private bool rightClicked = false;
    private bool leftClicked = false;

    private Character character1;
    private Character character2;
    private Camera mainCam;
    public bool printDebug;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // // If swapper is off, check if right click:
        // //     if so, the game freezes
        // // If swapper is on, swapper can "increment" or "decrement":
        // //     increment with left click -> select an character or if two characters selected, goes back to unfreeze
        // //     decrement with right click -> de-select an character or goes back to unfreeze with no characters selected
        // // TODO: take Justin's click machine to set "right/leftClicked" attributes
        // switch(status)
        // {
        //     case SwapperStatus.Off:
        //         if (rightClicked)
        //         {
        //             freezeGame();
        //             status = SwapperStatus.OnZeroClicked;
        //         }
        //         break;
        //     case SwapperStatus.OnZeroClicked:
        //         if (leftClicked){
        //             // -------------------------------
        //             // TODO: select the first character
        //             // -------------------------------
        //             character1 = getGameObjectUnderCursor();
        //             status = SwapperStatus.OnOneClicked;
        //         }
        //         if (rightClicked)
        //         {
        //             status = SwapperStatus.Off;
        //         }
        //         break;
        //     case SwapperStatus.OnOneClicked:
        //         if (leftClicked){
        //             // ---------------------------------------------------
        //             // TODO: select the second character and make the swap
        //             // ---------------------------------------------------
        //             character2 = getGameObjectUnderCursor();
        //             swapBehaviors();
        //             character1 = null;
        //             character2 = null;
        //             status = SwapperStatus.Off;
        //             // QUESTION: does the swap happens immediatly?
        //             unfreezeGame();
        //         }
        //         if (rightClicked)
        //         {
        //             character1 = null;
        //             status = SwapperStatus.OnZeroClicked;
        //         }
        //         break;
        //     default:
        //         break;
        //
        // }
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

        GameObject gameObject = getGameObjectUnderCursor();
        if (gameObject == null)
            return;

        Character character = gameObject.GetComponent<Character>();
        if (character == null)
            return;

        switch(status)
        {
            case SwapperStatus.Off:
                break;
            case SwapperStatus.OnZeroClicked:
                character1 = character;
                status = SwapperStatus.OnOneClicked;
                break;
            case SwapperStatus.OnOneClicked:
                character2 = character;
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

    public void swapBehaviors()
    {
        // TODO
        if (printDebug)
            Debug.Log("SWAPPED BEHAVIORS!!!");
    }

    public void freezeGame()
    {
        // TODO
        if (printDebug)
            Debug.Log("FREEZED GAME!!!");
    }

    public void unfreezeGame()
    {
        // TODO
        if (printDebug)
            Debug.Log("UNFREEZE GAME!!!");
    }
}
