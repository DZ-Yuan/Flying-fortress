using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_addHP : BaseObject
{
    private float falling_speed = 2f;
    private float re_HP = 20;


    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        transform.Translate(Vector3.down * falling_speed * Time.deltaTime);
                                        
        transform.position = new Vector3(Mathf.PingPong(Time.time, boundarySize_limit.x * 2) - boundarySize_limit.x, transform.position.y, 0);
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().add_HP(re_HP);
            collision.gameObject.GetComponent<Player>().add_score(20);
            Destroy(gameObject);
        }
    }
}
