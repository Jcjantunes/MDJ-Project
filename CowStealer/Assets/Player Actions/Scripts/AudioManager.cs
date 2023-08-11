using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip cowSound;

    private void OnEnable() {
        CowAbductee.OnCowAbducted += PlayCowSound;
    }

    private void OnDisable() {
        CowAbductee.OnCowAbducted -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(cowSound);
    }
}
