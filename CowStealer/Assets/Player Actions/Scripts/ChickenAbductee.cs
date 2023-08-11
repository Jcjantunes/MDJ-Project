using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAbductee : Abductee {

    public GameObject featherPrefab;
    public int amountFeathers = 20;
    
    private AudioSource _chickenSound;

    public override void Start() {
        base.Start();

        _chickenSound = GetComponent<AudioSource>();
    }

    public override void Abduct() {
        for (int i = 0; i < amountFeathers; i++) {
            GameObject feather = Instantiate(featherPrefab);
            feather.transform.position = transform.position;
        }

        _chickenSound.Play();
        transform.position = Vector3.one * 9999f; // Moving the game object off screen while it finishes it's sound, then destroy it
        Destroy(gameObject, _chickenSound.clip.length);
    }
}
