using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLv3_Bullet : PlayerBullet
{
    new void Start()
    {
        base.Start();
        bullet_damage = 5 + KillEnemyCount.P_Bullet_damage_Level * 2;
        bullet_speed = 15f;

        Effect_hitES = Resources.Load<GameObject>("Effect/Effect_HitES");
    }

    // Update is called once per frame
    new void Update()
    {
        outofBoundary(-0.3f);
        gameObject.transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BaseEnemy>().getDamage(bullet_damage);
            GameGlobal.play.add_score(1);
        }

        if (collision.gameObject.tag == "E_Shield")
        {
            collision.gameObject.GetComponent<Enemy_shield>().getDamage(bullet_damage);
            GameObject.Instantiate(Effect_hitES, gameObject.transform.position, Quaternion.identity);
            GameGlobal.play.add_score(1);
            
        }

    }
}
