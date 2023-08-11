using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BeamCollider : MonoBehaviour
{
    public Abductee abducteeVar;
    
    public int collisionFlag = 0;

    public int altFlag;
    public int altUpFlag;

    public AudioSource audioSource;
    
    public AudioClip cowAbductingSound;

    public GameObject beam;
    public GameObject outOfRangeBeam;

    public int bullyAbductFlag = 0;

    public abstract void Start();

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent<Abductee>(out Abductee abductee)){
            abducteeVar = abductee;
            
            collisionFlag = 1;

            if(outOfRangeBeam != false) {
                outOfRangeBeam.SetActive(false);
            }
            abducteeVar.SetTarget(); 
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(abducteeVar != null) {
            abducteeVar.deleteTarget();
        }
        abducteeVar = null;
        collisionFlag = 0;
    }

    public abstract void Update();
}
