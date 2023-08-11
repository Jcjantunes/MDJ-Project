using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackMeter : MonoBehaviour
{
    public Text barText;
    public Image barMeter;
    public Image[] barPoints;

    public static float fill, fillMin = 0, fillMax = 100;

    float lerpSpeed;

    private void Start() {
        fill = 0;
    }

    private void Update() {
        barText.text = "Special Attack: " + fill + "%";

        if(fill > fillMax) {
            fill = fillMax;
        }

        lerpSpeed = 3f * Time.deltaTime;

        BarFiller();
        //ColorChanger();
    }

    void BarFiller() {
        //barMeter.fillAmount = Mathf.Lerp(barMeter.fillAmount, (fill/fillMax), lerpSpeed);

        for (int i = 0; i < barPoints.Length; i++) {
            barPoints[i].enabled = !DisplayBarPoint(fill, i);
        }
    }

    void ColorChanger() {
        Color barMeterColor = Color.Lerp(Color.red, Color.green, (fill/fillMax));

        barMeter.color = barMeterColor;
    }

    bool DisplayBarPoint(float barFill, int pointNumber) {
        return((pointNumber * 10) >= barFill);
    }

    public static void ResetBar() {
        if(fill > 0) {
            fill = fillMin;
        }
    }

    public static void FillBar(float fillPoints) {
        if(fill < fillMax) {
            fill += fillPoints;
        }
    }
}
