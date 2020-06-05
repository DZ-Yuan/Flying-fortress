using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet
{
    protected GameObject Effect_explode;

    new void Start()
    {
        base.Start();
        bullet_damage = 4;
        bullet_speed = 4.0f;
        Effect_explode = Resources.Load<GameObject>("Effect\\HitShield");
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        gameObject.transform.Translate(Vector2.up * bullet_speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponent<Player>().getDamage(bullet_damage);
            Destroy(gameObject);

        }

        if (collision.gameObject.tag == "Shield")
        {

            collision.gameObject.GetComponent<Shield>().getDamage(bullet_damage);
            GameObject.Instantiate(Effect_explode, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }




}
