using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSThrowSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip throwSound;

    private void OnEnable() {
        FarmerAttacks.OnThrow += PlayCowSound;
    }

    private void OnDisable() {
        FarmerAttacks.OnThrow -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(throwSound);
    }
}
