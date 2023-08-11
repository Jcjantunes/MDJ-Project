using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public int tierOneCows = 5;
    public int tierTwoCows = 10;
    public int tierThreeCows = 15;

    public Text objectiveText;
    public GameObject scorePanel;
    public Animator scoreAnimator;

    public int tierOneHits = 3;
    public int tierTwoHits = 6;
    public int tierThreeHits = 9;
    public Text objectiveTextFarmer;
    public Animator farmerScoreAnimator;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scorePanel.SetActive(false);
        objectiveText.text = "0 / " + tierOneCows.ToString();
        scoreText.text = "Level Completed";
    }

    public int GetUFOScore()
    {
        var score = objectiveText.text.Split(" ");
        if (int.Parse(score[0]) >= tierThreeCows)
        {
            return 3;
        }
        else if (int.Parse(score[0]) >= tierTwoCows)
        {
            return 2;
        }
        else if (int.Parse(score[0]) >= tierOneCows)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int GetFarmerScore()
    {
        var score = objectiveTextFarmer.text.Split(" ");
        if (int.Parse(score[0]) >= tierThreeHits)
        {
            return 3;
        }
        else if (int.Parse(score[0]) >= tierTwoHits)
        {
            return 2;
        }
        else if (int.Parse(score[0]) >= tierOneHits)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void SaveScore(int levelId)
    {
        if (!GameMode.isVersus)
        {
            var score = GetUFOScore();
            GameMode.SetLevelScore(levelId, score);
            GameMode.SetUFOScore(levelId, score);
            if (score > 0) GameMode.CompleteLevel(levelId);
        } else
        {
            var score = GetUFOScore();
            int farmerScore = GetFarmerScore();
            GameMode.CompleteLevel(levelId);
            GameMode.SetFarmerScore(levelId, farmerScore);
            GameMode.SetUFOScore(levelId, score);
            GameMode.SetLevelScore(levelId, Mathf.Min(score, farmerScore));
        }
    }

    public void DisplayScore(){
        var score = this.GetUFOScore();
        if (score == 0) this.scoreText.text = "Level Failed";
        this.scorePanel.SetActive(true);
        scoreAnimator.SetInteger("Tier", score);


        if (GameMode.isVersus)
        {
            score = this.GetFarmerScore();
            if (score > 0) this.scoreText.text = "Level Completed";
            farmerScoreAnimator.SetInteger("Tier", score);
        }
    }
}
