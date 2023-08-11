using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Environment.stuff {
    public class Volcano : MonoBehaviour {

        public float timeToErupt = 3;
        public int minFireballs = 3;
        public int maxFireballs = 7;
        public GameObject smallFireballPrefab;
        public Vector3 fireballSpawnOffset = new(0, 0.5f, 0);
        public float secondsBetweenFireballs = 0.3f;
        
        private AudioSource _audioSource;

        private void Start() {
            _audioSource = GetComponent<AudioSource>();

            StartCoroutine(erupt());
        }

        public IEnumerator erupt() {
            yield return new WaitForSeconds(timeToErupt);

            _audioSource.Play();
            
            int amount = RandomUtils.intBetween(minFireballs, maxFireballs + 1);

            for (int i = 0; i < amount; i++) {
                Instantiate(smallFireballPrefab, transform.position + fireballSpawnOffset, 
                    Quaternion.identity);
                
                yield return new WaitForSeconds(secondsBetweenFireballs);
            }
        }
    }
}