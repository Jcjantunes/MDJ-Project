using UnityEngine;

namespace AI.concrete {
    public class VolcanoEruption : Environment {
        public double volcanoChance = 1 / (50.0 * 5);
        public GameObject player;

        public override void Start() {
            base.Start();
            player = GameObject.Find("ufo");
        }

        public void FixedUpdate() {
            if (GameMode.runningLevel && RandomUtils.odds(volcanoChance) && player.activeSelf) {
                // Creating a volcano
                volcanoShower((float) RandomUtils.doubleBetween(-6, 6));
            }
        }
    }
}