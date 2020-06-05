using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_T : BaseObject
{
    private int damage = 10 + KillEnemyCount.P_Bullet_damage_Level * 20;
    private float x_fly_speed = 3f;
    private float y_fly_speed = 5f;
    private float follow_speed = 8f;
    private float hit_rate = 0.3f;
    private float last_hitTime;

    private GameObject target;

    new void Start()
    {
        base.Start();
        Destroy(gameObject, 8f);
    }

    // Update is called once per frame
    new void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
            transform.up = target.transform.position - gameObject.transform.position;

            //transform.position += transform.up * fly_speed * Time.deltaTime;
            gameObject.transform.Translate(Vector3.up * follow_speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(Vector3.up * x_fly_speed * Time.deltaTime);
            gameObject.transform.Translate(Vector3.right * y_fly_speed * Time.deltaTime);

            // PingPong运动
            //transform.position = new Vector3(Mathf.PingPong(Time.time, boundarySize_limit.x * 2) - boundarySize_limit.x,
            //                                 Mathf.PingPong(Time.time, boundarySize_limit.y * 2) - boundarySize_limit.y, 0);  
        }

        if(gameObject.transform.position.x > boundarySize_limit.x || gameObject.transform.position.x < -boundarySize_limit.x)
        {
            x_fly_speed = -x_fly_speed;
        }

        if (gameObject.transform.position.y > boundarySize_limit.y || gameObject.transform.position.y < -boundarySize_limit.y)
        {
            y_fly_speed = -y_fly_speed;
        }

        

    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (Time.time - last_hitTime > hit_rate)
            {
                col.gameObject.GetComponent<BaseEnemy>().getDamage(damage);
                last_hitTime = Time.time;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "E_Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
