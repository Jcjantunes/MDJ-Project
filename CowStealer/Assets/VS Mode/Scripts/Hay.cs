using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hay : UFODestroyer
{
    public int collisionFlag = 0;
    public GameObject[] cows;

    public static event Action OnHit;

    private BoxCollider2D hayCollider, topCollider, downCollider, leftCollider, rightCollider, noFlyCollider, cowCollider;
    private GameObject topLimit, downLimit, leftLimit, rightLimit, noFlyLimit;

    public override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.name == "ufo"){
            OnHit?.Invoke();

            if(FarmerAttacks.getSpecialAbilityFlag() == 0) {
                SpecialAttackMeter.FillBar(20f);
                    
                if(meterLevel < 100) {
                    meterLevel += 20;
                }
            }
        }
    }
}
