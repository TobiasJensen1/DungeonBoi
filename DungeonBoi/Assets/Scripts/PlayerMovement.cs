using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 pushForce;

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
        CameraMovement();
    }


    void move()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Vector2.MoveTowards(transform.position, pushForce, 3*Time.deltaTime);
        }
    }


    void CameraMovement()
    {
        if(transform.position.x > Camera.main.transform.position.x)
        {
            print("hej");
        }
    }
}
