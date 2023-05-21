using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : Weapon
{
    private GameObject enemySpawnManager;
    void Start()
    {
        InitializeUpgradeText();
        maxLevel = upgradesText.Length;
        level = 0;
        Upgrade();
        enemySpawnManager = GameObject.Find("Spawn Manager");
        
    }

    void Shoot()
    {
        bool enemyOnScreen = false;
        //Create bullet and fire it
        foreach (GameObject enemy in enemySpawnManager.GetComponent<EnemySpawnManager>().enemies)
        {
            if (enemy.GetComponent<Renderer>().isVisible)
            {
                enemyOnScreen = true;
                break;
            }
        }
        if (enemyOnScreen)
        {
            GameObject nearestEnemy = enemySpawnManager.GetComponent<EnemySpawnManager>().GetNearestEnemy();
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetDamage(ApplyPower());
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce((nearestEnemy.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        level += 1;
        switch (level)
        {
            case 1:
                level = 1;
                damage = 100;
                reload = 0.6f;
                speed = 7;
                InvokeRepeating("Shoot", 0, reload);
                break;
            case 2:
                damage += 5;
                break;
            case 3:
                reload -= 0.2f;
                break;
            case 4:
                damage += 5;
                break;
            case 5:
                speed += 5;
                break;
            case 6:
                damage += 5;
                //replace by enemy penetration
                break;
            case 7:
                damage += 5;
                break;
            case 8:
                reload -= 0.2f;
                break;
            case 9:
                damage += 10;
                break;
            case 10:
                damage += 5;
                //replace by all enemy penetration
                break;
        }
/*        Debug.Log("Gun - damage = " + damage);*/
/*        Debug.Log("Gun -speed = " + speed);
          Debug.Log("Gun -reload = " + reload);*/
    }

    private void InitializeUpgradeText()
    {
        upgradesText[0] = "New weapon - Gun";
        upgradesText[1] = "Gun - damage+";
        upgradesText[2] = "Gun - reload-";
        upgradesText[3] = "Gun - damage+";
        upgradesText[4] = "Gun - projectile speed+";
        upgradesText[5] = "Gun - bullets penetrate +2 more enemies";
        upgradesText[6] = "Gun - damage+";
        upgradesText[7] = "Gun - reload-";
        upgradesText[8] = "Gun - damage++";
        upgradesText[9] = "Gun - bullets penetrate all enemies";
    }
}