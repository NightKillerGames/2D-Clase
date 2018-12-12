using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    public float maxSpeed = 1f;
    public float currentSpeed;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float v = Input.GetAxis("Vertical");
        if (v >= 0)
        {
            currentSpeed = v* maxSpeed;
            
        }
	}
}
