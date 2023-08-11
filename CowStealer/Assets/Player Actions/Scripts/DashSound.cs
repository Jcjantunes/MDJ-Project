using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip dashSound;

    private void OnEnable() {
        Movement.OnDash += PlayCowSound;
    }

    private void OnDisable() {
        Movement.OnDash -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(dashSound);
    }
}
