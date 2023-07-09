using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Menu SFX")]
    [field: SerializeField] public EventReference menuOpen { get; private set; }
    [field: SerializeField] public EventReference menuClose { get; private set; }
    [field: SerializeField] public EventReference menuOver { get; private set; }
    [field: SerializeField] public EventReference menuValid { get; private set; }


    [field: Header("Swap Menu SFX")]
    [field: SerializeField] public EventReference swapMenuLoop { get; private set; }
    [field: SerializeField] public EventReference swapMenuSelect { get; private set; }


    [field: Header("LVL SFX")]
    [field: SerializeField] public EventReference lvlStartSound { get; private set; }
    [field: SerializeField] public EventReference lvlEndSound { get; private set; }


    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }


    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }


    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference playerLandSound { get; private set; }
    [field: SerializeField] public EventReference playerJumpSound { get; private set; }
    [field: SerializeField] public EventReference playerDespawn { get; private set; }
    [field: SerializeField] public EventReference playerRespawn { get; private set; }



    [field: Header("Mover SFX")]
    [field: SerializeField] public EventReference moverExtender { get; private set; }
    [field: SerializeField] public EventReference moverRetracter { get; private set; }
    [field: SerializeField] public EventReference moverFootsteps { get; private set; }


    [field: Header("Platform SFX")]
    [field: SerializeField] public EventReference platformSlide { get; private set; }
    [field: SerializeField] public EventReference platFormFall { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}