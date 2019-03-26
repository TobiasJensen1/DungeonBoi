using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProcedualGeneration : MonoBehaviour
{
    GameObject test;
    
    

    Vector2 chunkChecker;

    public GameObject startChunk;
    public GameObject chunk1;
   static bool spawn = false;
   static List<Vector2> chunks = new List<Vector2>();


   
    // Start is called before the first frame update
    void Start()
    {

        chunks.Add(startChunk.transform.position);
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
              test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);
            
                chunks.Add(test.transform.position);
               
            Camera.main.transform.DOMoveX(startChunk.transform.position.x+18, 2);
                spawn = true;
            }
            
            if (transform.gameObject.name == "ColliderEast" && spawn && collision.transform.position.x < transform.position.x)
            {
                Camera.main.transform.DOMoveX(startChunk.transform.position.x + 18, 2);

               chunkChecker = new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y);


                if (!chunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);
                    chunks.Add(test.transform.position);
                }
                if (chunks.Contains(chunkChecker))
                {
                    print("nope");
                  
                }


            }
            
            if (transform.gameObject.name == "ColliderWest" && !spawn)
            {
          test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);

                chunks.Add(test.transform.position);
                

                Camera.main.transform.DOMoveX(startChunk.transform.position.x -18, 2);
                spawn = true;
            }
            if (transform.gameObject.name == "ColliderWest" && spawn && collision.transform.position.x > transform.position.x)
            {
                Camera.main.transform.DOMoveX(startChunk.transform.position.x - 18, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y);

                if (!chunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);
                    chunks.Add(test.transform.position);
                }
                if (chunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }


            if (transform.gameObject.name == "ColliderSouth" && !spawn)
            {
                test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);

                chunks.Add(test.transform.position);


                Camera.main.transform.DOMoveY(startChunk.transform.position.y - 14, 2);
                spawn = true;
            }   
            if (transform.gameObject.name == "ColliderSouth" && spawn && collision.transform.position.y > transform.position.y)
            {
                Camera.main.transform.DOMoveY(startChunk.transform.position.y - 14, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13);

                if (!chunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);
                    chunks.Add(test.transform.position);
                }
                if (chunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }
            if (transform.gameObject.name == "ColliderNorth" && !spawn)
            {
                test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);

                chunks.Add(test.transform.position);


                Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);
                spawn = true;
            }
            if (transform.gameObject.name == "ColliderNorth" && spawn && collision.transform.position.y < transform.position.y)
            {
                Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13);

                if (!chunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    test = Instantiate(chunk1, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);
                    chunks.Add(test.transform.position);
                }
                if (chunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }

        }
        
    }

    
   
}
