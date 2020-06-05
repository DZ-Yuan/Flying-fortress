using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletL2 : EnemyBullet
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        bullet_damage = 5;
        bullet_speed = 5.0f;
        Effect_explode = Resources.Load<GameObject>("Effect\\HitShield");
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //gameObject.transform.Translate(Vector2.up * bullet_speed * Time.deltaTime);
    }

    //继承自EnemyBullet方法，使用自身damage，speed参数
    //protected void OnTriggerEnter2D(Collider2D collision)
}
