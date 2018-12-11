using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    private static int numeroDeSlots = 2;
    public bool[] isFull = new bool[numeroDeSlots];
    public GameObject[] slots = new GameObject[numeroDeSlots];

}
