using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

    public Transform firePoint;
    public GameObject BellotaPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}
    void Shoot()
    {
        Instantiate(BellotaPrefab, firePoint.position, firePoint.rotation);
    }
}
