using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_slide : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {

        gameObject.transform.Translate(Vector3.down * 2f * Time.deltaTime);

        if(gameObject.transform.position.y < -9.984)
        {
            gameObject.transform.position = new Vector3(0, 9.98f, 1);
        }





    }
}
