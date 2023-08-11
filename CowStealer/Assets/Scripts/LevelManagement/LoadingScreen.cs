using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject screen;
    public Animator animator;
    public Slider loadingBar;
    public AudioClip[] clips;
    public AudioSource source;
    public bool isLoading = true;
    
    AsyncOperation loadingOperation = null;

    void Start()
    {
        //loadingBar.value = 0f;
    }
    
    public void LoadScene(string sceneName)
    {
        loadingBar.value = 0f;
        screen.SetActive(true);
        animator.SetTrigger("FadeIn");

        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    void Update()
    {
        if(loadingOperation != null)
        {
            loadingBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            if(loadingOperation.progress % 0.1f == 0){
                int index = Random.Range(0, clips.Length - 1);
                source.clip = clips[index];
                source.Play();
            }
        }
    }

    public void FadeOut()
    {
        isLoading = true;
        loadingBar.value = 1f;
        screen.SetActive(true);
        animator.SetTrigger("FadeOut");
        int index = Random.Range(0, clips.Length);
        source.clip = clips[index];
        source.Play();
        if(!this.gameObject.active) return;
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation(){
        yield return new WaitForSeconds(1f);
        isLoading = false;
    }
}