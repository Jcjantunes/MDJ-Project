using System;
using UnityEngine;
using Random = System.Random;

public class RandomUtils {
    public static readonly Random random = new Random();

    public static bool odds(double chanceOfWinning) {
        return random.NextDouble() < chanceOfWinning;
    }

    public static double odds() {
        return random.NextDouble();
    }

    /**
     * Returns a value between low (inclusive) and high (exclusive)
     */
    public static double doubleBetween(double low, double high) {
        if (high < low) {
            throw new ArgumentException($"Provided high value ${high} must be higher than the lower value ${low}");
        }
        
        double difference = high - low;

        return random.NextDouble() * difference + low;
    }

    public static float floatBetween(float low, float high) {
        return (float)doubleBetween(low, high);
    }

    public static int intBetween(int low, int high) {
        return random.Next(low, high);
    }

    public static Vector2 randomVector(double length) {
        Vector2 vector = new Vector2((float) doubleBetween(-1, 1), (float) doubleBetween(-1, 1));
        
        vector.Normalize();

        return vector * (float) length;
    }
    
}