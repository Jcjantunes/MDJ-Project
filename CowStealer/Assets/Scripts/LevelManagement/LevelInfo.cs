using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int id = 0;
    public bool completed = false;
    public bool unlocked = false;
    public int tier = 0;
    public int[] unlocks;
    public int ufoScore = 0;
    public int farmerScore = 0;

    public LevelInfo(int _id,  bool _completed, bool _unlocked, int _tier, int[] _unlocks)
    {
        id = _id;
        completed = _completed;
        unlocked = _unlocked;
        tier = _tier;
        unlocks = _unlocks;
    }
}
