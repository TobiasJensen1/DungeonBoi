using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ProcedualGeneration : MonoBehaviour
{
    //Stores chunk to instantiate
    GameObject chunkToSpawn;

    
    

    
    //Used to check if space is empty
    Vector2 chunkChecker;

    //EnemySpawning
    Vector2 Enemy_spawner;
    float Randomizer_Enemy;

    public GameObject spider;
    //4Way Chunks
    public GameObject startChunk;
    public GameObject ConnecterChunk1;
    public GameObject ConnecterChunk2;
    public GameObject ConnecterChunk3;

    //2way chunks
    public GameObject EastWestChunk1;
    public GameObject EastWestChunk2;
    public GameObject NorthSouthChunk1;

    //Used for list[rand]
    int chunkConnecter_rng; 
    //Lists
    static List<Vector2> ConnecterChunks = new List<Vector2>();
    static List<GameObject> spawnedChunks = new List<GameObject>();
    List<GameObject> chunkAmount = new List<GameObject>();
    public static List<Vector2> blockadeCheckerEastWest = new List<Vector2>();
    public static List<Vector2> blockadeCheckerNorthSouth = new List<Vector2>();



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

        if (collision.transform.tag == "Player")
        {
            onCollisionEast(collision);
            onCollisionWest(collision);
            onCollisionNorth(collision);
            onCollisionSouth(collision);
        }
    }

    public void onCollisionEast(Collider2D collision)
    {
        if (transform.gameObject.name == "ColliderEast" && collision.transform.position.x < transform.position.x)
        {
            Camera.main.transform.DOMoveX(startChunk.transform.position.x + 17, 2);
            chunkChecker = new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y);

            if (!ConnecterChunks.Contains(chunkChecker))
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                
                chunkToSpawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);

                //ikke færdig
                /*
                for(int i = 0; i < spawnedChunks.Count; i++)
                {
                    if(spawnedChunks[i].transform.position ==  )
                }
                */

                //If eastWest room spawned, save coordinates next to it to prevent 4way spawning
                if (chunkToSpawn.transform.tag == "4Way") {
                    
                    print(chunkToSpawn.transform.position);
                    if (blockadeCheckerEastWest.Contains(chunkToSpawn.transform.position))
                    {
                        chunkToSpawn.transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                        chunkToSpawn.transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                    }
                }


                if (chunkToSpawn.transform.tag == "EastWest")
                {
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y + 13));
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y - 13));
                }

                //rng for spawn af enemies
                enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
            }

        }
    }
        public void onCollisionWest(Collider2D collision)
        {
        if (transform.gameObject.name == "ColliderWest" && collision.transform.position.x > transform.position.x)
        {
            Camera.main.transform.DOMoveX(startChunk.transform.position.x - 17, 2);

            chunkChecker = new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y);

            if (!ConnecterChunks.Contains(chunkChecker))
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                print("kun 1 gang");
                chunkToSpawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);

                //If eastWest room spawned, save coordinates next to it to prevent 4way spawning

                if (chunkToSpawn.transform.tag == "4Way")
                {

                    print(chunkToSpawn.transform.position);
                    if (blockadeCheckerEastWest.Contains(chunkToSpawn.transform.position))
                    {
                        chunkToSpawn.transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                        chunkToSpawn.transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                    }
                }


                if (chunkToSpawn.transform.tag == "EastWest")
                {
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y + 13));
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y - 13));

                }

                //rng for spawn af enemies
                enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
            }
        }
    }

    public void onCollisionNorth(Collider2D collision)
    {

        if (transform.gameObject.name == "ColliderNorth" && collision.transform.position.y < transform.position.y)
        {
            chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
            Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);

            chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13);

            if (!ConnecterChunks.Contains(chunkChecker))
            {
                print("kun 1 gang");
                chunkToSpawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);

                if (chunkToSpawn.transform.tag == "4Way")
                {

                    print(chunkToSpawn.transform.position);
                    if (blockadeCheckerNorthSouth.Contains(chunkToSpawn.transform.position))
                    {
                        chunkToSpawn.transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                        chunkToSpawn.transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                    }
                }


                if (chunkToSpawn.transform.tag == "NorthSouth")
                {
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x +17, chunkToSpawn.transform.position.y));
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x -17, chunkToSpawn.transform.position.y));
                }

                //rng for spawn af enemies
                enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
            }
        }
    }

    public void onCollisionSouth(Collider2D collision)
    {
        if (transform.gameObject.name == "ColliderSouth" && collision.transform.position.y > transform.position.y)
        {
            Camera.main.transform.DOMoveY(startChunk.transform.position.y - 14, 2);

            chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13);

            if (!ConnecterChunks.Contains(chunkChecker))
            {
                chunkConnecter_rng = Random.Range(0, chunkAmount.Count);
                print("kun 1 gang");
                chunkToSpawn = Instantiate(chunkAmount[chunkConnecter_rng].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);

                if (chunkToSpawn.transform.tag == "4Way")
                {

                    print(chunkToSpawn.transform.position);
                    if (blockadeCheckerNorthSouth.Contains(chunkToSpawn.transform.position))
                    {
                        chunkToSpawn.transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                        chunkToSpawn.transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                    }
                }


                if (chunkToSpawn.transform.tag == "NorthSouth")
                {
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x +17, chunkToSpawn.transform.position.y));
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x -17, chunkToSpawn.transform.position.y));
                }

                //rng for spawn af enemies
                enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
            }
        }
    }
    public void enemySpawner()
    {

        if (chunkToSpawn.transform.Find("EnemySpawner") != null)
        {

            for (int i = 0; i < 2; i++)
            {
                Randomizer_Enemy = Random.Range(0, 2);

                Enemy_spawner = chunkToSpawn.transform.Find("EnemySpawner").GetChild(i).position;
                print(Randomizer_Enemy);
                if (Randomizer_Enemy == 1)
                {
                    Instantiate(spider, new Vector2(Enemy_spawner.x, Enemy_spawner.y), Quaternion.identity);
                }
                print(Enemy_spawner);
            }
        }
    }
}
