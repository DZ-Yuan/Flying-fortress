using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level3:直线移动；间隔连续散射子弹
public class EnemyLevel3 : EnemyLevel2
{

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        Enemy_ID = "L3";
        health = 50 + KillEnemyCount.Kill_L3 * 3;
        fire_rate = 0.3f;
        fly_speed = 0.6f;
        damage_myself = 15;

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        gameObject.transform.Translate(0, -1 * fly_speed * Time.deltaTime, 0);

        if (Time.time - last_fireTime > fire_rate && fire_flag)
        {
            GameObject bullet = GameObject.Instantiate(bullet_obj);
            bullet.transform.position = gun_tf.transform.position;

            bullet.transform.Rotate(new Vector3(0, 0, (5 - Random.Range(0, 15)) * 5));
           
            last_fireTime = Time.time;

            fire_time--;
            fire_flag = fire_time < 0 ? false : true;

        }
        else if (Time.time - last_recover > recover_rate && !fire_flag)
        {
            fire_time++;
            last_recover = Time.time;
            fire_flag = fire_time > 5 ? true : false;
        }


    }

    


}
