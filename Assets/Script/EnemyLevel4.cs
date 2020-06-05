using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level4:撞向玩家
public class EnemyLevel4 : BaseEnemy
{
    Transform Player_tf = null;
    Vector3 target;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        Enemy_ID = "L4";
        health = 15 + KillEnemyCount.Kill_L4 * 3;
        fire_rate = 0.3f;
        fly_speed = 5f;
        damage_myself = 10;

        if (GameObject.Find("Player"))
        {
            Player_tf = GameObject.Find("Player").transform;
        }

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (Player_tf == null)
        {
            if (GameObject.Find("Player"))
            {
                Player_tf = GameObject.Find("Player").transform;
            }

            transform.Translate(Vector3.down * fly_speed * Time.deltaTime);
        }

        if (Player_tf)
        {
            target = transform.position - Player_tf.position;
            //transform.position = Vector3.Lerp(transform.position, Player_tf.position, 0.05f);
            gameObject.transform.up = target;
        }

        gameObject.transform.Translate(Vector3.down * fly_speed * Time.deltaTime);
    }
}
