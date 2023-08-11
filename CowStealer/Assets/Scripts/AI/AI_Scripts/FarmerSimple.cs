using UnityEngine;
using System;

namespace AI {
    public class FarmerSimple : FarmerBehaviour {
        
        public Rigidbody2D player;

        public double hasHayWaitOdds = 0.5;
        public double hasHayFireAtPlayerOdds = 0.2;
        public double hasHayFireRandomlyOdds = 0.3;

        public double doestHaveHaySpawnHayOdds = 0.5;

        private int _nextDecisionTicks;

        public static event Action OnThrow;
        
        public override void Start() {
            base.Start();
            
            player = GameObject.Find("ufo").GetComponent<Rigidbody2D>();
        }
        
        public override Decision getDecision() {
            _nextDecisionTicks = ticks;
            
            double odds = RandomUtils.odds();

            if (hasHay()) {
                if (odds < hasHayWaitOdds) {
                    _nextDecisionTicks += 50;
                    return new FarmerDecisionWait();
                }
                else if (odds < hasHayWaitOdds + hasHayFireAtPlayerOdds) {
                    //Throwing at player
                    _nextDecisionTicks += 1;
                    OnThrow?.Invoke();
                    return new FarmerDecisionThrowHayAtUFO(player, RandomUtils.doubleBetween(5, 10));
                }
                else if (odds < hasHayWaitOdds + hasHayFireAtPlayerOdds + hasHayFireRandomlyOdds) {
                    //Throwing randomly
                    _nextDecisionTicks += 1;
                    OnThrow?.Invoke();
                    return new FarmerDecisionThrowHay(new Vector2((float) RandomUtils.doubleBetween(-10, -2.5), 
                        (float) RandomUtils.doubleBetween(2.5, 10)));
                }
            }
            else {
                _nextDecisionTicks += 50;
                if (odds < doestHaveHaySpawnHayOdds) {
                    return new FarmerDecisionSpawnHay();
                }
                else {
                    return new FarmerDecisionWait();
                }
            }

            _nextDecisionTicks += 50;

            return new FarmerDecisionWait();
        }

        public override bool canMakeDecision() {
            return ticks == _nextDecisionTicks;
        }
    }
}