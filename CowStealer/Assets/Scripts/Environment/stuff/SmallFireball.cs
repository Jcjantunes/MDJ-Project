using System;
using System.Collections;
using UnityEngine;

namespace Environment.stuff {
    public class SmallFireball : MonoBehaviour {
        public float timeToLive = 2;
        public float xDeviation = 0.2f;
        public float speed = 50;
        public GameObject fireballPrefab;

        private void Start() {
            Rigidbody2D body = GetComponent<Rigidbody2D>();

            Vector2 velocity = Vector2.up;

            velocity.x = (float) RandomUtils.doubleBetween(-xDeviation, xDeviation);
            velocity.Normalize();
            
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * velocity);

            body.velocity = velocity * speed;

            StartCoroutine(destroy());
        }

        private IEnumerator destroy() {
            yield return new WaitForSeconds(timeToLive);

            Instantiate(fireballPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.right));
            
            Destroy(gameObject);
        }
    }
}