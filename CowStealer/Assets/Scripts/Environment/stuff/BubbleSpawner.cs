using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Environment.stuff {
    public class BubbleSpawner : MonoBehaviour {

        public GameObject bubblePrefab;
        public int ticksPerBubble = 15;

        private int ticks = 0;
        
        private void FixedUpdate() {
            if (ticks % ticksPerBubble == 0) {
                GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            }

            ticks++;
        }
    }
}