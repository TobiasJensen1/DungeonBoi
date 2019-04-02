using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    List<Sprite> inventory = new List<Sprite>();
    public GameObject inventorySlots;
    public Sprite healthPotion;
    float playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        inventory.Add(healthPotion);
        GameObject space1 = inventorySlots.transform.Find("Space1").Find("Item").gameObject;
        space1.GetComponent<SpriteRenderer>().sprite = inventory[0];

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    public void useItem() {
        print("space1");
    }
}
