using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void Menu()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Credits()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Credits");
    }
}
