using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    [SerializeField]
    public GameObject _artToDisable = null;

    public Collider2D _collider;

    void Awake() {
        _collider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "UFO") {
            _collider.enabled = false;
            _artToDisable.SetActive(false);
            StartCoroutine(powerupEffect.Apply(collision.gameObject, gameObject));
        }
    }
}
