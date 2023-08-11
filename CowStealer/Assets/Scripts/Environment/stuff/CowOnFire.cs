using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace {
    public class CowOnFire : MonoBehaviour {

        private SpriteRenderer _spriteRenderer;

        public Color colorLossPerTick = new Color(0, 1, 1, 0);
        public float redPerTick = 0.007f;
        public float timeToLive = 2;
        
        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            StartCoroutine(destroy());
            
            AudioUtils.PlaySoundInterval(GetComponent<AudioSource>(), 0, timeToLive);
        }

        private void FixedUpdate() {
            _spriteRenderer.color -= colorLossPerTick * redPerTick;
        }

        private IEnumerator destroy() {
            yield return new WaitForSeconds(timeToLive);
            
            Destroy(gameObject);
        }
    }
}