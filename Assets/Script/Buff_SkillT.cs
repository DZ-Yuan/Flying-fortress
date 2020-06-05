using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_SkillT : BaseObject
{
    private float falling_speed = 2f;


    new void Start()
    {
        base.Start();
    }

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
            collision.gameObject.GetComponent<Player>().add_score(20);
            GameGlobal.play.skill_T = true;
            UIManager.updateSkillT_icon("Skill_T");
            Destroy(gameObject);
        }
    }
}
