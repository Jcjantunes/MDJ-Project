using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using DefaultNamespace;

namespace LevelManagement
{
public class LevelManager : MonoBehaviour
    {
        [Header("Objects")]
        public Timer timer;
        public Objective objective;
        public GameObject grid;
        public GameObject tilePrefab;
        public GameObject cowPrefabDontCare;
        public GameObject cowPrefabAfraid;
        public GameObject[] powerupPrefabs = new GameObject[2];
        public LevelEvent levelEvent;
        public GameObject startPanel;
        public AudioSource audioSource;
        public Movement playerMovement;
        public GameObject farmer;
        public LoadingScreen loadingScreen;

        int nrCows = 0;

        [Header("Specs")]
        public int minNumberOfCows = 15;
        public int tierOneCows = 5;
        public int tierTwoCows = 10;
        public int maxNumberOfCowsPerSection = 3;
        public float scrollSpeed = 50f;
        public float powerupTimer = 20f;
        public int levelId = 0;
        public float afraidChance = 0.5f;
        
        public List<Lava> lavas = new List<Lava>();


        Vector3 startPosition;
        float loopPos = -17.73f;
        GameObject[] cowSpawns;
        GameObject puSpawn = null;
        float lastPowerup = 0f;
        public bool hasStarted = false;
        bool levelFinished = false;
        

        void Start()
        {
            startPosition = grid.transform.position;
            cowSpawns = new GameObject[maxNumberOfCowsPerSection];
            startPanel.SetActive(true);

            for (int i = 0; i < maxNumberOfCowsPerSection; i++) {
                cowSpawns[i] = null;
            }

            lastPowerup = timer.GetDuration();
            if (playerMovement != null) playerMovement.enabled = false;
            if (farmer != null) {
                if (GameMode.isVersus)
                    farmer.GetComponent<FarmerAttacks>().enabled = false;
                else
                    farmer.GetComponent<FarmerSimple>().enabled = false;
            }
            loadingScreen.FadeOut();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("space") && loadingScreen.enabled && !loadingScreen.isLoading)
            {
                if (!hasStarted)
                {
                    hasStarted = true;
                    GameMode.runningLevel = true;
                    SpawnCows();
                    startPanel.SetActive(false);
                    timer.StartTimer();
                    if (audioSource != null) audioSource.Play();
                    if (playerMovement != null) playerMovement.enabled = true;
                    if (farmer != null) {
                        if (GameMode.isVersus)
                            farmer.GetComponent<FarmerAttacks>().enabled = true;
                        else
                            farmer.GetComponent<FarmerSimple>().enabled = true;
                    }
                }

            }

            if(Input.GetKeyDown("return")){
                if (levelFinished)
                {
                    // Go to selection menu TODO
                    loadingScreen.LoadScene("UI - Level Selection");
                }
            }

            if (hasStarted && timer.HasFinished())
            {
                GameMode.runningLevel = false;

                if (playerMovement != null) playerMovement.enabled = false;

                if (farmer != null) {
                    if (GameMode.isVersus)
                        farmer.GetComponent<FarmerAttacks>().enabled = false;
                    else
                        farmer.GetComponent<FarmerSimple>().enabled = false;
                }

                objective.DisplayScore();
                objective.SaveScore(levelId);
                levelFinished = true;
            }

