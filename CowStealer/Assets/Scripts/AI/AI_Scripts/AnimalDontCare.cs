using System;
using UnityEngine;
using Random = System.Random;

namespace AI {
    public class AnimalDontCare : AnimalBehaviour {
        public int minDecisionTicks = 2 * 50;
        public int maxDecisionTicks = 5 * 50;
        public double maxAcceleration = 3;
        public bool isCow = false;
        public float fartChanceEveryDecision = 0.1f;
        

        private int _nextDecisionTicks;
        private Decision _lastDecision;
        
        Animator animator;

        private static readonly Random random = new Random();

        public override void Start() {
            base.Start();
            
            _nextDecisionTicks = getNextDecisionTicks();
            animator = GetComponent<Animator>();
        }

        public override Decision getDecision() {

            if (ticks == _nextDecisionTicks) {
                //Time to make a decision
                _nextDecisionTicks = ticks + getNextDecisionTicks();
                double odds = RandomUtils.odds();

                if (isCow && odds < fartChanceEveryDecision) {
                    return new AnimalDecisionFart();
                }
                else if (odds < 0.4)
                {
                    int rand = UnityEngine.Random.Range(0, 10);

                    if (rand % 2 == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("cow_laying"))
                        animator.SetFloat("Speed", 0f);
                    else
                        animator.SetBool("IsTired", true);

                    return new AnimalDecisionWait();
                }
                else if (odds < 0.8) {
                    double velocityX = (random.NextDouble() - 0.5) * maxAcceleration;
                    if ((velocityX < 0 && this.transform.localScale.x < 0) || (velocityX > 0 && this.transform.localScale.x > 0))
                    {
                        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    }

                    animator.SetBool("IsTired", false);
                    animator.SetFloat("Speed", 10f);
                    _lastDecision = new AnimalDecisionMove(velocityX, 0);
                    
                    return _lastDecision;
                }
            }
            
            //No decision to make. Returning the last decision
            return _lastDecision ?? new AnimalDecisionWait();
        }

        public override bool canMakeDecision()
        {
            return ticks == _nextDecisionTicks;
        }

        private int getNextDecisionTicks()
        {
            return random.Next(minDecisionTicks, maxDecisionTicks + 1);
        }
    }
}