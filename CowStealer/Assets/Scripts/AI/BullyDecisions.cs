using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace AI {
    public class BullyDecisionMove : Decision {
        public readonly double accelerationX;
        public readonly double accelerationY;

        public BullyDecisionMove(double accelerationX, double accelerationY) {
            this.accelerationX = accelerationX;
            this.accelerationY = accelerationY;
        }

        public override void act(BullyBehaviour bully) {
            bully.body.AddForce(new Vector2((float) accelerationX, (float) accelerationY), ForceMode2D.Force);
        }
    }

    public class BullyDecisionWait : Decision {
        public override void act(BullyBehaviour bully) {
            // Do nothing
        }
    }

    public class BullyDecisionToggleBeam : Decision {

        private bool turnOn;
        
        public BullyDecisionToggleBeam(bool turnOn) {
            Debug.Log("Bully is turning " + turnOn + " his tractor beam");
            this.turnOn = turnOn;
        }
        
        public override void act(BullyBehaviour bully) {
            bully.beamOn = turnOn;
            //TODO

            var bullyAlienCollider = GameObject.Find("BullyTractionBeamCollider");

            if(bully.beamOn) {
                bullyAlienCollider.GetComponent<BullyBeamCollider>().bullyAbductFlag = 1;
            }

            else if(bully.beamOn == false) {
                bullyAlienCollider.GetComponent<BullyBeamCollider>().bullyAbductFlag = 0;
            }
        }
    }

    public class BullyDecisionMoveTo : Decision {

        private readonly float _acceleration;
        private readonly Vector2 _destination;
        
        public BullyDecisionMoveTo(float acceleration, Vector2 destination) {
            _acceleration = acceleration;
            _destination = destination;
        }

        public override void act(BullyBehaviour bully) {
            bully.reachedDestination = false;
            bully.continueReachingDestination = moving(bully);
        }

        private IEnumerator moving(BullyBehaviour bully) {
            Vector2 previousVelocity = bully.body.velocity;
            Vector2 acceleration = (_destination - bully.body.worldCenterOfMass).normalized * _acceleration;
            int ticks = 0;
            
            while (Vector2.Distance(bully.body.worldCenterOfMass, _destination) >= 0.5 && ticks <= 100) {
                acceleration = (_destination - bully.body.worldCenterOfMass).normalized * _acceleration;
                bully.body.AddForce(acceleration);
                previousVelocity = bully.body.velocity;
                ticks++;
                yield return null;
            }

            ticks = 0;

            while (Vector2.Dot(previousVelocity, bully.body.velocity) > 0 && ticks <= 50) {
                // While the current velocity is facing the same way as the previous, de-accelerate
                bully.body.AddForce(-acceleration);
                ticks++;
                yield return null;
            }

            bully.reachedDestination = true;
            bully.continueReachingDestination = null;
        }
    }

    public class BullyDecisionImpulseMoveTo : Decision {
        private readonly Vector2 _destination;

        public BullyDecisionImpulseMoveTo(Vector2 destination) {
            _destination = destination;
        }

        public override void act(BullyBehaviour bully) {
            // ---Incomplete---
            Rigidbody2D body = bully.GetComponent<Rigidbody2D>();
            Vector2 direction = (_destination - body.position);
            float distance = direction.magnitude;
            float mass = body.mass;
            float drag = body.drag;
            
            direction.Normalize();

            // After many hours of WolframAlpha + Brain power, these formulas came up:
            float distanceTimesDragDividedByMass = (distance * drag) / mass;
            float speed = (float) (0.01 * Math.Exp(distanceTimesDragDividedByMass));
            
            Debug.Log("Dir: " + direction + ", distance: " + distance + ", speed: " + speed);
            
            body.velocity = direction * speed;
        }
    }
}