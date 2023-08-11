using System;
using System.Collections;
using AI.stuff;
using UnityEngine;

namespace AI {
    public class Environment : MonoBehaviour {
        
        public GameObject rayPrefab;
        public GameObject cloudPrefab;
        public GameObject volcanoPrefab;
        public GameObject grassPrefab;
        public GameObject vodkaPrefab;

        public virtual void Start() {
            rayPrefab = Resources.Load<GameObject>("lightning_ray");
            cloudPrefab = Resources.Load<GameObject>("pixelCloud");
            volcanoPrefab = Resources.Load<GameObject>("volcano");
            grassPrefab = Resources.Load<GameObject>("grassBale");
            vodkaPrefab = Resources.Load<GameObject>("vodka");
        }

        public void createThunder(Vector2 where, int maxDepth) {
            GameObject cloud = Instantiate(cloudPrefab);

            cloud.transform.position = where;

            FadeInAndOut script = cloud.GetComponent<FadeInAndOut>();

            // Summoning lightning after the cloud has formed
            StartCoroutine(summonLightningDelayed(where, new Vector2(0, -1), maxDepth,
                script.fadeInSeconds + (script.aliveSeconds / 2)));
        }

        public IEnumerator summonLightningDelayed(Vector2 where, Vector2 direction, int maxDepth, float delaySeconds) {
            yield return new WaitForSeconds(delaySeconds);
            
            summonLightning(where, direction, maxDepth);
        }

        public void summonLightning(Vector2 where, Vector2 direction, int maxDepth) {
            if (maxDepth > 0) {
                GameObject ray = Instantiate(rayPrefab);
                
                ray.transform.position = where;
                ray.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

                LightningRay lightningRay = ray.GetComponent<LightningRay>();

                lightningRay.spawn = where;
                lightningRay.direction = direction.normalized;
                lightningRay.maxDepth = maxDepth;
                lightningRay.environment = this;
            }
        }

        public void earthquake(Vector2 where) {
            
        }

        public void volcanoShower(float xWhere) {
            Instantiate(volcanoPrefab, new Vector3(xWhere, -2.263215f, 0), Quaternion.identity);
        }

        public void spawnVodka(Vector2 where, Vector2 initialVelocity) {
            GameObject vodka = Instantiate(vodkaPrefab, where, Quaternion.identity);

            vodka.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }

        public void spawnGrass(Vector2 where, Vector2 initialVelocity) {
            GameObject grassBale = Instantiate(grassPrefab, where, Quaternion.identity);

            grassBale.GetComponent<Rigidbody2D>().velocity = initialVelocity;
        }
    }
}