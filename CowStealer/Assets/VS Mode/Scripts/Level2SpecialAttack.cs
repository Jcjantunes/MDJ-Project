using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Level2SpecialAttack : MonoBehaviour
{
    public int maxAnimals = 6;
    GameObject[] animalSpawns;
    public GameObject[] animalPrefabs;
    public float animalAfraidChance = 0.1f;
    float speed = 0.0f;
    bool spawn = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn();
        HandleAnimals();
    }

    void Start()
    {
        animalSpawns = new GameObject[maxAnimals];

        for (int i = 0; i < maxAnimals; i++)
            animalSpawns[i] = null;
    }

    public void Execute(float scrollSpeed)
    {
        speed = scrollSpeed;
        spawn = true;
    }

    void Spawn()
    {
        if (spawn)
        {
            for (int i = 0; i < maxAnimals; i++)
            {
                if (animalSpawns[i] == null)
                {
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
                    animal.transform.position = new Vector3(Random.Range(-8f, 10f), 4f, 0);
                    animalSpawns[i] = animal;

                    if (type < 2)
                    {
                        animal.GetComponent<AnimalDontCare>().enabled = false;
                    }
                    else
                    {
                        animal.GetComponent<AnimalAfraid>().enabled = false;
                    }
                }
            }
        }

        spawn = false;
    }

    void HandleAnimals()
    {
        for (int i = 0; i < maxAnimals; i++)
        {
            if (animalSpawns[i] == null || animalSpawns[i].transform.position.y > -2f) continue;

            if (animalSpawns[i].GetComponent<AnimalDontCare>() != null) animalSpawns[i].GetComponent<AnimalDontCare>().enabled = true;
            else animalSpawns[i].GetComponent<AnimalAfraid>().enabled = true;

            if (animalSpawns[i].transform.position.x < -10f)
            {
                Destroy(animalSpawns[i], 5);
                animalSpawns[i] = null;
            }
            else
            {
                animalSpawns[i].transform.position = new Vector3(animalSpawns[i].transform.position.x - (1 * speed * Time.deltaTime), animalSpawns[i].transform.position.y, 0);
            }
        }
    }
}
