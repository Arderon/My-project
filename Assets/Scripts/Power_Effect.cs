using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Effect : Upgradable
{
    private int power;

    private void Start()
    {
        InitializeUpgradeText();
        maxLevel = upgradesText.Length;
        level = 0;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        level += 1;
        switch (level)
        {
            case 1:
                power = 6;
                break;
            case 2:
                power = 12;
                break;
            case 3:
                power = 18;
                break;
            case 4:
                power = 25;
                break;
        }
        SetPower();
    }

    private void SetPower()
    {
        Component[] components = GetComponents(typeof(Weapon));
        foreach (Weapon w in components)
        {
            w.power = power;
        }
    }

    private void InitializeUpgradeText()
    {
        upgradesText = new string[4];
        upgradesText[0] = "Power - All damage +6%";
        upgradesText[1] = "Power - All damage +12%";
        upgradesText[2] = "Power - All damage +18%";
        upgradesText[3] = "Power - All damage +25%";
    }
}