            if (!timer.HasFinished())
            {
                if (hasStarted && !GameMode.isVersus)
                {
                    if (!playerMovement.gameObject.activeInHierarchy)
                    {
                        farmer.GetComponent<FarmerSimple>().enabled = false;
                    }
                    else
                    {
                        farmer.GetComponent<FarmerSimple>().enabled = true;
                    }
                }

                if (hasStarted && GameMode.isVersus)
                {
                    if (!playerMovement.gameObject.activeInHierarchy)
                    {
                        farmer.GetComponent<FarmerAttacks>().enabled = false;
                    }
                    else
                    {
                        farmer.GetComponent<FarmerAttacks>().enabled = true;
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (hasStarted && !timer.HasFinished()) {

                if (grid.transform.position.x < loopPos) {
                    grid.transform.position = startPosition;
                }

                SpawnCows();
                SpawnPowerup();
                HandleCows();
                HandlePowerup();
                HandleLevelEvent();
                HandleLavas();
                grid.transform.position = new Vector3(grid.transform.position.x - (1 * scrollSpeed * Time.deltaTime), grid.transform.position.y, 0);

            }

        }

        void HandleLevelEvent() {
            if (levelEvent != null)
                levelEvent.Execute(scrollSpeed);
        }

        void SpawnPowerup() {
            float upperBound = 4.5f;
            float lowerBound = 0.55f;

            float currentTime = timer.GetCurrent();

            if (Mathf.Abs(lastPowerup - currentTime) >= powerupTimer && puSpawn == null) {
                int idx = Random.Range(0, 10);
                idx = idx % 2;
                puSpawn = Instantiate(powerupPrefabs[idx]);
                puSpawn.transform.position = new Vector3(15, Random.Range(lowerBound, upperBound), 0);
                lastPowerup = currentTime;
            }
        }

        void HandlePowerup() {
            if (puSpawn != null) {
                if (puSpawn.transform.position.x < -10f) {
                    //Destroy(puSpawn);
                    puSpawn = null;
                }
                else {
                    puSpawn.transform.position = new Vector3(puSpawn.transform.position.x - (1 * scrollSpeed * Time.deltaTime), puSpawn.transform.position.y, 0);
                }
            }
        }

        void SpawnCows() {
            int j;
            for (j = 0; j < maxNumberOfCowsPerSection; j++) {
                if (cowSpawns[j] != null) break;
            }

            int type = 0;
            float prob = Random.Range(0f, 1f);

            if (prob < afraidChance)
                type = 1;

            if (j == maxNumberOfCowsPerSection && nrCows >= minNumberOfCows) {
                int count = Random.Range(maxNumberOfCowsPerSection / 2, maxNumberOfCowsPerSection);

                for (int i = 0; i < count; i++) {
                    GameObject prefab = null;
                    if (type == 0)
                        prefab = cowPrefabDontCare;
                    else
                        prefab = cowPrefabAfraid;

                    var cow = Instantiate(prefab);
                    cow.tag = "Cow";
                    cow.transform.position = new Vector3(Random.Range(15f, 30f), cow.transform.position.y, 0);
                    cowSpawns[i] = cow;

                    prob = Random.Range(0f, 1f);

                    if (prob < afraidChance)
                        type = 1;
                    else
                        type = 0;

                    nrCows++;
                }
            }

            else if (nrCows < minNumberOfCows) {
                for (int i = 0; i < maxNumberOfCowsPerSection; i++) {
                    if (cowSpawns[i] != null) continue;
                    GameObject prefab = null;
                    if (type == 0) {
                        prefab = cowPrefabDontCare;
                        prefab.GetComponent<AnimalDontCare>().enabled = true;
                    }
                    else {
                        prefab = cowPrefabAfraid;
                        prefab.GetComponent<AnimalAfraid>().enabled = true;
                    }

                    var cow = Instantiate(prefab);
                    cow.tag = "Cow";
                    cow.transform.position = new Vector3(Random.Range(15f, 30f), cow.transform.position.y, 0);
                    cowSpawns[i] = cow;
                    nrCows++;

                    prob = Random.Range(0f, 1f);

                    if (prob < afraidChance)
                        type = 1;
                    else
                        type = 0;
                }
            }
        }

        void HandleCows() {
            for (int i = 0; i < maxNumberOfCowsPerSection; i++) {
                if (cowSpawns[i] == null) continue;
                if (cowSpawns[i].transform.position.x < -10f) {
                    Destroy(cowSpawns[i], 5);
                    cowSpawns[i] = null;
                }
                else {
                    cowSpawns[i].transform.position = new Vector3(cowSpawns[i].transform.position.x - (1 * scrollSpeed * Time.deltaTime), cowSpawns[i].transform.position.y, 0);
                }
            }
        }

        void HandleLavas() {
            lavas.RemoveAll(lava => lava == null);

            foreach (Lava lava in lavas) {
                lava.transform.position = new Vector3(lava.transform.position.x - (1 * scrollSpeed * Time.deltaTime), lava.transform.position.y, 0);
            }
        }
    }
}