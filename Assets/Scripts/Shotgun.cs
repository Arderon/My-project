using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    private int buletsNumber = 3;
    private int shootAngle = 90;

    private Rigidbody2D playerRb;
    private Vector2 lastDirection;


    private void Start()
    {
        InitializeUpgradeText();
        maxLevel = upgradesText.Length;
        playerRb = GetComponent<Rigidbody2D>();
        level = 0;
    }

    void Update()
    {

        if (playerRb.velocity != new Vector2(0, 0))
        {
            lastDirection = playerRb.velocity;
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = Quaternion.AngleAxis(-shootAngle / 2, Vector3.forward) * lastDirection;
        for (int i = 0; i < buletsNumber; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetDamage(ApplyPower());
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(shootDirection.normalized * speed, ForceMode2D.Impulse);
            shootDirection = Quaternion.AngleAxis(shootAngle / (buletsNumber - 1), Vector3.forward) * shootDirection;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        level += 1;
        switch (level)
        {
            case 1:
                damage = 100;
                reload = 3f;
                lastDirection = new Vector2(1, 0);
                InvokeRepeating("Shoot", 0, reload);
                break;
            case 2:
                damage += 4;
                break;
            case 3:
                reload -= 0.2f;
                break;
            case 4:
                damage += 4;
                break;
            case 5:
                buletsNumber += 2;
                break;
            case 6:
                damage += 4;
                break;
            case 7:
                speed += 3;
                //replace by size
                break;
            case 8:
                buletsNumber += 2;
                break;
            case 9:
                buletsNumber += 2;
                break;
            case 10:
                damage += 5;
                //replace by instantly enemy kill
                break;
        }
        ApplyPower();
/*        Debug.Log("Shootgun - damage = " + damage);
        Debug.Log("Shootgun - speed = " + speed);
        Debug.Log("Shootgun - reload = " + reload);*/
    }

    private void InitializeUpgradeText()
    {
        upgradesText[0] = "New weapon - Shotgun";
        upgradesText[1] = "Shootgun - damage+";
        upgradesText[2] = "Shootgun - reload-";
        upgradesText[3] = "Shootgun - damage+";
        upgradesText[4] = "Shootgun - bullets+";
        upgradesText[5] = "Shootgun - damage+";
        upgradesText[6] = "Shootgun - bullets size+";
        upgradesText[7] = "Shootgun - bullets+";
        upgradesText[8] = "Shootgun - bullets+";
        upgradesText[9] = "Shootgun - bullets kill imidiatly";
    }
}