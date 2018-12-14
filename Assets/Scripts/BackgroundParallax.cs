using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    public Camera cam;
    private SpriteRenderer spr;
    private float camX;

    void Start ()
    {
        camX = cam.transform.position.x;
        spr = GetComponentInChildren<SpriteRenderer>();
    }
	
	void Update ()
    {
        camX = cam.transform.position.x;
        foreach (SpeedFactor spf in GetComponentsInChildren<SpeedFactor>())
        {
            spr = GetComponentInChildren<SpriteRenderer>();
            float delta_x = spf.spd * Time.deltaTime * spf.speedFactor;
            float new_x = spf.transform.position.x - delta_x;
 
            if (new_x <(camX -spr.size.x))
            {
              
                new_x += spr.size.x * 2;
            }
            if (new_x > (camX + spr.size.x))
            {

                new_x -= spr.size.x * 2;
            }
            spf.transform.position = new Vector2(new_x, spf.transform.position.y);
        }
	}
}
