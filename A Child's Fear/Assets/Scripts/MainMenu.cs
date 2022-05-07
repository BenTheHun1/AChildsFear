using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    // Start is called before the first frame update
    public void StartGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Imaginari");
    }

    // Update is called once per frame
    public void Quit()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Application.Quit();
    }
}
