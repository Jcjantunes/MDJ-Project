using System;
using System.Collections;
using UnityEngine;

namespace AI.stuff {
    public class LightningRay : UFODestroyer {
        public int maxDepth;
        public Vector2 direction, spawn;
        public float rayLifetimeSeconds = 1;
        public float raySpreadSeconds = 0.02f;
        public int nextRayTicks = 2;
        public double splitProbability = 0.1;
        public int splitMinCost = 5;
        public int splitMaxCost = 20;
        public double xNoise = 0.1;
        public Environment environment;
        
        public float length;

        public override void Start() {
            base.Start();
            
            length = (GetComponent<BoxCollider2D>().size.y * transform.localScale.y) * 0.8f;
            
            StartCoroutine(createChildRay());
            StartCoroutine(destroyAfterwards());
        }

        public IEnumerator destroyAfterwards() {
            yield return new WaitForSeconds(rayLifetimeSeconds);

            transform.position = Vector2.up * 9999;

            yield return new WaitForSeconds(10);
            
            Destroy(gameObject);
        }

        public IEnumerator createChildRay() {
            yield return new WaitForSeconds(raySpreadSeconds);

            Vector2 newDirection = direction + new Vector2((float) RandomUtils.doubleBetween(-xNoise, xNoise), 0);
            newDirection.Normalize();
            Vector2 translation = spawn + newDirection * length;
            
            environment.summonLightning(translation, newDirection, maxDepth - 1);

            if (RandomUtils.odds(splitProbability)) {
                Vector2 differentDirection = new Vector2((float) RandomUtils.doubleBetween(-1, 1), 
                    (float) RandomUtils.doubleBetween(-1.5, 0));
                
                differentDirection.Normalize();

                Vector2 differentTranslation = spawn + differentDirection * length;

                environment.summonLightning(differentTranslation, differentDirection, 
                    maxDepth - RandomUtils.intBetween(splitMinCost, splitMaxCost + 1));
            }
        }
    }
}