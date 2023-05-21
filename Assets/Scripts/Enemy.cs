using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private XpBarScript xpBarScript;
    private GameObject player;
    private EnemySpawnManager spawnManagerScript;
    private Vector2 playerPos;
    private float speed;
    public int maxHealth;
    public int currentHealth;
    void Start()
    {
        spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<EnemySpawnManager>();
        xpBarScript = GameObject.Find("XpBar").GetComponent<XpBarScript>();
        SetHealth();
        speed = Random.Range(1.2f, 2.2f);
        player = GameObject.Find("Player");
         
    }

    void Update()
    {
        Move();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void SetHealth()
    {
        maxHealth = 100 + (int)(Time.time / 5);
        currentHealth = maxHealth;
    }

    public int OutcomeDamage()
    {
        return 5 + (int)(maxHealth/50);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
            Destroy(other.gameObject);
        }else if(other.gameObject.tag == "Satellite")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(damage);
    }

    private void Die()
    {
        Destroy(gameObject);
        spawnManagerScript.enemies.Remove(gameObject);
        if (PlayerControlls.level != GameManager.Instance.maxLevel)
        {
            xpBarScript.AddXp(20);
        }
    }

    private void Move()
    {
        playerPos = player.transform.position;
        /*transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);*/
        gameObject.GetComponent<Rigidbody2D>().velocity = (playerPos - (Vector2)gameObject.transform.position).normalized * speed;
    }
}
