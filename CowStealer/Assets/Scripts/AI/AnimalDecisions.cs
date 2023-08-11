using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AI {

    public class AnimalDecisionMove : Decision {
        public readonly double x;
        public readonly double y;

        public AnimalDecisionMove(double accelerationX, double accelerationY) {
            this.x = accelerationX;
            this.y = accelerationY;
        }

        public override void act(AnimalBehaviour animal) {
            animal.body.AddForce(new Vector2((float) x, (float) y));
        }
    }

    public class AnimalDecisionWait : Decision {
        public override void act(AnimalBehaviour animal) {
            // Do nothing
        }
    }

    public class AnimalDecisionFart : Decision {
        private static readonly GameObject _fartPrefab = Resources.Load<GameObject>("fartCloud");

        private GameObject _fart;
        
        public override void act(AnimalBehaviour animal) {
            if (_fart != null && !_fart.activeSelf) {
                _fart = null;
            }
            
            if (_fart == null) {
                Rigidbody2D body = animal.body;
                _fart = Object.Instantiate(_fartPrefab);
                
                    
                Rigidbody2D fartBody = _fart.GetComponent<Rigidbody2D>();

                if (animal.transform.lossyScale.x > 0) {
                    // Cow is facing left
                    fartBody.angularVelocity = 45;
                    fartBody.velocity = new Vector2(1, 0);
                    _fart.transform.position = body.transform.position + new Vector3(0.75f, 0, 0);
                }
                else {
                    // Cow is facing right
                    fartBody.angularVelocity = -45;
                    fartBody.velocity = new Vector2(-1, 0);
                    _fart.transform.position = body.transform.position + new Vector3(-0.75f, 0, 0);
                    
                    // Making the fart face the opposite way
                    Vector3 transformLocalScale = fartBody.transform.localScale;
                    
                    transformLocalScale.x *= -1;
                    fartBody.transform.localScale = transformLocalScale;
                }
            }
        }
    }
}