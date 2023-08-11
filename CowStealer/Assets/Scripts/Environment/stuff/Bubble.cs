using System;
using UnityEngine;

namespace Environment.stuff {
    public class Bubble : MonoBehaviour {
        private Rigidbody2D _body;
        private int ticks = 0;
        private int nextFlipTicks = 0;
        private Vector2 force = new Vector2(RandomUtils.odds(0.5) ? 0.25f : -0.25f, 0.25f);

        private void Start() {
            _body = GetComponent<Rigidbody2D>();

            transform.localScale *= (float) RandomUtils.doubleBetween(0.5, 1);
        }

        private void FixedUpdate() {
            if (ticks == nextFlipTicks) {
                nextFlipTicks = RandomUtils.intBetween(10, 50);

                // Flipping the force
                force.x *= -1;
            }
            
            _body.AddForce(force, ForceMode2D.Force);

            ticks++;
        }
    }
}