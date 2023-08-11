using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using AI;

public abstract class UFODestroyer : UFOInteractions
{
    protected GameObject UFO;
    protected GameObject BULLY;

    private bool _ufoRecentlyHit = false;

    private static GameObject _deadUfoPrefab;
    protected static GameObject _explosionPrefab;
    
    public virtual float deSpawn{
        get {
            return .01f;
        } 
        set {

        }
    }

    public virtual float reSpawn{
        get {
            return 3f;
        } 
        set {

        }
    }
    
    public virtual void Start() {
        UFO = GameObject.Find("ufo");
        BULLY = GameObject.Find("bully");

        if (_deadUfoPrefab == null) {
            _deadUfoPrefab = Resources.Load<GameObject>("dead_ufo");
            _explosionPrefab = Resources.Load<GameObject>("effects/explosion");
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        /*Debug.Log("aqui");
        collisionFlag = 1;*/

        if(collision.gameObject.name == "ufo"){
            if (!_ufoRecentlyHit) {
                _ufoRecentlyHit = true;
                StartCoroutine(Respawn(deSpawn, reSpawn, collision.GetContact(0).point));
            }
        }
        else if (collision.gameObject.name == "bully") {
            GameObject bully = collision.gameObject;
            
            // Ignoring the following collisions
            BoxCollider2D boxCollider2D = bully.GetComponent<BoxCollider2D>();
            PolygonCollider2D polygonCollider2D = bully.GetComponent<PolygonCollider2D>();
            CircleCollider2D abductCollider2D = bully.GetComponentInChildren<CircleCollider2D>();
            BoxCollider2D destroyerCollider2D = GetComponent<BoxCollider2D>();
            
            Physics2D.IgnoreCollision(boxCollider2D, destroyerCollider2D);
            Physics2D.IgnoreCollision(polygonCollider2D, destroyerCollider2D);
            Physics2D.IgnoreCollision(abductCollider2D, destroyerCollider2D);
            
            // Restoring velocity and position to pre-collision values
            BullyBehaviour bullyBehaviour = bully.GetComponent<BullyBehaviour>();
            Rigidbody2D bullyBody = bully.GetComponent<Rigidbody2D>();

            bullyBody.velocity = bullyBehaviour.lastVelocity;
            bullyBody.position = bullyBehaviour.lastPosition;
            
            // Making the bully fade in and out when colliding with a destroyer
            if (!bullyBehaviour.recentlyHit) {
                bullyBehaviour.recentlyHit = true;
                StartCoroutine(fadeSlightly(bullyBehaviour, bully.GetComponent<SpriteRenderer>()));
            }
        }

        /*else if(collision.gameObject.name == "Floor") {
            Destroy(gameObject);
        }*/
        //StartCoroutine(Respawn(0.1f, 0.1f));
    }

    IEnumerator Respawn(float timeToDespawn, float timeToRespawn, Vector2 contact) {
        
        if(UFO != null) {
            // Explosion
            GameObject explosion = Instantiate(_explosionPrefab, contact, Quaternion.identity);
            
            yield return new WaitForSeconds(timeToDespawn);
            UFO.SetActive(false);

            GameObject deadUFO = Instantiate(_deadUfoPrefab, UFO.transform.position, UFO.transform.rotation);
            DeadUFO deadUFOScript = deadUFO.GetComponent<DeadUFO>();

            deadUFOScript.secondsToDisappear = timeToRespawn;
    
            yield return new WaitForSeconds(timeToRespawn);
            UFO.SetActive(true);
            _ufoRecentlyHit = false;
        }
    }

    public IEnumerator fadeSlightly(BullyBehaviour bullyBehaviour, SpriteRenderer renderer) {
        Color fadeInPerTick = new Color(0, 0, 0, 0.5f / 50f);
        Color fadeOutPerTick = new Color(0, 0, 0, -2f / 50f);

        while (renderer.color.a > 0.5) {
            yield return new WaitForFixedUpdate();

            renderer.color += fadeOutPerTick;
        }

        while (renderer.color.a < 1) {
            renderer.color += fadeInPerTick;
                
            yield return new WaitForFixedUpdate();
        }

        bullyBehaviour.recentlyHit = false;
    }
}
