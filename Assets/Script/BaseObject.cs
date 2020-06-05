using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    protected Vector2 obj_size;
    protected Vector2 boundarySize_limit;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // 加载的物体大小
        obj_size = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x * transform.localScale.x,
                                    GetComponent<SpriteRenderer>().bounds.size.y * transform.localScale.y);

        // 相对于物体大小的场景边界限制
        boundarySize_limit = new Vector2((GameGlobal.Screen_width - obj_size.x) / 2, (GameGlobal.Screen_height - obj_size.y) / 2);

        //Debug.Log("obj_size:" + obj_size);
        //Debug.Log("boundary_limit:" + boundarySize_limit);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        outofBoundary(2f);
    }

    // 销毁飞出场景的物体
    protected void outofBoundary(float scope)
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(boundarySize_limit.x) + scope || Mathf.Abs(transform.position.y) > Mathf.Abs(boundarySize_limit.y) + scope)
        {
            Destroy(gameObject);
        }
    }
}
