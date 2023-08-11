using System;
using UnityEngine;

namespace AI.concrete {
    public class Thunderstorm : Environment {
        public double lightningChance = 1 / (50.0 * 2);
        public GameObject player;
        public AudioSource source;

        public override void Start() {
            base.Start();
            player = GameObject.Find("ufo");
            source = GameObject.Find("ThunderSound").GetComponent<AudioSource>();
        }

        public void FixedUpdate() {
            if (GameMode.runningLevel && RandomUtils.odds(lightningChance) && player.activeSelf) {
                // Creating thunder somewhere on top of the map
                source.Play();
                createThunder(new Vector2((float) RandomUtils.doubleBetween(-9, 9), 4), 
                    RandomUtils.intBetween(60, 90));
            }
        }
    }
}