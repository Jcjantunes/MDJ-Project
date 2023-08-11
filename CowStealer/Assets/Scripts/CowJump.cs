using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowJump : MonoBehaviour {

    public float jumpHeight = 2;
    public Rigidbody2D body;
    
    // Start is called before the first frame update
    void Start() {
        Debug.Log(this.transform.position);
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var newVel = body.velocity;
            newVel.y += jumpHeight;
            body.velocity = newVel;
        }
    }
}
