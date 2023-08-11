using UnityEngine;
using LevelManagement;

namespace AI {
    public abstract class AIBehaviour : MonoBehaviour {
        
        protected int ticks = 0;
        
        private LevelManager _levelManager;
        
        public virtual void Start() {
            _levelManager = GameObject.Find("LevelManagement").GetComponent<LevelManager>();
        }
        
        public virtual void FixedUpdate() {
            if (canMakeDecision()) {
                applyDecision();
            }

            if (_levelManager.hasStarted) {
                ticks++;
            }
        }

        public abstract Decision getDecision();

        public abstract void applyDecision();
        
        public virtual bool canMakeDecision() {
            return true;
        }
    }
}