using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip powerdownSound;

    private void OnEnable() {
        PowerupEffect.OnPowerdown += PlayCowSound;
    }

    private void OnDisable() {
        PowerupEffect.OnPowerdown -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(powerdownSound);
    }
}
