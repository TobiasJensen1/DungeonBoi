using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProcedualGeneration : MonoBehaviour
{
    public GameObject startChunk;
    public GameObject chunk1;
    bool hasEast = false;
    bool hasWest = false;

    bool hasNorth = false;
    bool hasSouth = false;
   
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
            if(transform.gameObject.name == "ColliderEast" && !hasEast) { 
            Instantiate(chunk1, new Vector2(startChunk.transform.position.x + 14, startChunk.transform.position.y), Quaternion.identity);
            Camera.main.transform.DOMoveX(startChunk.transform.position.x+15, 2);
                hasEast = true;

            }
            if (transform.gameObject.name == "ColliderWest" && !hasWest)
            {
                Instantiate(chunk1, new Vector2(startChunk.transform.position.x - 14, startChunk.transform.position.y), Quaternion.identity);
                Camera.main.transform.DOMoveX(startChunk.transform.position.x -15, 2);
                hasWest = true;
            }
            
        }
    }
}
