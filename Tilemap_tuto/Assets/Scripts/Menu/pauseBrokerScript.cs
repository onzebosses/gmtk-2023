using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBrokerScript : MonoBehaviour
{
    public GameObject pauseMenuPrefab;
    public void reinstantiatePauseMenu(GameObject toDestroy)
    {
        //Debug.Log("LETS DESTROY");
        Destroy(toDestroy);
        Instantiate(pauseMenuPrefab);
    }
}
