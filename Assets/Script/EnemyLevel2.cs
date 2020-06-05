using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level2:直线移动；间隔连续发射子弹
public class EnemyLevel2 : BaseEnemy
{
    protected int fire_time = 5;
    protected bool fire_flag = true;
    protected float recover_rate = 0.5f;
    protected float last_recover;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        Enemy_ID = "L2";
        health = 30 + KillEnemyCount.Kill_L2 * 5;
        fire_rate = 0.3f;
        fly_speed = 0.8f;
        damage_myself = 6;

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        gameObject.transform.Translate(0, -1 * fly_speed * Time.deltaTime, 0);

        if (Time.time - last_fireTime > fire_rate && fire_flag)
        {
            fire();
            fire_time--;
            fire_flag = fire_time < 0 ? false : true;

        }
        else if(Time.time - last_recover > recover_rate && !fire_flag)
        {
            fire_time++;
            last_recover = Time.time;
            fire_flag = fire_time > 5 ? true : false;
        }

    }

}
