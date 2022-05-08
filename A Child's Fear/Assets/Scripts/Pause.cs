using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private CameraControllerFPS cc;
    private Slider sense;

    // Start is called before the first frame update
    void Start()
    {
        sense = GameObject.Find("Sensitivity").GetComponent<Slider>();
        cc = GameObject.Find("Main Camera").GetComponent<CameraControllerFPS>();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0)
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    public void LoadScene(string sceneToLoad)
    {
        gameObject.GetComponent<AudioSource>().Play();
        if (sceneToLoad == "MomTest")
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            SceneManager.LoadScene("Working");
        }
    }

    public void changeSense()
    {
        cc.mouseSensitivity = sense.value;
    }
    public void QuitGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}
