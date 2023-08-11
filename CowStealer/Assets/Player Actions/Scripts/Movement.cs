using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public float speed = 30f;
    public float maxSpeed = 200f;
    public float normalSpeed = 30f;
    public float startDashTime = .1f;
    private float dashTime;
    private int spaceFlag;
    private Vector2 input;
    private Rigidbody2D body;
    
    public static event Action OnDash;

    void Start() {
        dashTime = startDashTime;
        spaceFlag = 0;
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame

    void Update() {

        float xDirection = Input.GetAxisRaw("Horizontal");
        float yDirection = Input.GetAxisRaw("Vertical");
        input = new Vector2 (xDirection, yDirection);
        
        if(Input.GetKeyDown(KeyCode.C)) {
            OnDash?.Invoke();
            spaceFlag = 1;
        }

        if(spaceFlag == 1) {
            if(dashTime <= 0) {
                dashTime = startDashTime;
                normalSpeed = speed;
                spaceFlag = 0;
            }
            else {
                normalSpeed = maxSpeed;
                dashTime -= Time.deltaTime;
            }
        }
    }

    void FixedUpdate() {
        body.AddForce(input * normalSpeed);
    }
}