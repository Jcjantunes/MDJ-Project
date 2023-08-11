using System;
using UnityEngine;

namespace AI {
    public class AnimalAfraid : AnimalBehaviour {

        public double maxEscapingDistance = 5;
        public double runSpeed = 2;
        Animator animator;
        bool animating;
        public Rigidbody2D player;
        public bool isCow = false;
        public float fartChancePerTick = 0.0002f; // 0.02 -> average 1 fart per second. 50 ticks per second
        
        public override void Start() {
            base.Start();
            animator = GetComponent<Animator>();
        }

        public override Decision getDecision()
        {   
            if(GameObject.Find("ufo") != null) {
                var player = GameObject.Find("ufo");
                var playerPos = player.GetComponent<Rigidbody2D>().worldCenterOfMass;
                var animalPos = body.worldCenterOfMass;

                var difference = animalPos.x - playerPos.x;

                if (isCow && RandomUtils.odds(fartChancePerTick)) {
                    return new AnimalDecisionFart();
                }
                
                if (Math.Abs(difference) >= maxEscapingDistance) {
                    int rand = UnityEngine.Random.Range(0, 10);

                    if (!animating)
                    {
                        if (rand % 2 == 0)
                            animator.SetFloat("Speed", 0f);
                        else
                            animator.SetBool("IsTired", true);
                    }

                    animating = true;

                    return new AnimalDecisionWait();
                }
                else {
                    var direction = difference / Math.Abs(difference);

                    if ((direction < 0 && this.transform.localScale.x < 0) || (direction > 0 && this.transform.localScale.x > 0))
                    {
                        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                    }

                    animator.SetBool("IsTired", false);
                    animator.SetFloat("Speed", 10f);
                    animating = false;
                    return new AnimalDecisionMove(direction * runSpeed, 0);
                }
            }

            return new AnimalDecisionWait();
        }

        public override bool canMakeDecision() {
            return true;
        }

    }
}