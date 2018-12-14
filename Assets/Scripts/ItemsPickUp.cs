using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPickUp : MonoBehaviour {

    private InventoryController inventory;
    public GameObject objetoInventario;

    void Start () {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryController>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    inventory.objetos[i] = this.name;
                    Instantiate(objetoInventario, inventory.slots[i].transform, false);

                    Destroy(gameObject);
                    break;
                }
                else
                {
                    Debug.Log("Inventario lleno");
                }
            }
        }
    }
}
