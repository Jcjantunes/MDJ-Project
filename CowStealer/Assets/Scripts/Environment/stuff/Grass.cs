using System.Security.Cryptography;
using AI;
using UnityEngine;

namespace Environment.stuff {
    public class Grass : Food {

        public float weightMultiplier = 3;
        public float scaleMultiplier = 1.5f;
        
        public override bool collidedWithCow(GameObject cow) {
            AnimalBehaviour behaviour = cow.GetComponent<AnimalBehaviour>();

            if (behaviour.hasEaten) {
                return false;
            }
            else {
                cow.GetComponent<Rigidbody2D>().mass *= weightMultiplier;
                cow.transform.localScale *= scaleMultiplier;
                cow.layer = 16; //Fed animal layer
            
                destroy();

                behaviour.hasEaten = true;
                
                return true;
            }
        }
    }
}