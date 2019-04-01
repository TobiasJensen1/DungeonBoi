using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 pushForce;
    public float speed;

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
        pushForce = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        move();
        attack();
        
    }


    void move()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Vector2.MoveTowards(transform.position, pushForce, speed*Time.deltaTime);
        }
        

    }

    void attack() { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null && hit.collider.transform.tag == "Enemy")
        {
            distanceX = transform.position.x - hit.collider.transform.position.x;
            distanceY = transform.position.y - hit.collider.transform.position.y;

            if (Input.GetMouseButton(0) && distanceX <= 0.3 && distanceX <= 0.3 ) { 
            Destroy(hit.collider.gameObject);
                print("attack!");
            }


        }
    }
    



}
