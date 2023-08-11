using System;
using System.Collections;
using UnityEngine;

namespace Environment.stuff {
    public abstract class Food : MonoBehaviour {

        public float timeToLive = 8;
        
        private bool _scheduledForDestruction = false;

        private void Start() {
            StartCoroutine(destroyLater());
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Cow")) {
                if (collidedWithCow(collision.gameObject)) {
                    // Collision has been accepted and processed
                    GetComponents<AudioSource>()[0].Play();
                }
            }
        }

        public abstract bool collidedWithCow(GameObject cow);

        public IEnumerator destroyLater() {
            yield return new WaitForSeconds(timeToLive);

            if (!_scheduledForDestruction) {
                destroy();
            }
        }

        public void destroy() {
            _scheduledForDestruction = true;
            
            transform.position = Vector3.up * 8000;
            
            Destroy(gameObject, 5);
        }
    }
}