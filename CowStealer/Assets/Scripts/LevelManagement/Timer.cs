using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0;
    public int duration = 60;
    public Text timer;
    public AudioSource audioSource;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
            currentTime = currentTime - Time.deltaTime;

        if(audioSource != null && !audioSource.isPlaying){
            if(currentTime <= 10)
                audioSource.Play();
        }

        if(currentTime >= 0)
            timer.text = currentTime.ToString("F0");

        else{
            timer.text = "Time's Up!";
            if(audioSource!=null) audioSource.Stop();
            this.StopTimer();
        }
    }

    public void StartTimer(){
        isActive = true;
    }

    public void StopTimer(){
        isActive = false;
    }

    public int GetDuration(){
        return duration;
    }

    public float GetCurrent(){
        return currentTime;
    }

    public bool HasFinished(){
        return !isActive;
    }
}
