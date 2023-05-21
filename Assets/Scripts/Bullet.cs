using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public int damage;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if ((transform.position - player.transform.position).magnitude > 50)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int d)
    {
        damage = d;
    }
}
//for test