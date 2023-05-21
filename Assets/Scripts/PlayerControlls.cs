using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public HealthBar healthBar;
    public XpBarScript xpBar;
    public float speed = 1.7f;
    public int maxHealth = 200;
    public int currentHealth;

    private Rigidbody2D rb;
    public static int level = 1;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        Move();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void LevelUp()
    {
        level++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().OutcomeDamage());
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }

/*    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timer)
        {
            damageColisionTimer -= Time.deltaTime;
            if (damageColisionTimer < 0)
            {
                TakeDamage(5);
                damageColisionTimer = 1f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        timer = false;
        damageColisionTimer = 1;
    } */
}
