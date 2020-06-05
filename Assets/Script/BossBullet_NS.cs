using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_NS : EnemyBullet
{
    
    new void Start()
    {
        base.Start();
        bullet_damage = 3;
        bullet_speed = 5.0f;
        Effect_explode = Resources.Load<GameObject>("Effect\\HitShield");
    }

    
    new void Update()
    {
        base.Update();
    }
}
