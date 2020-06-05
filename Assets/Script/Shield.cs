using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float health;
    public float max_health = 50f;

    private void Awake()
    {
        GameGlobal.shield = this;
        health = max_health;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(health <= 0)
        {
            gameObject.GetComponent<Animation>().Play("Shield_broken");
            GameGlobal.play.shield_flag = false;
            UIManager.updateS_HP();
            Destroy(gameObject, 0.2f);
           
        }
    }

    public void getDamage(int damge)
    {
        health -= damge / 2;
        UIManager.updateS_HP(true);
    }

}
