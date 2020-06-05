using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyCount : MonoBehaviour
{
    public static int Kill_L1;
    public static int Kill_L2;
    public static int Kill_L3;
    public static int Kill_L4;

    public static int Kill_BossL1;

    public static int Kill_sum;

    public static int stage = 10;
    public static bool BosshasbeenKilled_flag = false;

    public static int P_Bullet_damage_Level = 0;
    public static int P_Bullet_damage_up_stage = 30;


    void Start()
    {

    }


    void Update()
    {

    }

    public static void init_ALL()
    {
        Kill_L1 = 0;
        Kill_L2 = 0;
        Kill_L3 = 0;
        Kill_L4 = 0;

        Kill_BossL1 = 0;

        Kill_sum = 0;

        stage = 10;
        BosshasbeenKilled_flag = false;

        P_Bullet_damage_Level = 0;
        P_Bullet_damage_up_stage = 30;

    }

    public static void Kill_count(string enemy_ID)
    {
        Debug.Log(enemy_ID + "+1");
        Kill_sum++;
        
        switch (enemy_ID)
        {
            case "L1":
                Kill_L1++;
                GameGlobal.play.add_EXP(3);
                break;

            case "L2":
                Kill_L2++;
                GameGlobal.play.add_EXP(5);
                break;

            case "L3":
                Kill_L3++;
                GameGlobal.play.add_EXP(10);
                break;

            case "L4":
                Kill_L4++;
                GameGlobal.play.add_EXP(5);
                break;

            case "BossL1":
                Kill_BossL1++;
                GameGlobal.play.add_EXP(50);
                break;

        }

        if (Kill_sum > stage)
        {
            Debug.Log("Game Stage Up");
            GameObject boss = GameObject.Instantiate(Resources.Load<GameObject>("BossLevel1"));
            boss.transform.position = new Vector3(0, -6, 0);

            stage += 20;  
        }

        if(Kill_sum > P_Bullet_damage_up_stage)
        {
            Debug.Log("P_Bullet Level Up");
            P_Bullet_damage_Level++;
            P_Bullet_damage_up_stage += 30;
        }


    }

    public static void Record_data()
    {
        PlayerPrefs.SetInt("Kill_sum", Kill_sum);

    }


}
