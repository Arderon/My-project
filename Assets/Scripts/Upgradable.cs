using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    public int level = 0;
    public int maxLevel;
    public string[] upgradesText = new string[10];

    public void PrintLevel()
    {
        Debug.Log(level);
    }

    public string GetUpgradeText()
    {
        return upgradesText[level];
    }

    public virtual void Upgrade()
    {

    }
}
