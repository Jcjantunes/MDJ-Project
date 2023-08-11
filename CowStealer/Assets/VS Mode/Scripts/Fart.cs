using System;
using System.Collections;
using UnityEngine;

namespace VS_Mode.Scripts {
    public class Fart : UFODestroyer {

        public float dragMultiplier = 2;
        public float slownessSeconds = 3;
        public float maxScale = 0.5f;
        public float acceleration = 5f;
        public Color infectedColor = new Color(0, 0.5f, 0);

        private Rigidbody2D _body;
        private BoxCollider2D fartCollider, topCollider, downCollider, leftCollider, rightCollider, noFlyCollider, cowCollider;
        private GameObject topLimit, downLimit, leftLimit, rightLimit, noFlyLimit;

        public override void Start() {
            base.Start();
            
            StartCoroutine(getBigger());
            
            _body = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate() {
            _body.AddForce(Vector2.up * acceleration, ForceMode2D.Force);
        }

        public override void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.name is "ufo") {
                StartCoroutine(slowDown(collision.rigidbody, collision.gameObject.GetComponent<SpriteRenderer>()));
                
                destroy(true);
            }
        }

        public IEnumerator slowDown(Rigidbody2D victim, SpriteRenderer renderer) {
            victim.drag *= dragMultiplier;
            renderer.color = infectedColor;
            
            //Debug.Log("Slowing down: " + victim.drag);

            yield return new WaitForSeconds(slownessSeconds);

            victim.drag /= dragMultiplier;
            renderer.color = Color.white;
            //Debug.Log("Speeding up down: " + victim.drag);
        }

        public IEnumerator getBigger() {
            while (transform.localScale.x < maxScale) {
                transform.localScale *= 1.005f;
                yield return new WaitForFixedUpdate();
            }
        }

        public void destroy(bool playCoughingSound) {
            AudioSource coughingSource = GetComponents<AudioSource>()[1];

            if (playCoughingSound) {
                coughingSource.Play();
                transform.position = Vector3.one * 9999f; // Moving the game object off screen while it finishes it's sound, then destroy it
                Destroy(gameObject, coughingSource.clip.length + slownessSeconds + 1);
            }
            else {
                transform.position = Vector3.one * 9999f; // Moving the game object off screen while it finishes it's sound, then destroy it
                Destroy(gameObject, slownessSeconds + 1);
            }
        }
    }
}