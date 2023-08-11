using System;
using System.Collections;
using UnityEngine;

namespace AI.stuff {
    public class FadeInAndOut : MonoBehaviour {
        public float fadeInSeconds = 1;
        public float aliveSeconds = 1;
        public float fadeOutSeconds = 1;
        public float timeToDestroy = 5;

        private SpriteRenderer _renderer;
        
        private void Start() {
            _renderer = GetComponent<SpriteRenderer>();

            Color currentColor = _renderer.color;

            currentColor.a = 0;
            
            // Removing the alpha from the sprite
            _renderer.color = currentColor;
            
            StartCoroutine(appearAndDisappear());
        }

        private double fadePerTick(float seconds) {
            return 1f / (50f * seconds);
        }

        public IEnumerator appearAndDisappear() {
            Color fadeInPerTick = new Color(0, 0, 0, (float) fadePerTick(fadeInSeconds));
            Color fadeOutPerTick = new Color(0, 0, 0, -(float) fadePerTick(fadeOutSeconds));

            while (_renderer.color.a < 1) {
                yield return new WaitForFixedUpdate();

                _renderer.color += fadeInPerTick;
            }

            yield return new WaitForSeconds(aliveSeconds);

            while (_renderer.color.a > 0) {
                _renderer.color += fadeOutPerTick;
                
                yield return new WaitForFixedUpdate();
            }
            
            transform.position = Vector3.one * 9999f;
            Destroy(gameObject, timeToDestroy);
        }
    }
}