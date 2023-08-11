using AI;
using UnityEngine;

namespace Environment.stuff {
    public class Vodka : Food {

        public float weightMultiplier = 3;
        public float scaleMultiplier = 1.5f;
        
        public override bool collidedWithCow(GameObject cow) {
            AnimalBehaviour behaviour = cow.GetComponent<AnimalBehaviour>();

            if (behaviour.isDrunk) {
                return false;
            }
            else {
                cow.layer = 16; //Fed animal layer
                
                behaviour.isDrunk = true;
            
                destroy();

                return true;
            }
        }
    }
}