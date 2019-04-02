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

    // Start is called before the first frame update
    void Start()
    {
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
            if (Input.GetMouseButtonDown(0) && hit.collider.transform.tag == "Ground")
            {
                moveTo = mousePos;
            }
            
            
        }


    }
    
    



}
