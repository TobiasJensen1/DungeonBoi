using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 mousePos;
    public float speed;
    Vector2 moveTo;

    bool combat = false;

    float distanceX;
    float distanceY;

    Sprite objectToAdd;
    bool revive;
    public bool hasHearth;


    List<GameObject> playerInventory;
    List<GameObject> itemsOnFloor;
    int emptySpaces;
    int emptyIndex;

    public GameObject merchant;

    // Start is called before the first frame update
    void Start()
    {
        merchant.transform.position = new Vector2(merchant.transform.position.x + 0.1f, merchant.transform.position.y + 0.1f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        move();
        transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);


    }


    void move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            //Move
            if (Input.GetMouseButton(0) && (hit.collider.transform.tag == "Ground" || hit.collider.transform.tag == "Item"))
            {
                moveTo = mousePos;
            }
            
            
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Item")
        {
            playerInventory = GameObject.Find("Player").GetComponent<Inventory>().inventorySlots;

        for(int i = 0; i < playerInventory.Count; i++)
        {
                if(playerInventory[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite != null && playerInventory[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite.name == "heart")
                {
                    hasHearth = true;
                }

            if(playerInventory[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite == null)
            {
                    objectToAdd = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
                    if (objectToAdd.name == "heart" && hasHearth)
                    {
                        break;
                    }
                    else
                    {
                        playerInventory[i].transform.Find("Item").GetComponent<SpriteRenderer>().sprite = objectToAdd;
                        for(int j = 0; j < Combat.itemsOnFloor.Count; j++)
                        {
                            if(Combat.itemsOnFloor[j].transform.position == collision.transform.position)
                            {
                                Destroy(Combat.itemsOnFloor[j]);
                            }
                        }
                        break;
                    }
                    
                }
            }
        }
    }
}
