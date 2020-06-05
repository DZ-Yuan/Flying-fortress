using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shield : MonoBehaviour
{
    public float health;
    public float max_health = 60f;

    public Animation low_health;

    private void Awake()
    {
        health = max_health;
    }

    void Start()
    {
        low_health = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        if (health <= 50 && health > 1)
        {
            low_health.Play();

        }

        if (health <= 0)
        {
            low_health.Play("Shield_broken");
            
            Destroy(gameObject, 0.2f);
        }
    }

    public void getDamage(int damge)
    {
        health -= damge / 2;
    }
}
