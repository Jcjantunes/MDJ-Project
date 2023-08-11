using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip powerupSound;

    private void OnEnable() {
        PowerupEffect.OnPowerup += PlayCowSound;
    }

    private void OnDisable() {
        PowerupEffect.OnPowerup -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(powerupSound);
    }
}
