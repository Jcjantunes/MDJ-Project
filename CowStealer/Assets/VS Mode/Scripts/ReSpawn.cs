using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    public IEnumerator Respawn(float timeToDespawn, float timeToRespawn) {
        if(gameObject != null) {
            yield return new WaitForSeconds(timeToDespawn);
            gameObject.SetActive(false);
            Debug.Log("aquiu2");
            yield return new WaitForSeconds(timeToRespawn);
            Debug.Log("aquiu3");
            gameObject.SetActive(true);
            Debug.Log("aquiu4");
        }
    }
}
