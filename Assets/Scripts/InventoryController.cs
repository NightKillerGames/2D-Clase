using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    private static int numeroDeSlots = 2;
    public bool[] isFull = new bool[numeroDeSlots];
    public GameObject[] slots = new GameObject[numeroDeSlots];
    public string[] objetos = new string[numeroDeSlots];
    private GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();   
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (objetos[0] == "MedioCorazonCura")
            {
                gm.playerHealth += 1;
            }
            if (objetos[0] == "CorazonCura")
            {
                gm.playerHealth += 3;
            }
            isFull[0] = false;
            GameObject gb = slots[0].transform.GetChild(0).gameObject;
            Destroy(gb);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (objetos[1] == "MedioCorazonCura")
            {
                gm.playerHealth += 1;
            }
            if (objetos[1] == "CorazonCura")
            {
                gm.playerHealth += 3;
            }
            isFull[0] = false;
            GameObject gb = slots[1].transform.GetChild(0).gameObject;
            Destroy(gb);
        }
    }
}
