using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : LevelEvent
{
    public int maxAnimalsPerSection = 5;
    public GameObject[] animalPrefabs;
    GameObject[] animalSpawns;
    public float animalAfraidChance = 0.1f;

    void Start(){
        animalSpawns = new GameObject[maxAnimalsPerSection];

        for(int i = 0; i < maxAnimalsPerSection; i++)
            animalSpawns[i] = null;
    }

    public override void Execute(float scrollSpeed){
        Spawn();
        HandleAnimals(scrollSpeed);
    }

    void Spawn(){
        for(int i = 0; i < maxAnimalsPerSection; i++){
            if(animalSpawns[i] == null){
                float chance = Random.Range(0f, 1f);
                var type = 0;

                if (chance > animalAfraidChance)
                {
                    type = Random.Range(0, 10) % 2;
                }
                else
                {
                    type = (Random.Range(0, 10) % 2) + 2;
                }

                var animal = Instantiate(animalPrefabs[type]);
                animal.transform.position = new Vector3(Random.Range(15f, 30f), animal.transform.position.y, 0);
                animalSpawns[i] = animal;
            }
        }
    }

    void HandleAnimals(float scrollSpeed){
        for(int i = 0; i< maxAnimalsPerSection; i++){
            if(animalSpawns[i] == null) continue;
            if(animalSpawns[i].transform.position.x < -10f){
                Destroy(animalSpawns[i], 5);
                animalSpawns[i] = null;
            }
            else {
                animalSpawns[i].transform.position = new Vector3(animalSpawns[i].transform.position.x - (1*scrollSpeed*Time.deltaTime), animalSpawns[i].transform.position.y, 0);
            }
        }
    }
}
