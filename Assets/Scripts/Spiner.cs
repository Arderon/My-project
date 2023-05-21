using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spiner : Weapon
{
    private float duration;
    private float radius;
    private int amount;
    public GameObject pivotPoint;

    private bool isReloading = false;
    private bool isWorking = false;
    private Vector3 playerLastPos;
    private List<GameObject> satelites = new List<GameObject>();


    public void SpawnSatelites()
    {
        Vector2 spawnDirection = Vector2.right;
        for (int i = 0; i < amount; i++)
        {
            Vector2 spawnPoint = (Vector2)transform.position + (spawnDirection * radius);
            GameObject satelite = Instantiate(bulletPrefab, spawnPoint, transform.rotation);
            satelite.GetComponent<Bullet>().SetDamage(ApplyPower());
            satelites.Add(satelite);
            spawnDirection = Quaternion.AngleAxis(360 / amount, Vector3.forward) * spawnDirection;
        }
    }

    public void MoveSatelities()
    {
        foreach (var s in satelites)
        {
            s.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity + (Vector2)(gameObject.transform.position - playerLastPos);
            s.transform.RotateAround(pivotPoint.transform.position, Vector3.forward, speed);
            playerLastPos = gameObject.transform.position;
        }
    }

    private void DeleteSatelities()
    {
        foreach (var s in satelites)
        {
            Destroy(s.gameObject);
        }
        satelites.Clear();
    }

    private void Start()
    {
        InitializeUpgradeText();
        maxLevel = upgradesText.Length;
        level = 0;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        if (level == 0) return;

        if (!isReloading)
        {
            StartCoroutine("SpawnAndReload");
        }
        else if(isWorking)
        {
            MoveSatelities();
        }
    }

    

    IEnumerator SpawnAndReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reload);
        SpawnSatelites();
        isWorking = true;
        yield return new WaitForSeconds(duration);
        DeleteSatelities();
        isReloading = false;
        isWorking = false;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        level += 1;
        switch (level)
        {
            case 1:
                damage = 40;
                reload = 5f;
                playerLastPos = gameObject.transform.position;
                duration = 5f;
                radius = 2f;
                amount = 1;
                speed = 0.3f;
                break;
            case 2:
                damage += 5;
                break;
            case 3:
                speed += 0.15f;
                break;
            case 4:
                amount += 1;
                break;
            case 5:
                radius += 1;
                break;
            case 6:
                amount += 1;
                break;
            case 7:
                speed += 0.15f;
                break;
            case 8:
                duration += 1;
                break;
            case 9:
                amount += 2;
                break;
            case 10:
                speed += 0.15f; //replace by slow enemies
                break;
        }
        ApplyPower();
/*      Debug.Log("Spiner - damage = " + damage);
        Debug.Log("Spiner - speed = " + speed);
        Debug.Log("Spiner - reload = " + reload);*/
    }

    private void InitializeUpgradeText()
    {
        upgradesText[0] = "New weapon - Spiner";
        upgradesText[1] = "Spiner - damage+";
        upgradesText[2] = "Spiner - speed+";
        upgradesText[3] = "Spiner - satelites+";
        upgradesText[4] = "Spiner - radius+";
        upgradesText[5] = "Spiner - satelites+";
        upgradesText[6] = "Spiner - speed+";
        upgradesText[7] = "Spiner - duration+";
        upgradesText[8] = "Spiner - satelites+";
        upgradesText[9] = "Spiner - satelites strongly slow enemyes";
    }
}
