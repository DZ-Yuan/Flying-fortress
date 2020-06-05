using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SHPFollow : MonoBehaviour
{
    Vector3 target;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameGlobal.play)
        {
            target = Camera.main.WorldToScreenPoint(GameGlobal.play.transform.position);
            target.x += 80f;
            target.y += 30f;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, 0.1f);
        }
    }
}
