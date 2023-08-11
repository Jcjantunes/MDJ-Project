using UnityEngine;

namespace AI {
    public class FarmerDecisionMove : Decision {
        public readonly double x;
        public readonly double y;

        public FarmerDecisionMove(double velocityX, double velocityY) {
            this.x = velocityX;
            this.y = velocityY;
        }

        public override void act(FarmerBehaviour farmer) {
            farmer.body.velocity = new Vector2((float) x, (float) y);
        }
    }
    
    public class FarmerDecisionWait : Decision {
        public override void act(FarmerBehaviour animal) {
            //Making the animal stand still
            animal.body.velocity = new Vector2(0, 0);
        }
    }

    public class FarmerDecisionSpawnHay : Decision {
        public override void act(FarmerBehaviour farmer) {
            farmer.spawnHay();
        }
    }

    public class FarmerDecisionThrowHay : Decision {
        private readonly Vector2 _hayVelocity;

        public FarmerDecisionThrowHay(Vector2 hayVelocity) {
            _hayVelocity = hayVelocity;
        }

        public override void act(FarmerBehaviour farmer) {
            farmer.throwHay(_hayVelocity);
        }
    }

    public class FarmerDecisionThrowHayAtUFO : Decision {

        private readonly Rigidbody2D _player;
        private readonly float _speed;

        public FarmerDecisionThrowHayAtUFO(Rigidbody2D player, double speed) {
            _player = player;
            _speed = (float) speed;
        }
        
        public override void act(FarmerBehaviour farmer) {
            Vector2 direction = _player.position - farmer.body.position;
            
            direction.Normalize();
            
            farmer.throwHay(direction * _speed);
        }
    }
}