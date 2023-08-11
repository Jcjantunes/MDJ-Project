using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3SpecialAttack : MonoBehaviour
{
    public double vodkaChance = 1 / (50.0 * 3);
    public double grassChance = 1 / (50.0 * 2);
    public int minVodka = 1;
    public int maxVodka = 3;
    public int minGrass = 2;
    public int maxGrass = 5;
    public float minSpeed = 2;
    public float maxSpeed = 4;

    public int specialAbilityFlag = 0;

    public Vector2 spawnPosition;

    public GameObject grassPrefab;
    public GameObject vodkaPrefab;

    public void specialAttackAbility()
    {
        specialAbilityFlag = 1;
    }

    public void cancelSpecialAttackAbility()
    {
        specialAbilityFlag = 0;
    }

    public Vector2 getRandomVelocity() {
        Vector2 vector = new Vector2(RandomUtils.floatBetween(-1, 0), RandomUtils.floatBetween(0, 1));
        
        vector.Normalize();
        vector *= RandomUtils.floatBetween(minSpeed, maxSpeed);
        return vector;
    }

    public void spawnVodka(Vector2 where, Vector2 initialVelocity) {
            GameObject vodka = Instantiate(vodkaPrefab, where, Quaternion.identity);

            vodka.GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }

    public void spawnGrass(Vector2 where, Vector2 initialVelocity) {
        GameObject grassBale = Instantiate(grassPrefab, where, Quaternion.identity);

        grassBale.GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }

    void FixedUpdate() {
        if(specialAbilityFlag == 1) {
            if (RandomUtils.odds(vodkaChance)) {
                spawnVodka(spawnPosition, getRandomVelocity());
            }

            if (RandomUtils.odds(grassChance)) {
                spawnGrass(spawnPosition, getRandomVelocity());
            }
        }
    }
}
