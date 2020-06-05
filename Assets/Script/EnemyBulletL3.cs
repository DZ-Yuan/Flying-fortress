using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletL3 : EnemyBullet
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        bullet_damage = 2;
        bullet_speed = 3.0f;
        Effect_explode = Resources.Load<GameObject>("Effect\\HitShield");
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
