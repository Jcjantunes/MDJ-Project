using UnityEngine;

using Random = System.Random;

namespace AI {
    
    public class BullySimple : BullyBehaviour {
        
        public float maxAcceleration = 20;
        public float minAcceleration = 40;
        public int minDecisionTicks = 25;
        public int maxDecisionTicks = 50;
        
        private int _nextDecisionTicks;
        private Decision _defaultDecision;

        private static readonly Random random = new Random();
        
        public override void Start() {
            base.Start();

            _defaultDecision = new BullyDecisionWait();
        }

        public override Decision getDecision() {
            if (reachedDestination) {
                if (!beamOn && canTurnOnBeam()) {
                    // Beam is off and there's a nearby cow. Leaving the beam on for 1 second
                    
                    _nextDecisionTicks += 50;

                    return new BullyDecisionToggleBeam(true);
                }
                else if (beamOn) {
                    setNextDecisionTicks();
                    
                    return new BullyDecisionToggleBeam(false);
                }
                
                double odds = RandomUtils.odds();

                if (odds < 0.2) {
                    // Don't do anything
                    setNextDecisionTicks();

                    return new BullyDecisionWait();
                }
                else if (odds < 0.4) {
                    // Moving towards the player
                    _nextDecisionTicks++;

                    if (GameObject.Find("ufo") != null) {
                        return new BullyDecisionMoveTo(
                            (float)RandomUtils.doubleBetween(minAcceleration, maxAcceleration),
                            GameObject.Find("ufo").GetComponent<Rigidbody2D>().worldCenterOfMass);
                    }
                    else {
                        return new BullyDecisionWait();
                    }
                } 
                else if (odds < 0.6) {
                    // Move to a random spot
                    _nextDecisionTicks++;
                    
                    return new BullyDecisionMoveTo((float)RandomUtils.doubleBetween(minAcceleration, maxAcceleration),
                        new Vector2((float) RandomUtils.doubleBetween(-7, 7), 4));
                }
                else {
                    // Move towards a cow
                    _nextDecisionTicks++;

                    Rigidbody2D nearestCow = findNearestCow();

                    if (nearestCow != null) {
                        Debug.Log("Bully moving towards cow");
                        
                        return new BullyDecisionMoveTo(
                            (float)RandomUtils.doubleBetween(minAcceleration, maxAcceleration),
                            nearestCow.worldCenterOfMass + 2 * Vector2.up);
                    }
                    else {
                        return new BullyDecisionWait();
                    }
                }
            }
            else {
                continueReachingDestination.MoveNext();

                _nextDecisionTicks++;
                
                return new BullyDecisionWait();
            }
        }
        
        public override bool canMakeDecision() {
            return ticks == _nextDecisionTicks;
        }

        private void setNextDecisionTicks() {
            _nextDecisionTicks = ticks + random.Next(minDecisionTicks, maxDecisionTicks + 1);
        }
    }
}