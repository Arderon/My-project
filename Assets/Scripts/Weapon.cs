using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : Upgradable
{
    public GameObject bulletPrefab;
    public float reload;
    public float speed;
    public int damage;
    public float power { get; set;}

    public int ApplyPower()
    {
        return (int)Math.Round(damage * (1 + power / 100));
    }
}
