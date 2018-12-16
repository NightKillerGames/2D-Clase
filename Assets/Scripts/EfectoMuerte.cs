﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoMuerte : MonoBehaviour {

    public float duracion = 1f;
    private float timer;
	
	void Update () {
        timer += Time.deltaTime;
        if (timer >= duracion)
            Destroy(gameObject);
	}
}