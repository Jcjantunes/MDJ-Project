using UnityEngine;

namespace ScreenBlockers {
    public class Bacon : ScreenBlocker {
        public double maxVelocity = 5;
        public double minVelocity = 2;
        public double maxScale = 0.25;
        public double minScale = 0.1;
        public double maxAngularVelocity = 5;
        public double minAngularVelocity = 2;
        
        public override void spawnBehaviour(Rigidbody2D body) {
            body.velocity = RandomUtils.randomVector(RandomUtils.doubleBetween(minVelocity, maxVelocity));
            body.angularVelocity = (float) RandomUtils.doubleBetween(minAngularVelocity, maxAngularVelocity);

            transform.localScale *= (float) RandomUtils.doubleBetween(minScale, maxScale);
        }
    }
}