using UnityEngine;

namespace AI.concrete {
    public class FeedingTime : Environment {
        public double vodkaChance = 1 / (50.0 * 3);
        public double grassChance = 1 / (50.0 * 2);
        public int minVodka = 1;
        public int maxVodka = 3;
        public int minGrass = 2;
        public int maxGrass = 5;
        public float minSpeed = 2;
        public float maxSpeed = 4;
        public GameObject player;
        public Vector2 spawnPosition; // Must be set in editor

        public override void Start() {
            base.Start();
            player = GameObject.Find("ufo");
        }

        public void FixedUpdate() {
            if (GameMode.runningLevel && player.activeSelf) {
                if (RandomUtils.odds(vodkaChance)) {
                    spawnVodka(spawnPosition, getRandomVelocity());
                }

                if (RandomUtils.odds(grassChance)) {
                    spawnGrass(spawnPosition, getRandomVelocity());
                }
            }
        }

        public Vector2 getRandomVelocity() {
            Vector2 vector = new Vector2(RandomUtils.floatBetween(-1, 0), RandomUtils.floatBetween(0, 1));
            
            vector.Normalize();
            vector *= RandomUtils.floatBetween(minSpeed, maxSpeed);

            return vector;
        }
    }
}