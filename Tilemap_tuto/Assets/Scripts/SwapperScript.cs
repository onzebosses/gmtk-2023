using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapperScript : MonoBehaviour
{
    public enum SwapperStatus {Off, OnZeroClicked, OnOneClicked};
    public SwapperStatus status = SwapperStatus.Off;
    private bool rightClicked = false;
    private bool leftClicked = false;

    private Character character1;
    private Character character2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If swapper is off, check if right click:
        //     if so, the game freezes
        // If swapper is on, swapper can "increment" or "decrement":
        //     increment with left click -> select an character or if two characters selected, goes back to unfreeze
        //     decrement with right click -> de-select an character or goes back to unfreeze with no characters selected
        // TODO: take Justin's click machine to set "right/leftClicked" attributes
        switch(status)
        {
            case SwapperStatus.Off:
                if (rightClicked)
                {
                    freezeGame();
                    status = SwapperStatus.OnZeroClicked;
                }
                break;
            case SwapperStatus.OnZeroClicked:
                if (leftClicked){
                    // -------------------------------
                    // TODO: select the first character
                    // -------------------------------
                    character1 = getCharacterUnderCursor();
                    status = SwapperStatus.OnOneClicked;
                }
                if (rightClicked)
                {
                    status = SwapperStatus.Off;
                }
                break;
            case SwapperStatus.OnOneClicked:
                if (leftClicked){
                    // ---------------------------------------------------
                    // TODO: select the second character and make the swap
                    // ---------------------------------------------------
                    character2 = getCharacterUnderCursor();
                    swapBehaviors();
                    character1 = null;
                    character2 = null;
                    status = SwapperStatus.Off;
                    // QUESTION: does the swap happens immediatly?
                    unfreezeGame();
                }
                if (rightClicked)
                {
                    character1 = null;
                    status = SwapperStatus.OnZeroClicked;
                }
                break;
            default:
                break;

        }
    }

    public Character getCharacterUnderCursor()
    {
        // TODO: return a character
    }

    public void swapBehaviors()
    {
        // TODO
    }

    public void freezeGame()
    {
        // TODO
    }

    public void unfreezeGame()
    {
        // TODO
    }
}
