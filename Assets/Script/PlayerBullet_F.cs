using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_F : PlayerBullet
{ 
    GameObject target;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        bullet_damage = 3 + KillEnemyCount.P_Bullet_damage_Level * 2;
        bullet_speed = 20f;

        Effect_hitES = Resources.Load<GameObject>("Effect/Effect_HitES");
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // Follow
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            target = GameObject.FindGameObjectWithTag("Enemy");

            transform.up = Vector3.Slerp(transform.up, target.transform.position - transform.position, 
                                0.5f / Vector2.Distance(this.transform.position, target.transform.position));

            transform.position += transform.up * bullet_speed * Time.deltaTime;

            //Vector3 vec = target.transform.position - gameObject.transform.position;
            //float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.translate();
            

            //Debug.Log(Mathf.Abs(vec.y) / Mathf.Abs(vec.x));
            //gameObject.transform.Rotate(new Vector3(0, 0, Mathf.Atan(vec.y / vec.x)));
        }

        else
        {
            transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);
        }

    }
}
