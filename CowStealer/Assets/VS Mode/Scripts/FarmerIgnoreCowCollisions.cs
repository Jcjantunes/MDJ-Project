using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerIgnoreCowCollisions : MonoBehaviour
{
    private BoxCollider2D FarmerCollider, cowCollider;
    private Rigidbody2D rb;
    public GameObject[] cows;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        FarmerCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        
        cows = GameObject.FindGameObjectsWithTag("Cow");
        
        foreach (GameObject cow in cows)
        {
            cowCollider = cow.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(FarmerCollider, cowCollider, true);
        }
    }
}
