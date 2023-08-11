using System;
using System.Collections;
using LevelManagement;
using UnityEngine;

namespace DefaultNamespace {
    public class Lava : MonoBehaviour {

        public GameObject cowOnFirePrefab;
        public float timeToLive = 3;

        private static LevelManager _levelManager;
        
        private void Start() {
            StartCoroutine(destroy());

            if (_levelManager == null) {
                _levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManager>();
            }
            
            _levelManager.lavas.Add(this);
        }

        private void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.CompareTag("Cow")) {
                // Killing the cow
                Instantiate(cowOnFirePrefab, col.gameObject.transform.position, col.gameObject.transform.rotation);
                Destroy(col.gameObject);
            }
        }

        private IEnumerator destroy() {
            yield return new WaitForSeconds(timeToLive);
            
            Destroy(gameObject);
        }
    }
}