using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProcedualGeneration : MonoBehaviour
{
    GameObject chunk_to_spawn;

    float Randomizer_Enemy;
    int chunkConnecter_rng;

    Vector2 Enemy_spawner;

    Vector3 camera_Pos;

    Vector2 chunkChecker;

    public GameObject spider;
    //Chunks
    public GameObject startChunk;
    public GameObject ConnecterChunk1;
    public GameObject ConnecterChunk2;
    public GameObject ConnecterChunk3;

    //one way chunks
    public GameObject EastWestChunk1;
    public GameObject EastWestChunk2;
    public GameObject NorthSouthChunk1;


    static bool spawn = false;
   static List<Vector2> ConnecterChunks = new List<Vector2>();
     List<GameObject> chunkAmount = new List<GameObject>();


   
    // Start is called before the first frame update
    void Start()
    {
        if (transform.name == "ColliderEast" || transform.name == "ColliderWest")
        {
            chunkAmount.Add(ConnecterChunk1);
            chunkAmount.Add(ConnecterChunk2);
            chunkAmount.Add(ConnecterChunk3);
            chunkAmount.Add(EastWestChunk1);
            chunkAmount.Add(EastWestChunk2);
           
        }
        if (transform.name == "ColliderNorth" || transform.name == "ColliderSouth")
        {
            chunkAmount.Add(ConnecterChunk1);
            chunkAmount.Add(ConnecterChunk2);
            chunkAmount.Add(ConnecterChunk3);
            chunkAmount.Add(NorthSouthChunk1);
           
        }
        ConnecterChunks.Add(startChunk.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        GameObject objectToPlace = ConnecterChunk1;

        
   
        
        if(collision.transform.tag == "Player")
        {
            if(transform.gameObject.name == "ColliderEast" && !spawn) {

                MultiWay_Chunk_Spawner();
            }
            
            if (transform.gameObject.name == "ColliderEast" && spawn && collision.transform.position.x < transform.position.x)
            {

                
              Camera.main.transform.DOMoveX(startChunk.transform.position.x + 17, 2);
                

                chunkChecker = new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y);


                if (!ConnecterChunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    chunk_to_spawn = Instantiate(ConnecterChunk1, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);
                  

                    //rng for spawn af enemies
                   // enemeySpawner();


                    ConnecterChunks.Add(chunk_to_spawn.transform.position);
                }
                if (ConnecterChunks.Contains(chunkChecker))
                {
                    
                  
                }


            }
            
            if (transform.gameObject.name == "ColliderWest" && !spawn)
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);

                ConnecterChunks.Add(chunk_to_spawn.transform.position);
                

                Camera.main.transform.DOMoveX(startChunk.transform.position.x -17, 2);
                spawn = true;
            }
            if (transform.gameObject.name == "ColliderWest" && spawn && collision.transform.position.x > transform.position.x)
            {
                Camera.main.transform.DOMoveX(startChunk.transform.position.x - 17, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y);

                if (!ConnecterChunks.Contains(chunkChecker))
                {
                    chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                    print("kun 1 gang");
                    chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);
                    ConnecterChunks.Add(chunk_to_spawn.transform.position);
                }
                if (ConnecterChunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }


            if (transform.gameObject.name == "ColliderSouth" && !spawn)
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);

                ConnecterChunks.Add(chunk_to_spawn.transform.position);


                Camera.main.transform.DOMoveY(startChunk.transform.position.y - 14, 2);
                spawn = true;
            }   
            if (transform.gameObject.name == "ColliderSouth" && spawn && collision.transform.position.y > transform.position.y)
            {
                Camera.main.transform.DOMoveY(startChunk.transform.position.y - 14, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13);

                if (!ConnecterChunks.Contains(chunkChecker))
                {
                    chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                    print("kun 1 gang");
                    chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);
                    ConnecterChunks.Add(chunk_to_spawn.transform.position);
                }
                if (ConnecterChunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }
            if (transform.gameObject.name == "ColliderNorth" && !spawn)
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);

                ConnecterChunks.Add(chunk_to_spawn.transform.position);


                Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);
                spawn = true;
            }
            if (transform.gameObject.name == "ColliderNorth" && spawn && collision.transform.position.y < transform.position.y)
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);

                chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13);

                if (!ConnecterChunks.Contains(chunkChecker))
                {
                    print("kun 1 gang");
                    chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);
                    ConnecterChunks.Add(chunk_to_spawn.transform.position);
                }
                if (ConnecterChunks.Contains(chunkChecker))
                {
                    print("nope");

                }

            }

        }
        
    }

    public void enemeySpawner()
    {
        for (int i = 0; i < 2; i++)
        {
            Randomizer_Enemy = Random.Range(0, 2);
            Enemy_spawner = chunk_to_spawn.transform.Find("EnemySpawner").GetChild(i).position;
            print(Randomizer_Enemy);
            if (Randomizer_Enemy == 1)
            {
                Instantiate(spider, new Vector2(Enemy_spawner.x, Enemy_spawner.y), Quaternion.identity);
            }
            print(Enemy_spawner);
        }
    }

    public void MultiWay_Chunk_Spawner()
    {
        chunkConnecter_rng = Random.Range(0, chunkAmount.Count);

        chunk_to_spawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);


     //   enemeySpawner();

        ConnecterChunks.Add(chunk_to_spawn.transform.position);


        camera_Pos = chunk_to_spawn.transform.Find("CameraPoint").transform.position;

        Camera.main.transform.DOMoveX(camera_Pos.x, 2);
        spawn = true;
    } 
   
    
    

}
