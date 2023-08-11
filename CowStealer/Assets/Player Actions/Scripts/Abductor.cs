using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abductor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){

        IAbductable abductable = collision.GetComponent<IAbductable>();
        
        if(abductable != null) {
            abductable.Abduct();
        }
        
    }
}
