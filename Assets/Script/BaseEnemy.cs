using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人基类
public class BaseEnemy : BaseAttribute
{
    protected GameObject Effect_explode;
    protected int damage_myself;
    public string Enemy_ID;


    protected new void Start()
    {
        base.Start();

        // 初始化炮管位置、加载爆炸特效、获取动画组件
        gun_tf = gameObject.transform.Find("gun_pos");
        Effect_explode = Resources.Load<GameObject>("Effect\\Effect_explode");
        _animation = gameObject.GetComponent<Animation>();

    }


    protected new void Update()
    {
        base.Update();

        //gameObject.transform.Translate(new Vector3(Mathf.Sin(Time.time) * 5 * Time.deltaTime, -1 * fly_speed * Time.deltaTime, 0));
        dead();
    }

    protected void dead()
    {
        if (health <= 0)
        {
            KillEnemyCount.Kill_count(Enemy_ID);
            GameObject.Instantiate(Effect_explode, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

    // 基类碰撞（共有方法）
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().getDamage(damage_myself);
            getDamage(999);
            //GameObject.Instantiate(Effect_explode, gameObject.transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Shield")
        {
            collision.GetComponent<Shield>().getDamage(damage_myself);
            getDamage(999);
            //GameObject.Instantiate(Effect_explode, gameObject.transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }

    }





}
