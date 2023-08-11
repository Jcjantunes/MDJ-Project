using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HitCounter : MonoBehaviour
{
    
    public Text hitText;
    public int goal = 5;
    int hitCount = 0;
    
    void Start() {
        hitText.text = $"0 / {goal}";
    }

    private void OnEnable() {
        Hay.OnHit += IncrementCowCount;
    }

    private void OnDisable() {
        Hay.OnHit -= IncrementCowCount;
    }

    public void IncrementCowCount() {
        hitCount++;
        hitText.text = $"{hitCount} / {goal}";
    }

}
