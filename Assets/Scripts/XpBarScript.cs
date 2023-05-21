using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBarScript : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        SetMaxXp(100);
    }

    void Update()
    {
        if (slider.maxValue == slider.value)
        {
            GameManager.Instance.LevelUp();
        }
    }

    public void SetMaxXp(int xp)
    {
        slider.maxValue = xp;
        SetXp(0);
    }

    public void AddXp(int xp)
    {
        slider.value += xp;
    }

    public void SetXp(int xp)
    {
        slider.value = xp;
    }
}
