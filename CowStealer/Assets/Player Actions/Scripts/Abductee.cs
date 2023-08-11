using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public abstract class Abductee : MonoBehaviour, IAbductable {
    Rigidbody2D rb;

    public bool hasTarget;
    Vector3 targetPosition;
    public float moveSpeed = 5f;
    
    public int altFlag = 0;
    public int altUpFlag = 0;

    public int UFOFlag = 0;
    public int BullyFlag = 0;

    protected BeamCollider beamVar;

    private Vector2 input;

    //public AudioSource audioSource;
    
    //public AudioClip cowSound;

    public virtual void Start() {
        
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Abduct();

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent<BeamCollider>(out BeamCollider beam)){
            beamVar =  beam;
            if(collision.gameObject.tag == "UFOBeamCollider") {
                UFOFlag = 1;
                BullyFlag = 0;    
            }

            if(collision.gameObject.tag == "BullyBeamCollider") {
                BullyFlag = 1;
                UFOFlag = 0;
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "UFOBeamCollider") {
                UFOFlag = 0; 
        }

        else if(other.gameObject.tag == "BullyBeamCollider") {
                BullyFlag = 0;   
        }
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space)) {
            altUpFlag = 1;
            altFlag = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            altFlag = 1;
            altUpFlag = 0;
        }
    }

    void FixedUpdate() {
        if(UFOFlag == 1) {
            if(altUpFlag == 1) {
                altFlag = 0;
                altUpFlag = 0;
                hasTarget = false;
                rb.AddForce(new Vector2(0, 0));
                if (this.GetComponent<AnimalDontCare>() != null) this.GetComponent<AnimalDontCare>().enabled = true;
                if (this.GetComponent<AnimalAfraid>() != null) this.GetComponent<AnimalAfraid>().enabled = true;
                //audioSource.Stop();
            }

            //Debug.Log(altFlag);
            if(altFlag == 1) {
                if(hasTarget) {
                    if (this.GetComponent<AnimalDontCare>() != null) this.GetComponent<AnimalDontCare>().enabled = false;
                    if (this.GetComponent<AnimalAfraid>() != null) this.GetComponent<AnimalAfraid>().enabled = false;
                    Vector2 targetDirection = (targetPosition - transform.position).normalized;
                    input = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
                    rb.AddForce(input * moveSpeed);
                    //audioSource.PlayOneShot(cowSound);
                }
            }
        }

        else if (BullyFlag == 1) {
            if(beamVar.bullyAbductFlag == 1) {
                if(hasTarget) {
                        if (this.GetComponent<AnimalDontCare>() != null) this.GetComponent<AnimalDontCare>().enabled = false;
                        if (this.GetComponent<AnimalAfraid>() != null) this.GetComponent<AnimalAfraid>().enabled = false;
                        Vector2 targetDirection = (targetPosition - transform.position).normalized;
                        input = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
                        rb.AddForce(input * moveSpeed);
                        //audioSource.PlayOneShot(cowSound);
                }
            }
        }
    }

    public void SetTarget() {
        hasTarget = true;
    }

    public void SetTargeyPosition(Vector3 position) {
        targetPosition = position;
    }

    public void deleteTarget() {
        targetPosition = Vector3.zero;
        hasTarget = false;
    }
}