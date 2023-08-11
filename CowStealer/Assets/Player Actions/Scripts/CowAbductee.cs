using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using AI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CowAbductee : Abductee
{
    public static event Action OnCowAbducted;

    private AudioSource _abductSound;
    private AudioSource _burpSound;

    private static bool _ufoShaking = false;

    public override void Start() {
        base.Start();

        _abductSound = GetComponents<AudioSource>()[0];
        _burpSound = GetComponents<AudioSource>()[1];
    }

    public override void Abduct() {
        if(BullyFlag == 0) {
            OnCowAbducted?.Invoke();
        }
        
        AudioSource abductSound;
        
        if (GetComponent<AnimalBehaviour>().isDrunk) {
            if (!_ufoShaking) {
                _ufoShaking = true;
                StartCoroutine(shakeUFO());
            }
            
            abductSound = _burpSound;
        }
        else {
            abductSound = _abductSound;
        }
        
        abductSound.Play();

        transform.position = Vector3.up * 5892;
        
        Destroy(gameObject, 10);
        
    }

    public IEnumerator shakeUFO() {
        GameObject ufo = GameObject.FindGameObjectWithTag("UFO");
        GameObject bubbles = ufo.transform.Find("Bubbles").gameObject;
        Vector3 vector;
        int maxShakes = 6;
        
        bubbles.SetActive(true);
        
        for (int shake = 0; shake < maxShakes; shake++) {
            float angle;

            if (shake == 0) {
                angle = 2;
            }
            else if (shake == (maxShakes - 1)) {
                angle = -2;
            }
            else {
                angle = (shake % 2 == 0) ? 4 : -4;
            }
            
            for (int i = 0; i < 25; i++) {
                yield return new WaitForFixedUpdate();
                
                vector = ufo.transform.rotation.eulerAngles;
                ufo.transform.rotation = Quaternion.Euler(vector.x, vector.y, vector.z + angle);
            }
        }
        
        bubbles.SetActive(false);

        _ufoShaking = false;
    }
}
