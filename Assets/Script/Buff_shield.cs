using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_shield : BaseObject
{
    private float falling_speed = 4f;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        transform.Translate(Vector3.down * falling_speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().shield_on();
            collision.gameObject.GetComponent<Player>().add_score(10);
            Destroy(gameObject);
        }
    }
}
