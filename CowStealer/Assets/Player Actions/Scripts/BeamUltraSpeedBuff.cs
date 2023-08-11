using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/BeamUtraSpeed")]

public class BeamUltraSpeedBuff : PowerupEffect
{
    public float newSpeed = 11;

    public GameObject[] cows;

    public override void EnablePowerup() {
        base.EnablePowerup();
        cows = GameObject.FindGameObjectsWithTag("Cow");
        
        foreach (GameObject cow in cows)
        {
            cow.GetComponent<CowAbductee>().moveSpeed = newSpeed;
        }
    } 

    public override void DisablePowerup() {
        base.DisablePowerup();
        cows = GameObject.FindGameObjectsWithTag("Cow");
        
        foreach (GameObject cow in cows)
        {
            cow.GetComponent<CowAbductee>().moveSpeed = 5;
        }
    }
}
