using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater : MonoBehaviour
{
    public float creat_rate = 2f;
    private float last_creatTime;
    private int index;

    SpriteRenderer SR;
    Vector2 enemy_size;

    [System.Serializable]
    public struct Enemy_Info
    {
        public GameObject enemy_obj;
        public int weight;  // 权重设置从大到小
        public Vector2 border_limit;
    }

    public Enemy_Info[] enemy_Info;

    private int sum_weight;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemy_Info.Length; i++)
        {
            SR = enemy_Info[i].enemy_obj.GetComponent<SpriteRenderer>();
            // 加载的物体大小
            enemy_size = new Vector2(SR.bounds.size.x * transform.localScale.x, SR.bounds.size.y * transform.localScale.y);
            // 相对于物体大小的场景边界限制
            enemy_Info[i].border_limit = new Vector2((GameGlobal.Screen_width - enemy_size.x) / 2, (GameGlobal.Screen_height - enemy_size.y) / 2);

            sum_weight += enemy_Info[i].weight;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - last_creatTime > creat_rate)
        {
            index = Random.Range(0, sum_weight);
            for (int i = 0; i <= enemy_Info.Length; i++)
            {
                // 设置权重从大到小
                if (index < enemy_Info[i].weight)
                {
                    GameObject enemy_clone = GameObject.Instantiate(enemy_Info[i].enemy_obj);
                    enemy_clone.transform.position = new Vector3(Random.Range(-enemy_Info[i].border_limit.x, enemy_Info[i].border_limit.x), 6, 0);
                    last_creatTime = Time.time;
                    break;
                }
                else
                {
                    index -= enemy_Info[i].weight;
                }
            }
        }




    }
}
