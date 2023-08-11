using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CowCounter : MonoBehaviour
{
    
    public Text cowText;
    public int goal = 5;
    int cowCount = 0;
    
    void Start() {
        cowText.text = $"0 / {goal}";
    }

    private void OnEnable() {
        CowAbductee.OnCowAbducted += IncrementCowCount;
    }

    private void OnDisable() {
        CowAbductee.OnCowAbducted -= IncrementCowCount;
    }

    public void IncrementCowCount() {
        cowCount++;
        cowText.text = $"{cowCount} / {goal}";
    }

}
