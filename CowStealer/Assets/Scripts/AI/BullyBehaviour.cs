using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AI {
    public abstract class BullyBehaviour : AIBehaviour {
        public Rigidbody2D body;
        public double speed = 1;
        public bool reachedDestination = true;
        public IEnumerator continueReachingDestination = null;
        public float cowSearchRadius = 3;
        public bool beamOn = false;
        public bool recentlyHit = false;
        
        // Need to keep track of these to undo collisions with UFO Destroyers
        public Vector2 lastVelocity;
        public Vector2 lastPosition;
        
        public override void Start() {
            base.Start();
            
            body = GetComponent<Rigidbody2D>();
        }

        public override void FixedUpdate() {
            base.FixedUpdate();

            lastVelocity = body.velocity;
            lastPosition = body.position;
        }

        public override void applyDecision() {
            getDecision().act(this);
        }

        public bool canTurnOnBeam() {
            Rigidbody2D nearestCow = findNearestCow();
            
            return nearestCow != null && Vector2.Distance(nearestCow.worldCenterOfMass, body.worldCenterOfMass) <= 4;
        }

        [CanBeNull]
        public Rigidbody2D findNearestCow() {
            float nearestDistance = float.MaxValue;
            Rigidbody2D nearestCow = null, cow;
            float distance;
            
            foreach (GameObject aux in GameObject.FindGameObjectsWithTag("Cow")) {
                cow = aux.GetComponent<Rigidbody2D>();
                distance = Vector2.Distance(cow.worldCenterOfMass, body.worldCenterOfMass);

                if (distance < nearestDistance && 
                    cow.worldCenterOfMass.x is >= -8 and <= 8) {
                    
                    nearestDistance = distance;
                    nearestCow = cow;
                }
            }

            return nearestCow == null ? null : nearestCow.GetComponent<Rigidbody2D>();
        }
    }
}