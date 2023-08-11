using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAbductee : Abductee
{
    public GameObject baconPrefab;
    public int amountBacon = 100;
    
    private AudioSource _pigSound;

    public override void Start() {
        base.Start();

        _pigSound = GetComponent<AudioSource>();
    }

    public override void Abduct() {
        for (int i = 0; i < amountBacon; i++) {
            GameObject bacon = Instantiate(baconPrefab);
            bacon.transform.position = transform.position;
        }
        
        _pigSound.Play();
        transform.position = Vector3.one * 9999f; // Moving the game object off screen while it finishes it's sound, then destroy it
        Destroy(gameObject, _pigSound.clip.length);
    }
}
