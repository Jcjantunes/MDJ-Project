using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionFilter : MonoBehaviour
{
    private BoxCollider2D UFOCollider, cowCollider, floorCollider;
    private Rigidbody2D rb;
    public GameObject[] cows;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        UFOCollider = GetComponent<BoxCollider2D>();
        floorCollider = GameObject.Find("Floor").GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(UFOCollider, floorCollider, true);
    }

    void Update() {
        
        cows = GameObject.FindGameObjectsWithTag("Cow");
        
        foreach (GameObject cow in cows)
        {
            cowCollider = cow.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(UFOCollider, cowCollider, true);
        }
    }

}
