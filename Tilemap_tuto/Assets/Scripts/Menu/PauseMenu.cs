using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    //public GameObject runMouse;
    public GameObject settingsActive;
    public GameObject buttonCanvas;
    public GameObject prefabCanvas;
    public GameObject pauseCanvas;


    public static bool isPaused;

    private PauseBrokerScript pauseBroker;

    [SerializeField]
    private InputActionReference pauseKey;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(isPaused);
        //runMouse.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update Triggered");
        // Debug.Log(EventSystem.current);
        //Debug.Log(settingsActive.activeInHierarchy);
        if (pauseKey.action.WasPressedThisFrame())
        {
            if (isPaused && !settingsActive.activeInHierarchy)
            {
                ResumeGame();
            }
            else if (isPaused && settingsActive.activeInHierarchy)
            {
                settingsActive.SetActive(false);
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        //runMouse.SetActive(false);
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        //runMouse.SetActive(true);
        Cursor.visible = false;
        pauseBroker = GameObject.FindGameObjectWithTag("PauseBroker").GetComponent<PauseBrokerScript>();
        pauseBroker.reinstantiatePauseMenu(this.GameObject());
        //Destroy(buttonCanvas);
        //buttonCanvas = Instantiate(prefabCanvas);
        //buttonCanvas.transform.parent = pauseCanvas.transform;
    }

    public void Restart()
    {
        // Debug.Log(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
        //runMouse.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
