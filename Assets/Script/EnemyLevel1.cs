using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level1：直线移动；间隔发射子弹
public class EnemyLevel1 : BaseEnemy
{

    new void Start()
    {
        // 基类Start（子类共有的初始化操作）
        base.Start();

        // 子类特有属性
        Enemy_ID = "L1";
        health = 15 + KillEnemyCount.Kill_L1 * 3;
        fire_rate = 1.5f;
        fly_speed = 1.2f;
        damage_myself = 5;
    }


    new void Update()
    {
        // 基类BaseEnemy Update（子类共有行为）
        base.Update();

        // 子类特有行为
        gameObject.transform.Translate(0, -1 * fly_speed * Time.deltaTime, 0);

        if (Time.time - last_fireTime > fire_rate)
        {
            fire();
        }

    }

    //protected void OnTriggerEnter2D(Collider2D collision)
    //{

    //}


}
