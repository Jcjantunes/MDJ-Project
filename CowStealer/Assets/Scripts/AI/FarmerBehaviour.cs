using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class FarmerBehaviour : AIBehaviour {

        public Rigidbody2D body;
        public GameObject hayPrefab;
        public GameObject currentHay; // Hay currently being held by the farmer. May be null

        public BoxCollider2D farmerCollider;
        
        private static LinkedList<BoxCollider2D> hayColliders = new();
        
        public override void Start() {
            base.Start();
            
            body = GetComponent<Rigidbody2D>();
            hayPrefab = Resources.Load<GameObject>("hay");
            farmerCollider = GameObject.Find("farmer").GetComponent<BoxCollider2D>();
        }

        public override void applyDecision() {
            getDecision().act(this);
        }

        public bool hasHay() {
            return currentHay != null;
        }

        public void spawnHay() {
            if (!hasHay()) {
                currentHay = Instantiate(hayPrefab);
                Vector3 hayPosition = transform.position;

                hayPosition.x -= 1f;
            
                currentHay.transform.position = hayPosition;
                
                /*BoxCollider2D collider = currentHay.GetComponent<BoxCollider2D>();
                
                Physics2D.IgnoreCollision(collider, farmerCollider);

                //List to collect all hay colliders that are still valid
                LinkedList<BoxCollider2D> validHayColliders = new();

                //Making it so no hay blocks can collide with each-other
                foreach (BoxCollider2D hayCollider in hayColliders) {
                    Physics2D.IgnoreCollision(collider, hayCollider);
                    validHayColliders.AddLast(hayCollider);
                }

                hayColliders.AddLast(collider);*/
            }
        }

        public void throwHay(Vector2 velocity) {
            if (hasHay()) {
                Rigidbody2D hayBody = currentHay.GetComponent<Rigidbody2D>();

                hayBody.velocity = velocity;
                hayBody.angularVelocity = 5;

                currentHay = null;
            }
        }
    }
}