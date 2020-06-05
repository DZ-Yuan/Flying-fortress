using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
 
    private float creat_rate = 5;
    private float last_creatTime;
    private int index;
    private int sum_weight;

    //float slide_dis = 8;
    //SpriteRenderer slide;

    SpriteRenderer SR;
    Vector2 buff_size;

    [System.Serializable]
    public struct Buff_Info
    {
        public GameObject buff_obj;
        public int weight;
        public Vector2 border_limit;
    }

    public Buff_Info[] buff_Info;


    // Start is called before the first frame update
    void Start()
    {
        //slide = gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < buff_Info.Length; i++)
        {
            sum_weight += buff_Info[i].weight;
            SR = buff_Info[i].buff_obj.GetComponent<SpriteRenderer>();
            // 加载的物体大小
            buff_size = new Vector2(SR.bounds.size.x * transform.localScale.x, SR.bounds.size.y * transform.localScale.y);
            // 相对于物体大小的场景边界限制
            buff_Info[i].border_limit = new Vector2((GameGlobal.Screen_width - buff_size.x) / 2, (GameGlobal.Screen_height - buff_size.y) / 2);

        }

    }

    // Update is called once per frame
    void Update()
    {
        // 场景滚动
        //slide.size = new Vector2(5, slide_dis += 0.05f);

        // 增益道具掉落
        if (Time.time - last_creatTime > creat_rate)
        {
            index = Random.Range(0, sum_weight);
            for (int i = 0; i < buff_Info.Length; i++)
            {
                // 设置权重从大到小
                if (index < buff_Info[i].weight)
                {
                    GameObject buff_clone = GameObject.Instantiate(buff_Info[i].buff_obj);
                    buff_clone.transform.position = new Vector3(Random.Range(-buff_Info[i].border_limit.x, buff_Info[i].border_limit.x), 6, 0);
                    last_creatTime = Time.time;
                    break;
                }
                else
                {
                    index -= buff_Info[i].weight;
                }
            }
        }




    }

    void slide_BG()
    {

    }
}
