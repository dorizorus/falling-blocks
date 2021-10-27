using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    private static float secondsToMaxDifficulty = 30;

    // scales the difficulty (spawn timer, size, speed etc of falling blocks) as time passes since the level loaded
    public static float GetDifficultyPercent()
    {
    
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
