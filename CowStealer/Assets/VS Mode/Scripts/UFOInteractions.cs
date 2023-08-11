using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UFOInteractions : MonoBehaviour
{

    public static int meterLevel = 0;

    public int rightMouseFlag = 0;

    public static void resetBarLevel() {
        meterLevel = 0;
    }

    public static int getBarLevel() {
        return meterLevel;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            rightMouseFlag = 1;
        }

        if(rightMouseFlag == 1) {
            if(meterLevel == 100) {
                SpecialAttackMeter.ResetBar(); 
            }

            else{
                rightMouseFlag = 0;
            }
        }
    } 
}
