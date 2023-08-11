using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1SpecialAttack : FarmerAttacks
{
    public int specialAbilityFlag = 0;
    public Vector3 mousePosition3DVS;
    public Vector2 mousePositionVS;

    public bool allowfireVS = false;
    public float fireRateVS = 1.5f;

    public void specialAttackAbility() {
        specialAbilityFlag = 1;
        allowfireVS = true;
    }

    public void cancelSpecialAttackAbility() {
        specialAbilityFlag = 0;
    }

    public IEnumerator actVS(Vector2 mousePos) {
        allowfireVS = false;            
        spawnHay();
        
        Vector2 direction = mousePos - body.position;
        
        FarmerOnThrown();
        throwHay(direction * _speed);
        
        yield return new WaitForSeconds(fireRateVS);
        allowfireVS = true;
    }

    void FixedUpdate() {

        mousePosition3DVS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositionVS = new Vector2(mousePosition3DVS.x, mousePosition3DVS.y);

        if(specialAbilityFlag == 1) {
            if(allowfireVS) {
                StartCoroutine(actVS(mousePositionVS));
            }
        }
    }
}
