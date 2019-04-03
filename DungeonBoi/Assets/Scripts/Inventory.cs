using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{

    List<Sprite> inventory = new List<Sprite>();
    public List<GameObject> inventorySlots = new List<GameObject>();

    public GameObject playerInventory;
    public Sprite healthPotion;
    float playerHealth;

    Sprite chosenItem;

    public GameObject reviveObject;
    public Sprite heart;

    bool minimap;
    bool merchant;

    Sprite objectToAdd;
    bool hasHearth;


    // Start is called before the first frame update
    void Start()
    {
        hasHearth = GameObject.Find("Player").GetComponent<PlayerMovement>().hasHearth;
        Camera.main.transform.Find("MerchantItems").gameObject.SetActive(false);
        for (int i = 0; i < playerInventory.transform.childCount; i++)
        {
            inventorySlots.Add(playerInventory.transform.GetChild(i).gameObject);
        }
        //inventory.Add(healthPotion);
    }

    // Update is called once per frame
    void Update()
    {



        useItem();
    }


    public void useItem()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        //Finds item chosen from inventory
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Space1")
        {
            if (inventorySlots[0].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null)
            {
                chosenItem = inventorySlots[0].transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
                inventorySlots[0].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = null;
                //inventory[0] = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Space2")
        {
            if (inventorySlots[1].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null)
            {
                chosenItem = inventorySlots[1].transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
                inventorySlots[1].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = null;
                //inventory[1] = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Space3")
        {
            if (inventorySlots[2].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null)
            {
                chosenItem = inventorySlots[2].transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
                inventorySlots[2].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = null;
                //inventory[2] = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Space4")
        {
            if (inventorySlots[3].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null)
            {
                chosenItem = inventorySlots[3].transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
                inventorySlots[3].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = null;
                //inventory[3] = null;
            }
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Map")
        {
            if (minimap)
            {

                Camera.main.transform.Find("UiHealth").gameObject.SetActive(true);
                Camera.main.transform.Find("UiXp").gameObject.SetActive(true);
                Camera.main.transform.Find("Inventory").gameObject.SetActive(true);
                Camera.main.transform.Find("PlayerCoins").gameObject.SetActive(true);

                Camera.main.DOOrthoSize(7, 2);
                minimap = false;
            }
            else
            {
                Camera.main.transform.Find("UiHealth").gameObject.SetActive(false);
                Camera.main.transform.Find("UiXp").gameObject.SetActive(false);
                Camera.main.transform.Find("Inventory").gameObject.SetActive(false);
                Camera.main.transform.Find("PlayerCoins").gameObject.SetActive(false);

                Camera.main.DOOrthoSize(25, 2);
                minimap = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && (hit.collider.transform.tag == "Merchant" || hit.collider.transform.tag == "Merchant1" || hit.collider.transform.tag == "Merchant2" || hit.collider.transform.tag == "Merchant3"))
        {
            merchant = true;
            if (merchant)
            {
                Camera.main.transform.Find("MerchantItems").gameObject.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag != "Merchant")
        {
            merchant = false;
            Camera.main.transform.Find("MerchantItems").gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Merchant1")
        {
            //buyItem(25, hit, false);
        }

        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Merchant2")
        {
            //buyItem(100, hit, false);
        }
        if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Merchant3")
        {
            //buyItem(250, hit, true);
        }


        if (chosenItem != null)
        {

            //Uses item chosen from inventory
            if (chosenItem.name == "potion_red")
            {
                GameObject.Find("Player").GetComponent<PlayerStats>().health = GameObject.Find("Player").GetComponent<PlayerStats>().health + 10;
            }
            if (chosenItem.name == "heart")
            {
                GameObject.Find("Player").GetComponent<PlayerStats>().revive = true;
                GameObject.Find("Player").GetComponent<PlayerMovement>().hasHearth = false;
                reviveObject.GetComponent<SpriteRenderer>().sprite = heart;
            }
        }

        chosenItem = null;


    }

    void buyItem(float price, RaycastHit2D hit, bool map)
    {
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {

                if (inventorySlots[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null && inventorySlots[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite.name == "heart")
                {
                    hasHearth = true;
                }


                if (inventorySlots[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite == null)
                {
                    objectToAdd = hit.collider.transform.Find("Item").GetComponent<SpriteRenderer>().sprite;
                }


                if (objectToAdd.name == "heart" && hasHearth)
                {
                    break;
                }
                else
                {
                    if (GameObject.Find("Player").GetComponent<PlayerStats>().coins >= price)
                    {
                        inventorySlots[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = objectToAdd;
                        GameObject.Find("Player").GetComponent<PlayerStats>().coins = GameObject.Find("Player").GetComponent<PlayerStats>().coins - price;
                        break;
                    }
                }
            }

        }
    }
}
    

