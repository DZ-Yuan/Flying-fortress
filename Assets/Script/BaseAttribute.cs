using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttribute : BaseObject
{
    public float health = 10;
    protected float fire_rate;
    protected float last_fireTime;
    protected float fly_speed;

    public GameObject bullet_obj;
    protected Transform gun_tf;

    protected Animation _animation;


    //// Start is called before the first frame update
    //protected void Start()
    //{

    //}

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
        _animation = gameObject.GetComponent<Animation>();
    }

    public void getDamage(int bullet_damage)
    {
        _animation.Play();
        health -= bullet_damage;
    }

    protected void fire()
    {
        GameObject bullet = GameObject.Instantiate(bullet_obj);
        bullet.transform.position = gun_tf.transform.position;
        
        last_fireTime = Time.time;
    }

}
