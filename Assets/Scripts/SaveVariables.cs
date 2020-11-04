using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveVariables
{
    public int enemiesAmount;
    public int levelsUnlocked;

    public SaveVariables(int enemiesAmount, int levelsUnlocked)
    {
        this.enemiesAmount = enemiesAmount;
        this.levelsUnlocked = levelsUnlocked;
    }
}
