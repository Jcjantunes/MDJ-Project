using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    public string chosenLevel = "None";

    public CanvasGroup loadingScreen;
    public LoadingScreen loadingScreenScript;
    // Start is called before the first frame update


    public AudioSource audioSource;
    
    public AudioClip lockedSound;

    void Start()
    {
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.gameObject.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingScreen.alpha == 0) loadingScreen.gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectLevel();
        }
    }

    public void SelectLevel()
    {
        if (!chosenLevel.Equals("None"))
        {
            var id = chosenLevel.Split("_");
            if (GameMode.IsUnlocked(chosenLevel))
            {
                if (GameMode.isVersus) chosenLevel += "_versus";
                loadingScreenScript.LoadScene(chosenLevel);
            }

            else
            {
                audioSource.PlayOneShot(lockedSound);
            }
        }
    }

    public void SetChosenLevel(string new_level)
    {
        chosenLevel = new_level;
    }
}