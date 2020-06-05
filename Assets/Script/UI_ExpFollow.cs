using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ExpFollow : MonoBehaviour
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
            target.x += 65f;
            target.y -= 65f;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, 0.1f);
        }

    }
}
