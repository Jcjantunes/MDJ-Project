using UnityEngine;

namespace Environment.stuff {
    public class Fireball : UFODestroyer {

        public GameObject lavaPrefab;

        private AudioSource _collisionSound;
        private AudioSource _fireCrackling;

        public override void Start() {
            base.Start();

            _collisionSound = GetComponents<AudioSource>()[0];
            _fireCrackling = GetComponents<AudioSource>()[1];
        }

        public override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);

            if (collision.gameObject.name == "Floor") {
                // Collided with floor
                Instantiate(lavaPrefab, transform.position, Quaternion.identity);
                GameObject explosion = Instantiate(_explosionPrefab, collision.GetContact(0).point, Quaternion.identity);
                explosion.transform.localScale *= 0.6f;
            }

            if (collision.gameObject.name is "Floor" or "ufo") {
                _fireCrackling.Stop();
                _collisionSound.Play();

                transform.position = Vector3.up * 99999f;
                
                Destroy(gameObject, _collisionSound.clip.length + 5);
            }
        }
    }
}