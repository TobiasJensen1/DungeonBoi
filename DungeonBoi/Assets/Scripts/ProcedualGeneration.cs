using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProcedualGeneration : MonoBehaviour
{
    public GameObject startChunk;
    public GameObject chunk1;
    bool spawn = false;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectToPlace = chunk1;


        if(collision.transform.tag == "Player")
        {
            if(transform.gameObject.name == "ColliderEast" && !spawn) {
            Instantiate(chunk1, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);
            Camera.main.transform.DOMoveX(startChunk.transform.position.x+18, 2);
                spawn = true;
            }
            if(transform.gameObject.name == "ColliderEast" && spawn)
            {
                Camera.main.transform.DOMoveX(startChunk.transform.position.x + 18, 2);
            }
            if (transform.gameObject.name == "ColliderWest" && !spawn)
            {
                Instantiate(chunk1, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);
                Camera.main.transform.DOMoveX(startChunk.transform.position.x -18, 2);
                spawn = true;
            }
            if (transform.gameObject.name == "ColliderWest" && spawn)
            {
                Camera.main.transform.DOMoveX(startChunk.transform.position.x - 18, 2);
            }
            
            
        }
    }
}
