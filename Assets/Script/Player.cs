using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseAttribute
{
    //private int energy = 5; 

    public float max_health = 200f;
    private char fire_type = 'N';
    private float special_fireTime;
    private float get_special_fireTime;

    public int player_score;

    public int player_EXP;
    public int player_maxEXP = 200;

    private int player_Lv = 1;
    public bool shield_flag = false;

    public bool skill_T = false;
    //private float shield_time;

    Vector3 fly_direction;
    Vector3 planeMove_pos;

    Vector3 android_direction;
    Vector3 android_target;

    GameObject Effect_explode;
    AudioSource _audio;

    Transform gun2_pos;
    Transform gun3_pos;
    Transform gun4_pos;
    Transform gun5_pos;

    GameObject PLv3_Bullet;
    GameObject SkillT_obj;


    private void Awake()
    {
        health = max_health;
        GameGlobal.play = this;
    }


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        fire_rate = 0.1f;
        fly_speed = 3f;

        // gun_tf -- 从基类继承的炮管
        gun_tf = gameObject.transform.Find("gun1_pos");
        gun2_pos = gameObject.transform.Find("gun2_pos");
        gun3_pos = gameObject.transform.Find("gun3_pos");
        gun4_pos = gameObject.transform.Find("gun4_pos");
        gun5_pos = gameObject.transform.Find("gun5_pos");

        Effect_explode = Resources.Load<GameObject>("Effect\\Effect_explode");

        _animation = gameObject.GetComponent<Animation>();
        _audio = gameObject.GetComponent<AudioSource>();

        bullet_obj = Resources.Load<GameObject>("Bullet_Prefabs\\P_Bullet");
        PLv3_Bullet = Resources.Load<GameObject>("Bullet_Prefabs\\PLv3_Bullet");
        SkillT_obj = Resources.Load<GameObject>("Skill_T");

        //Debug.Log(gun_tf.position);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        Move();


        if (Time.time - last_fireTime > fire_rate)
        {
            switch (fire_type)
            {
                case 'N':
                    fire();
                    break;

                case 'S':
                    fire_S();
                    break;

                case 'F':
                    fire();
                    break;

            }
            _audio.Play();
        }


        if (fire_type != 'N')
        {

            if (Time.time - get_special_fireTime > special_fireTime)
            {
                if (fire_type == 'F')
                {
                    Debug.Log("change to N");
                    bullet_obj = Resources.Load<GameObject>("Bullet_Prefabs\\P_Bullet");
                    PLv3_Bullet = Resources.Load<GameObject>("Bullet_Prefabs\\PLv3_Bullet");

                }

                fire_type = 'N';
            }
        }

        dead();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {


    }

    new public void getDamage(int bullet_damage)
    {
        if (!shield_flag)
        {
            _animation.Play();
            UIManager.updateHP();
            health -= bullet_damage;
        }

    }

    void dead()
    {
        if (health <= 0)
        {

            UIManager.showScore();
            GameObject.Instantiate(Effect_explode, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

    public void add_EXP(int exp)
    {
        player_EXP += exp;
        UIManager.updateEXP();

        if (player_EXP > player_maxEXP)
        {
            player_Lv++;
            player_EXP = 0;
            player_maxEXP += 100;

            Player_update();
        }
    }

    void Player_update()
    {
        switch (player_Lv)
        {
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("PlayerLv2").GetComponent<SpriteRenderer>().sprite;
                break;

            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("PlayerLv3").GetComponent<SpriteRenderer>().sprite;
                break;

            case 4:
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<GameObject>("PlayerLv4").GetComponent<SpriteRenderer>().sprite;
                break;

        }


    }


    void Move()
    {

#if UNITY_ANDROID || UNITY_IOS
        android_target = Vector3.zero;
        android_direction = Vector3.zero;
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                android_direction = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0);
            }
        }
        planeMove_pos = transform.position + GameGlobal.Screen_height / Screen.height * android_direction;

#else

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

        //限制Player飞出边框
        planeMove_pos = transform.position + fly_direction.normalized * fly_speed * Time.deltaTime;
#endif

        gameObject.transform.position = new Vector3(Mathf.Clamp(planeMove_pos.x, -boundarySize_limit.x, boundarySize_limit.x),
                                                        Mathf.Clamp(planeMove_pos.y, -boundarySize_limit.y, boundarySize_limit.y), 0);
    }

    void fire_S()
    {
        // 子弹散射
        for (int i = 0; i <= 10; i++)
        {
            GameObject bullet = GameObject.Instantiate(bullet_obj);
            bullet.transform.position = gun_tf.transform.position;
            bullet.transform.Rotate(new Vector3(0, 0, (5 - i) * 5));
        }


        last_fireTime = Time.time;
    }

    new void fire()
    {
        switch (player_Lv)
        {
            case 1:
                GameObject bullet = GameObject.Instantiate(bullet_obj);
                bullet.transform.position = gun_tf.transform.position;
                break;

            case 2:
                GameObject bullet1 = GameObject.Instantiate(bullet_obj);
                bullet1.transform.position = gun2_pos.transform.position;

                GameObject bullet2 = GameObject.Instantiate(bullet_obj);
                bullet2.transform.position = gun3_pos.transform.position;
                break;

            case 3:
                GameObject bullet3 = GameObject.Instantiate(PLv3_Bullet);
                bullet3.transform.position = gun2_pos.transform.position;

                GameObject bullet4 = GameObject.Instantiate(PLv3_Bullet);
                bullet4.transform.position = gun3_pos.transform.position;
                break;

            case 4:
                GameObject bullet5 = GameObject.Instantiate(bullet_obj);
                bullet5.transform.position = gun2_pos.transform.position;

                GameObject bullet6 = GameObject.Instantiate(bullet_obj);
                bullet6.transform.position = gun3_pos.transform.position;

                GameObject bullet7 = GameObject.Instantiate(PLv3_Bullet);
                bullet7.transform.position = gun4_pos.transform.position;

                GameObject bullet8 = GameObject.Instantiate(PLv3_Bullet);
                bullet8.transform.position = gun5_pos.transform.position;
                break;

        }

        last_fireTime = Time.time;
    }

    public void useSkill()
    {
        UIManager.updateSkillT_icon("Skill_T", false);
        GameObject.Instantiate(SkillT_obj, gameObject.transform.position, Quaternion.identity);
    }


    public void change_fireType(char type, float time)
    {
        switch (type)
        {
            case 'S':
                fire_type = type;
                break;

            case 'F':
                change_bullet('F');
                fire_type = type;
                break;
        }

        special_fireTime = time;
        get_special_fireTime = Time.time;
    }

    public void change_bullet(char type)
    {
        switch (type)
        {
            case 'F':
                bullet_obj = Resources.Load<GameObject>("Bullet_Prefabs\\P_bullet_F");
                PLv3_Bullet = Resources.Load<GameObject>("Bullet_Prefabs\\P_bullet_F");
                break;

        }


    }

    public void add_score(int score)
    {
        player_score += score;
        UIManager.updateSCORE();
    }

    public void add_HP(float num)
    {
        health = Mathf.Clamp(health + num, 0, 100);
        UIManager.updateHP();
    }

    public void shield_on()
    {
        if (!shield_flag)
        {
            shield_flag = true;
            GameObject.Instantiate(Resources.Load<GameObject>("Buff_Prefabs/Player_Shield"), gameObject.transform);
        }

    }

}
