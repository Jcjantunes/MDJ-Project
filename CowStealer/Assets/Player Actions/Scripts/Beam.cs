using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private GameObject beam;

    public int altFlag;
    public int altUpFlag;

    public AudioSource audioSource;
    
    public AudioClip cowAbductingSound;

    void Start() {
        beam = GameObject.Find("Beam");
        beam.SetActive(false);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            altFlag = 1;
        }

        if(Input.GetKeyUp(KeyCode.Space)) {
            altUpFlag = 1;
            altFlag = 0;
        }
        
        if(altUpFlag == 1) {
            if(beam.activeSelf != false) {
                beam.SetActive(false);
                audioSource.Stop();
            }
            altUpFlag = 0;
        }

        if(altFlag == 1) {
            if(beam.activeSelf != true) {
                beam.SetActive(true);
                audioSource.PlayOneShot(cowAbductingSound);
            }
        }
    }
}
