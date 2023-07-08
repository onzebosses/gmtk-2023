using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerFootsteps : MonoBehaviour
{
    public PlayerMovement player;

    private EventInstance playerFootsteps;

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
    }

    private void Update()
    {
        if (player.isMoving && player.isGrounded)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }

        else
        {
            playerFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}