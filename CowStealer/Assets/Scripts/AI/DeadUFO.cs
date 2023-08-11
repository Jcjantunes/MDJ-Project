using System;
using System.Collections;
using System.Numerics;
using DefaultNamespace;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace AI {
    public class DeadUFO : MonoBehaviour {

        public float secondsToDisappear = 3;
        public double minSpin = 40;
        public double maxSpin = 100;
        public AudioSource collidingSoundSource;

        private GameObject[] fires;
        
        private void Start() {
            StartCoroutine(disappear());

            collidingSoundSource = GetComponents<AudioSource>()[0];
            
            Rigidbody2D body = GetComponent<Rigidbody2D>();

            body.angularVelocity = (float) RandomUtils.doubleBetween(minSpin, maxSpin);

            // Saving the UFO's fires to correct their rotation
            fires = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++) {
                fires[i] = transform.GetChild(i).gameObject;
            }
            
            // Ignoring collisions
            
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), 
                GameObject.Find("NoFlyingZoneCollider").GetComponent<BoxCollider2D>(), true);
            
            // Playing fire sound
            
            AudioUtils.PlaySoundInterval(GetComponents<AudioSource>()[2], 0, secondsToDisappear);
        }

        private void FixedUpdate() {
            foreach (GameObject fire in fires) {
                fire.transform.rotation = Quaternion.identity;
            }
        }

        private void OnCollisionEnter2D(Collision2D col) {
            collidingSoundSource.Play();
        }

        public IEnumerator disappear() {
            yield return new WaitForSeconds(secondsToDisappear);

            transform.position = Vector3.up * 9999;
            Destroy(gameObject, 3);
        }
    }
}