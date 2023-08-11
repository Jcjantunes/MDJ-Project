using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementLevelSelection : MonoBehaviour
{

    public float speed = .1f;

    public GameObject topRightLimitGameObject;
    public GameObject bottomLeftLimitGameObject;

    private Vector3 topRightLimit;
    private Vector3 bottomLeftLimit;

    private Vector2 input;

    void Start() {
        topRightLimit = topRightLimitGameObject.transform.position;
        bottomLeftLimit = bottomLeftLimitGameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        float yDirection = Input.GetAxisRaw("Vertical");

        input = new Vector2 (xDirection, yDirection);
        
        if((transform.position.x <= bottomLeftLimit.x && input.x == -1) || (transform.position.x >= topRightLimit.x && input.x == 1)){
            input.x = 0;
        }

         if((transform.position.y <= bottomLeftLimit.y && input.y == -1) || (transform.position.y >= topRightLimit.y && input.y == 1)){
            input.y = 0;
        }
       
        Vector3 moveDirection = new Vector3(input.x * speed, input.y * speed, 0);
        transform.position += moveDirection;
    }

}
