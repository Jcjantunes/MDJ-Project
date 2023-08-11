using UnityEngine;

namespace AI {
    public abstract class AnimalBehaviour : AIBehaviour {

        public Rigidbody2D body;
        public bool hasEaten = false;
        
        private bool _isDrunk = false;
        

        public override void Start() {
            base.Start();
            
            body = GetComponent<Rigidbody2D>();
        }

        public override void applyDecision() {
            getDecision().act(this);
        }

        public bool isDrunk {
            get => _isDrunk;
            set {
                _isDrunk = value;

                transform.Find("BubbleSpawner").gameObject.SetActive(_isDrunk);
            }
        }
    }
}