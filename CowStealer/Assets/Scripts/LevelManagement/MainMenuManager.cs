using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SinglePlayer()
    {
        audioSource.Play();
        GameMode.isVersus = false;
        loadingScreen.LoadScene("InitialScene");
        //loadingScreen.LoadScene("UI - Level Selection");
    }

    public void VersusMode()
    {
        audioSource.Play();
        GameMode.isVersus = true;
        loadingScreen.LoadScene("InitialScene");
        //loadingScreen.LoadScene("UI - Level Selection");
    }
}
