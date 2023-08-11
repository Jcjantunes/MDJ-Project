using System;
using UnityEngine;

public class SimplePlayerControls : MonoBehaviour {
    
    public Rigidbody2D body;
    public float speedPerTick = 5;
    
    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 acceleration = new Vector2();
        
        // Updating the acceleration
        if(Input.GetKey(KeyCode.W)) {
            acceleration.y += speedPerTick;
        }
        
        if(Input.GetKey(KeyCode.A)) {
            acceleration.x -= speedPerTick;
        }
        
        if(Input.GetKey(KeyCode.S)) {
            acceleration.y -= speedPerTick;
        }
        
        if(Input.GetKey(KeyCode.D)) {
            acceleration.x += speedPerTick;
        }

        // Adding acceleration
        
        body.AddForce(acceleration, ForceMode2D.Force);
    }
}