using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelManagement;

public class FarmerAttacks : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject hayPrefab;
    public GameObject currentHay; // Hay currently being held by the farmer. May be null

    public BoxCollider2D farmerCollider;
    public float _speed = 1f;
    
    public int leftMouseFlag = 0;
    public int rightMouseFlag = 0;

    public Vector3 mousePosition3D;
    public Vector2 mousePosition;

    private BoxCollider2D cowCollider;
    public GameObject[] cows;

    public float startDashTime = .1f;
    private float dashTime;

    public bool allowfire = true;
    public static bool allowSpecialAbility = true;

    public static int specialAbilityFlag = 0;

    public static float fireRate = 1.5f;

    public static int level1SpecialAttackFlag = 0;

    public static event Action OnThrow;

    public float specialAbilityTime = 3f;

    public LevelManager manager;
    
    public void Start() {
        body = GetComponent<Rigidbody2D>();
        hayPrefab = Resources.Load<GameObject>("hayVersus");
        farmerCollider = GameObject.Find("farmer").GetComponent<BoxCollider2D>();
        dashTime = startDashTime;
        manager = GameObject.FindObjectOfType<LevelManager>();
    }

    public bool hasHay() {
            return currentHay != null;
        }

    public void spawnHay() {
        if (!hasHay()) {
            currentHay = Instantiate(hayPrefab);
            Vector3 hayPosition = transform.position;

            hayPosition.x -= 1f;
        
            currentHay.transform.position = hayPosition;
            
            BoxCollider2D collider = currentHay.GetComponent<BoxCollider2D>();
            
            Physics2D.IgnoreCollision(collider, farmerCollider);

            cows = GameObject.FindGameObjectsWithTag("Cow");
        
            foreach (GameObject cow in cows)
            {
                cowCollider = cow.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(collider, cowCollider, true);
            }
        }
    }

    public void throwHay(Vector2 velocity) {
        if (hasHay()) {
            Rigidbody2D hayBody = currentHay.GetComponent<Rigidbody2D>();

            hayBody.velocity = velocity;
            hayBody.angularVelocity = 5;

            currentHay = null;
        }
    }

    public IEnumerator act(Vector2 mousePosition) {
        allowfire = false;            
        spawnHay();
        
        Vector2 direction = mousePosition - body.position;
        
        OnThrow?.Invoke(); 
        throwHay(direction * _speed);
        
        yield return new WaitForSeconds(fireRate);
        allowfire = true;
    }

    public void FarmerOnThrown() {
        OnThrow?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(mousePosition3D.x, mousePosition3D.y);
        

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            leftMouseFlag = 1;
        }

        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            rightMouseFlag = 1;
        }  
    }

    public IEnumerator SpecialAbility() {
        allowSpecialAbility = false; 
        specialAbilityFlag = 1;
        switch (manager.levelId)
        {
            case 0:
                GameObject.FindObjectOfType<Level1SpecialAttack>().specialAttackAbility();
                break;
            case 1:
                GameObject.FindObjectOfType<Level2SpecialAttack>().Execute(manager.scrollSpeed);
                break;
            case 2:
                GameObject.FindObjectOfType<Level3SpecialAttack>().specialAttackAbility();
                break;
            case 3:
                break;
            case 4:
                break;
        }

        UFOInteractions.resetBarLevel();
        
        yield return new WaitForSeconds(specialAbilityTime);

        switch (manager.levelId)
        {
            case 0:
                GameObject.FindObjectOfType<Level1SpecialAttack>().cancelSpecialAttackAbility();
                break;
            case 1:
                break;
            case 2:
                GameObject.FindObjectOfType<Level3SpecialAttack>().cancelSpecialAttackAbility();
                break;
            case 3:
                break;
            case 4:
                break;
        }

        allowSpecialAbility = true;
        specialAbilityFlag = 0;
    }

    void FixedUpdate() {
        mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(mousePosition3D.x, mousePosition3D.y);
        
        if(rightMouseFlag == 1) {
            if(UFODestroyer.getBarLevel() == 100) {
                if(allowSpecialAbility) {
                    StartCoroutine(SpecialAbility());
                    rightMouseFlag = 0;
                }
            }
            else {
                rightMouseFlag = 0;
            }
        }

        else if(leftMouseFlag == 1) {
            if(allowfire) {
                StartCoroutine(act(mousePosition));
                leftMouseFlag = 0;
            }
            else {
                leftMouseFlag = 0;
            }
        }
    }

    public static void setFireRate(int value) {
        fireRate = value;
    }

    public static int getSpecialAbilityFlag() {
        return specialAbilityFlag;
    }
}
