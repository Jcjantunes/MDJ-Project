using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayCollissionFilter : MonoBehaviour
{
    // Start is called before the first frame update

    private BoxCollider2D hayCollider, floorCollider;
    
    void Start()
    {
        hayCollider = GetComponent<BoxCollider2D>();
        floorCollider = GameObject.Find("Floor").GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(hayCollider, floorCollider, true);
    }
}
