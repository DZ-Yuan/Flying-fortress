using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float fly_speed = 3f;

    protected Vector2 obj_size;
    protected Vector2 boundarySize_limit;

    Vector3 fly_direction;
    Vector3 planeMove_pos;



    // Start is called before the first frame update
    void Start()
    {
        obj_size = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x * transform.localScale.x,
                                    GetComponent<SpriteRenderer>().bounds.size.y * transform.localScale.y);
        boundarySize_limit = new Vector2((GameGlobal.Screen_width - obj_size.x) / 2, (GameGlobal.Screen_height - obj_size.y) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
    {
        fly_direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            fly_direction += Vector3.up;

        }

        if (Input.GetKey(KeyCode.S))
        {
            fly_direction += Vector3.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            fly_direction += Vector3.left;
            //gameObject.transform.Rotate(new Vector3(0, 30, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            fly_direction += Vector3.right;
            //gameObject.transform.Rotate(new Vector3(0, -30, 0));
        }

        planeMove_pos = transform.position + fly_direction.normalized * fly_speed * Time.deltaTime;

        gameObject.transform.position = new Vector3(Mathf.Clamp(planeMove_pos.x, -boundarySize_limit.x, boundarySize_limit.x),
                                                        Mathf.Clamp(planeMove_pos.y, -boundarySize_limit.y, boundarySize_limit.y), 0);



    }


}
