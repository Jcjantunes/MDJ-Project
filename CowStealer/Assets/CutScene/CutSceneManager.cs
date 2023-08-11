using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables; 

public class CutSceneManager : MonoBehaviour
{
    public bool levelFlag = false;
    public string nextSceneName;
    public bool canContinue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (canContinue)
            {
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene()
    {
        if (levelFlag)
        {
            if (GameMode.isVersus)
            {
                SceneManager.LoadScene("level_1_versus");
            }
            else
            {
                SceneManager.LoadScene("level_1");
            }
        }

        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
