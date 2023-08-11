using System;
using System.Collections;
using UnityEngine;

namespace ScreenBlockers {
    
    public class ScreenBlocker : MonoBehaviour {
        public float secondsToDisappear = 4;

        private void Start() {
            spawnBehaviour(GetComponent<Rigidbody2D>());
            StartCoroutine(disappear());
        }

        public virtual void spawnBehaviour(Rigidbody2D body) {
            
        }

        public IEnumerator disappear() {
            yield return new WaitForSeconds(secondsToDisappear);
            
            Destroy(gameObject);
        }
    }
}