using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public Animator scoreAnimator;
    public int levelId;


    public void DisplayScore(){
        var score = GameMode.GetLevelScore(levelId);
        scoreAnimator.SetInteger("Tier", score);
    }

    void Update()
    {
        DisplayScore();
    }
}
