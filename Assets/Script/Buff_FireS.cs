using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_FireS : BaseObject
{
    protected float falling_speed;
    
    
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        falling_speed = 2f;      
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        transform.Translate(Vector3.down * falling_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().change_fireType('S', 5f);
            Destroy(gameObject);
        }
    }

}
