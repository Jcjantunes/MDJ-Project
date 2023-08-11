using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSound : MonoBehaviour
{
    public AudioSource audioSource;
    
    [SerializeField] AudioClip throwSound;

    private void OnEnable() {
        AI.FarmerSimple.OnThrow += PlayCowSound;
    }

    private void OnDisable() {
        AI.FarmerSimple.OnThrow -= PlayCowSound;
    }

    private void Awake() {

    }

    public void PlayCowSound() {
        audioSource.PlayOneShot(throwSound);
    }
}
