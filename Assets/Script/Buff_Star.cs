using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Star : BaseObject
{
    private float falling_speed = 4f;
    private int score = 50;


    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        transform.Translate(Vector3.down * falling_speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().add_score(score);
            Destroy(gameObject);
        }
    }

}
