using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel1 : BaseEnemy
{
    private bool boss_canCome_flag = true;
    Vector3 plan_pos;

    private float last_fire_N_Time;
    private float fire_N_rate = 1.0f;

    private float last_fire_S_Time;
    private float fire_S_rate = 0.3f;
    private bool fire_S_flag = true;
    private float fire_sum = 20;
    private float recover_time;

    Vector3 bound_pos;

    private GameObject bulletNormal_obj;
    private GameObject bulletS_obj;
    //private GameObject bulletBig_obj;

    private Transform gun1_tf;
    private Transform gun2_tf;
    private Transform gun3_tf;
    private Transform gun4_tf;
    private Transform gunS_tf;

    //private float shield_time;
    //private float shield_enableTime;
    //private GameObject shield_obj;

    private GameObject Effect_getDamage;

    private GameObject Effect_BosslowHP;
    Vector3 Effect_lowHP_pos;
    private float smoke_rate;
    private float Last_smokeTime;

    private string BossStage = "NO1";
    public bool shield_flag = false;

    private void Awake()
    {
        //GameGlobal.BossLevel1 = this;
    }


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // 特有属性初始化
        Enemy_ID = "BossL1";
        health = 400 + KillEnemyCount.Kill_BossL1 * 200;
        fly_speed = 0.5f;
        smoke_rate = 0.3f;

        // 加载所需组件
        bulletNormal_obj = Resources.Load<GameObject>("Bullet_Prefabs/BossBullet_N");
        bulletS_obj = Resources.Load<GameObject>("Bullet_Prefabs/BossBullet_S");

        Effect_getDamage = Resources.Load<GameObject>("Effect/Effect_HitBoss");
        Effect_BosslowHP = Resources.Load<GameObject>("Effect/Effect_BosslowHP");


        gun1_tf = gameObject.transform.Find("gun1_pos");
        gun2_tf = gameObject.transform.Find("gun2_pos");
        gun3_tf = gameObject.transform.Find("gun3_pos");
        gun4_tf = gameObject.transform.Find("gun4_pos");
        gunS_tf = gameObject.transform.Find("gunS_pos");

        // Boss移动位置限制；Boss出场预定位置
        bound_pos = new Vector3(boundarySize_limit.x, boundarySize_limit.y, 0);
        plan_pos = new Vector3(0, 3.4f, 0);




    }

    // Update is called once per frame
    new void Update()
    {
        // Boss出场 无碰撞
        if (boss_canCome_flag)
        {

            gameObject.transform.Translate(Vector3.up * 1.2f * Time.deltaTime);
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, plan_pos, 0.003f);
            if (gameObject.transform.position.y > 3.3f)
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                gameObject.gameObject.tag = "Enemy";
                boss_canCome_flag = false;

            }

        }

        else
        {

            gameObject.transform.Translate(Vector3.left * fly_speed * Time.deltaTime);
            if (gameObject.transform.position.x > boundarySize_limit.x - 1f || gameObject.transform.position.x < -boundarySize_limit.x + 1f)
            {
                fly_speed = -fly_speed;
            }

            // Boss阶段转换
            BossStage = health < 100 ? "NO2" : BossStage;

            BossStage = health < 80 ? "NO3" : BossStage;

            BossStage = health < 30 ? "DEAD" : BossStage;

            smoke_rate = health < 50 ? 0.15f : smoke_rate;

            switch (BossStage)
            {
                case "NO1":
                    fire_N();
                    fire_S();
                    break;

                case "NO2":
                    smoking();
                    fire_S();
                    break;

                case "NO3":
                    smoking();
                    fire_S();
                    if(!shield_flag)
                    {
                        GameObject.Instantiate(Resources.Load<GameObject>("Buff_Prefabs/Enemy_shield"), gameObject.transform);
                        shield_flag = true;
                    }
                    
                    break;

                case "DEAD":
                    explosion();
                    break;

            }










        }

        isDead();
    }

    void fire_N()
    {

        if (Time.time - last_fire_N_Time > fire_N_rate)
        {

            GameObject bullet = GameObject.Instantiate(bulletNormal_obj);
            bullet.transform.position = gun1_tf.position;

            GameObject bullet2 = GameObject.Instantiate(bulletNormal_obj);
            bullet2.transform.position = gun2_tf.position;

            last_fire_N_Time = Time.time;
        }

    }

    void fire_S()
    {
        if (Time.time - last_fire_S_Time > fire_S_rate && fire_S_flag)
        {
            GameObject bullet = GameObject.Instantiate(bulletS_obj);
            bullet.transform.position = gunS_tf.transform.position;

            bullet.transform.Rotate(new Vector3(0, 0, (5 - Random.Range(0, 15)) * 5));

            fire_sum--;
            if (fire_sum < 0)
            {
                fire_S_flag = false;
                recover_time = Time.time;
            }

            last_fire_S_Time = Time.time;
        }

        else if (Time.time - recover_time > 5f)
        {
            fire_S_flag = true;
            fire_sum = 20;
        }

    }

    void smoking()
    {
        if (Time.time - Last_smokeTime > smoke_rate)
        {
            Effect_lowHP_pos = gameObject.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            GameObject.Instantiate(Effect_BosslowHP, Effect_lowHP_pos, Quaternion.identity);
            Last_smokeTime = Time.time;
        }

    }

    void explosion()
    {
        if (Time.time - Last_smokeTime > smoke_rate)
        {
            Effect_lowHP_pos = gameObject.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            GameObject.Instantiate(Effect_explode, Effect_lowHP_pos, Quaternion.identity);
            Last_smokeTime = Time.time;
        }
    }

    void isDead()
    {
        if (health <= 0)
        {
            explosion();
            gameObject.transform.Translate(Vector3.down * 1f * Time.deltaTime);

            if (gameObject.transform.position.y < 0)
            {
                for (int i = 0; i < 500; i++)
                {
                    explosion();
                }
                KillEnemyCount.Kill_count(Enemy_ID);
                Destroy(gameObject);
            }
        }


    }


    // 碰撞
    new private void OnTriggerEnter2D(Collider2D col)
    {
        // Caution: 被动调用主动
        if (col.gameObject.tag == "P_Bullet")
        {
            GameObject.Instantiate(Effect_getDamage, col.gameObject.transform.position, Quaternion.identity);
        }

        // Caution: 被动调用主动
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().getDamage(999);
        }

    }



}
