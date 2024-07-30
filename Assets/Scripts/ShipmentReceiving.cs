using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipmentReceiving : MonoBehaviour
{
    [SerializeField] public GameObject ReceivingArea;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RawInventoryItem item = new RawInventoryItem();

            ReceivingArea.GetComponent<ReceivingInventory>().AddInventoryItems(1, item);
        }
    }
}
