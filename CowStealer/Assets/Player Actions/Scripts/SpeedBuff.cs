using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedDemon")]

public class SpeedBuff : PowerupEffect
{
    public float newSpeed = 30;

    public override void EnablePowerup() {
        base.EnablePowerup();
        targetVar.GetComponent<Movement>().speed = newSpeed;
        targetVar.GetComponent<Movement>().normalSpeed = newSpeed;
        targetVar.GetComponent<Movement>().maxSpeed = newSpeed;
    } 

    public override void DisablePowerup() {
        base.DisablePowerup();
        targetVar.GetComponent<Movement>().speed = 80;
        targetVar.GetComponent<Movement>().normalSpeed = 80;
        targetVar.GetComponent<Movement>().maxSpeed = 80;
    }
}
