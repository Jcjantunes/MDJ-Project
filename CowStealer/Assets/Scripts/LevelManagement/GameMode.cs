using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMode
{
    public static bool isVersus = false;
    public static bool runningLevel = false;
    public static LevelInfo[] levels = { new LevelInfo(0, false, true, 0, new int[1]{1}), 
        new LevelInfo(1, false, false, 0, new int[1]{2}), 
        new LevelInfo(2, false, false, 0, new int[1]{3}), 
        new LevelInfo(3, false, false, 0, new int[1]{4}), 
        new LevelInfo(4, false, false, 0, new int[1]{-1}) 
    };

    public static int GetLevelScore(int levelId)
    {
        return levels[levelId].tier;
    }

    public static int GetUFOScore(string levelName)
    {
        return levels[TranslateLevelName(levelName)].ufoScore;
    }

    public static int GetFarmerScore(string levelName)
    {
        return levels[TranslateLevelName(levelName)].farmerScore;
    }

    public static void SetLevelScore(int level, int score)
    {
        if(levels[level].tier < score) levels[level].tier  = score;
    }

    public static void UnlockLevel(int levelId)
    {
        if(levelId >= 0)
            levels[levelId].unlocked = true;
    }

    public static void CompleteLevel(int levelId)
    {
        levels[levelId].completed = true;
        foreach(var level in levels[levelId].unlocks)
            UnlockLevel(level);
    }

    public static int TranslateLevelName(string name)
    {
        var id = name.Split("_");
        return int.Parse(id[1]) - 1;
    }

    public static bool IsUnlocked(string levelName)
    {
        return levels[TranslateLevelName(levelName)].unlocked;
    }

    public static int GetFinalScore(){
        int score = 0;

        foreach(var level in levels){
            score += level.tier;
        }

        return score;
    }

    public static void SetFarmerScore(int level, int score)
    {
        if(levels[level].farmerScore < score) levels[level].farmerScore = score;
    }

    public static void SetUFOScore(int level, int score)
    {
        if(levels[level].ufoScore < score) levels[level].ufoScore = score;
    }
}
