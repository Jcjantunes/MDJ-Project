using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PhantomUFO")]

public class PhantomBuff : PowerupEffect
{
    // Start is called before the first frame update
    public override void EnablePowerup() {
        //targetVar.GetComponent<PolygonCollider2D>().enabled = false;
        base.EnablePowerup();
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 10, true);
        Physics2D.IgnoreLayerCollision(6, 11, true);
        Physics2D.IgnoreLayerCollision(6, 13, true);
    } 

    public override void DisablePowerup() {
        //targetVar.GetComponent<PolygonCollider2D>().enabled = true;
        base.DisablePowerup();
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
        Physics2D.IgnoreLayerCollision(6, 11, false);
        Physics2D.IgnoreLayerCollision(6, 13, false);
    }
}
