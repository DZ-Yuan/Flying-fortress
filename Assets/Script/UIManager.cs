using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static RectTransform HP;
    public static RectTransform EXP;
    public static RectTransform S_HP;
    public static RectTransform skill_icon;
    public static GameObject SHP_obj;
    public static GameObject skill_icon_obj;

    public static Animation Score_ani;
    public static Text Score;
    public static Text showPlayerScore;

    public static Vector3 skill_icon_pos;
    public static string isNeed2Move;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        HP = GameObject.Find("HP_Slider/HP").GetComponent<RectTransform>();
        HP.sizeDelta = new Vector2(100 * GameGlobal.play.health / GameGlobal.play.max_health, 21f);

        SHP_obj = GameObject.Find("Shield_HP");
        S_HP = GameObject.Find("Shield_HP/SHP").GetComponent<RectTransform>();
        S_HP.sizeDelta = new Vector2(0, 9f);
        SHP_obj.SetActive(false);

        EXP = GameObject.Find("EXP/exp").GetComponent<RectTransform>();
        EXP.sizeDelta = new Vector2(0, 14f);

        skill_icon_obj = GameObject.Find("SkillT_icon");
        skill_icon = GameObject.Find("SkillT_icon").GetComponent<RectTransform>();
        skill_icon_pos = Camera.main.WorldToScreenPoint(new Vector3(2.2f, -3.5f, 0));
        skill_icon_obj.SetActive(false);

        Score_ani = GameObject.Find("Score").GetComponent<Animation>();
        Score = Score_ani.GetComponent<Text>();
        Score.text = "Score:" + GameGlobal.play.player_score;

        showPlayerScore = transform.Find("GameOvre/PlayerSocre").GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {

        switch(isNeed2Move)
        {
            case "Skill_T":
                skill_icon.position = Vector3.Lerp(skill_icon.position, skill_icon_pos, 0.05f);
                isNeed2Move = skill_icon.position == skill_icon_pos ? "" : isNeed2Move;
                break;

            case "Skill_M":
                // Todo
                break;

        }





    }

    public static void updateHP()
    {
        HP.sizeDelta = new Vector2(100 * GameGlobal.play.health / GameGlobal.play.max_health, 21f);
    }

    public static void updateS_HP(bool on_off = false)
    {

        SHP_obj.SetActive(on_off);
        S_HP.sizeDelta = new Vector2(46 * GameGlobal.shield.health / GameGlobal.shield.max_health, 9f);
    }

    public static void updateEXP()
    {
        EXP.sizeDelta = new Vector2(120 * GameGlobal.play.player_EXP / GameGlobal.play.player_maxEXP, 14f);
    }

    public static void updateSCORE()
    {
        Score.text = "Score:" + GameGlobal.play.player_score;
        Score_ani.GetComponent<Animation>().Play();
    }

    public static void updateSkillT_icon(string icon, bool on_off = true)
    {
        skill_icon_obj.SetActive(on_off);
        switch (icon)
        {
            case "Skill_T":
                skill_icon.position = Camera.main.WorldToScreenPoint(GameGlobal.play.transform.position);
                isNeed2Move = icon;
                break;

            case "Skill_M":
                // Todo
                break;
        }

    }

    public static void showScore()
    {
        GameObject.Find("Canvas").transform.Find("GameOvre").gameObject.SetActive(true);
        showPlayerScore.text = "" + GameGlobal.play.player_score;


    }  

    public void Pause(bool pause)
    {
        Debug.Log("Pause");
        
        switch (pause)
        {
            case true:
                transform.Find("Pause_Panel").gameObject.SetActive(true);
                Time.timeScale = 0;
                break;

            case false:
                transform.Find("Pause_Panel").gameObject.SetActive(false);
                Time.timeScale = 1;
                break;

        }


    }

    public void Restart()
    {
        KillEnemyCount.init_ALL();
        SceneManager.LoadScene(1);
    }

    public void Back2Start()
    {
        KillEnemyCount.init_ALL();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
