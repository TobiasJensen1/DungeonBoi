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
    static int chunkRandom1;
    static int chunkRandom2;
    //Lists
    static List<Vector2> ConnecterChunks = new List<Vector2>();
    static List<GameObject> spawnedChunks = new List<GameObject>();
    List<GameObject> chunkAmount = new List<GameObject>();
    public static List<Vector2> blockadeCheckerEastWest = new List<Vector2>();
    public static List<Vector2> blockadeCheckerNorthSouth = new List<Vector2>();

    public static List<GameObject> allWayChunks = new List<GameObject>();
    public static List<GameObject> eastWestChunks = new List<GameObject>();
    public static List<GameObject> northSouthChunks = new List<GameObject>();



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

                chunkRandom1 = Random.Range(0, chunkAmount.Count);

                while (chunkRandom1 == chunkRandom2)
                {
                    chunkRandom1 = Random.Range(0, chunkAmount.Count);
                }

                chunkToSpawn = Instantiate(chunkAmount[chunkRandom1].gameObject, new Vector2(startChunk.transform.position.x + 17, startChunk.transform.position.y), Quaternion.identity);
                chunkRandom2 = chunkRandom1;
            }

            if (chunkToSpawn.tag == "4Way")
            {
                allWayChunks.Add(chunkToSpawn);
            }


            if (chunkToSpawn.tag == "EastWest")
            {

                eastWestChunks.Add(chunkToSpawn);
                blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y + 13));
                blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y - 13));
            }


            for (int i = 0; i < allWayChunks.Count; i++)
            {
                for (int j = 0; j < eastWestChunks.Count; j++)
                {
                    if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y - 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                    {
                        allWayChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                    }
                    if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y + 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                    {
                        allWayChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                    }
                }
                for (int k = 0; k < northSouthChunks.Count; k++)
                {
                    if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x - 17)
                    {
                        allWayChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                    }
                    if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x + 17)
                    {
                        allWayChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                    }
                }

            }

            for (int i = 0; i < eastWestChunks.Count; i++)
            {
                for (int j = 0; j < northSouthChunks.Count; j++)
                {
                    if (eastWestChunks[i].transform.position.x + 17 == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                    }
                    if (eastWestChunks[i].transform.position.x - 17 == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                    }
                }
            }
            //rng for spawn af enemies
            enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
                chunkToSpawn = null;
            
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
                chunkRandom1 = Random.Range(0, chunkAmount.Count);

                while (chunkRandom1 == chunkRandom2)
                {
                    chunkRandom1 = Random.Range(0, chunkAmount.Count);
                }
                chunkToSpawn = Instantiate(chunkAmount[chunkRandom1].gameObject, new Vector2(startChunk.transform.position.x - 17, startChunk.transform.position.y), Quaternion.identity);
                chunkRandom2 = chunkRandom1;

            }
                if (chunkToSpawn.tag == "4Way")
                {
                    allWayChunks.Add(chunkToSpawn);
                }
                
                if (chunkToSpawn.transform.tag == "EastWest")
                {
                    eastWestChunks.Add(chunkToSpawn);
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y + 13));
                    blockadeCheckerEastWest.Add(new Vector2(chunkToSpawn.transform.position.x, chunkToSpawn.transform.position.y - 13));
                }


                for (int i = 0; i < allWayChunks.Count; i++)
                {
                    for (int j = 0; j < eastWestChunks.Count; j++)
                    {
                        if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y - 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                        {
                            allWayChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                        }
                        if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y + 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                        {
                            allWayChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                        }
                    }
                    for (int k = 0; k < northSouthChunks.Count; k++)
                    {
                        if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x - 17)
                        {
                            allWayChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                        }
                        if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x + 17)
                        {
                            allWayChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                        }
                    }
                }

            for (int i = 0; i < eastWestChunks.Count; i++)
            {
                for (int j = 0; j < northSouthChunks.Count; j++)
                {
                    if (eastWestChunks[i].transform.position.x + 17 == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                    }
                    if (eastWestChunks[i].transform.position.x - 17 == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                    }
                }
            }






            //rng for spawn af enemies
            enemySpawner();
            ConnecterChunks.Add(chunkToSpawn.transform.position);
            spawnedChunks.Add(chunkToSpawn);
            chunkToSpawn = null;
        }
    }


    public void onCollisionNorth(Collider2D collision)
    {

        if (transform.gameObject.name == "ColliderNorth" && collision.transform.position.y < transform.position.y)
        {



            
            Camera.main.transform.DOMoveY(startChunk.transform.position.y + 12, 2);

            chunkChecker = new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13);

            if (!ConnecterChunks.Contains(chunkChecker))
            {

                chunkRandom1 = Random.Range(0, chunkAmount.Count);

                while (chunkRandom1 == chunkRandom2)
                {
                    chunkRandom1 = Random.Range(0, chunkAmount.Count);
                }
                chunkToSpawn = Instantiate(chunkAmount[chunkRandom1].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y + 13), Quaternion.identity);

                chunkRandom2 = chunkRandom1;

            }


                if (chunkToSpawn.tag == "4Way")
                {
                    allWayChunks.Add(chunkToSpawn);
                }



                if (chunkToSpawn.transform.tag == "NorthSouth")
                {
                    northSouthChunks.Add(chunkToSpawn);
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x + 17, chunkToSpawn.transform.position.y));
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x - 17, chunkToSpawn.transform.position.y));
                }

                    for (int i = 0; i < allWayChunks.Count; i++)
                    {
                        for (int j = 0; j < eastWestChunks.Count; j++)
                        {
                            if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y - 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                            }
                            if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y + 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                            }
                        }
                        for (int k = 0; k < northSouthChunks.Count; k++)
                        {
                            if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x - 17)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                            }
                            if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x + 17)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                            }
                        }
                    }

            for (int i = 0; i < eastWestChunks.Count; i++)
            {
                for (int j = 0; j < northSouthChunks.Count; j++)
                {
                    if (eastWestChunks[i].transform.position.x == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y - 13)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                    }
                    if (eastWestChunks[i].transform.position.x == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y + 13)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                    }
                }
            }

            //rng for spawn af enemies
            enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
                chunkToSpawn = null;
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


                chunkRandom1 = Random.Range(0, chunkAmount.Count);

                while (chunkRandom1 == chunkRandom2)
                {
                    chunkRandom1 = Random.Range(0, chunkAmount.Count);
                }
                chunkToSpawn = Instantiate(chunkAmount[chunkRandom1].gameObject, new Vector2(startChunk.transform.position.x, startChunk.transform.position.y - 13), Quaternion.identity);
                chunkRandom2 = chunkRandom1;

                }


                if (chunkToSpawn.tag == "4Way")
                {
                    allWayChunks.Add(chunkToSpawn);
                }

                if (chunkToSpawn.transform.tag == "NorthSouth")
                {
                    northSouthChunks.Add(chunkToSpawn);
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x + 17, chunkToSpawn.transform.position.y));
                    blockadeCheckerNorthSouth.Add(new Vector2(chunkToSpawn.transform.position.x - 17, chunkToSpawn.transform.position.y));
                }

                    for (int i = 0; i < allWayChunks.Count; i++)
                    {
                        for (int j = 0; j < eastWestChunks.Count; j++)
                        {
                            if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y - 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                            }
                            if (eastWestChunks[j].transform.position.y == allWayChunks[i].transform.position.y + 13 && eastWestChunks[j].transform.position.x == allWayChunks[i].transform.position.x)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                            }
                        }
                        for (int k = 0; k < northSouthChunks.Count; k++)
                        {
                            if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x - 17)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("West").transform.gameObject.SetActive(true);
                            }
                            if (northSouthChunks[k].transform.position.y == allWayChunks[i].transform.position.y && northSouthChunks[k].transform.position.x == allWayChunks[i].transform.position.x + 17)
                            {
                                allWayChunks[i].transform.Find("Blockades").transform.Find("East").transform.gameObject.SetActive(true);
                            }
                        }
                    }

            for (int i = 0; i < eastWestChunks.Count; i++)
            {
                for (int j = 0; j < northSouthChunks.Count; j++)
                {
                    if (eastWestChunks[i].transform.position.x == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y - 13)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("South").transform.gameObject.SetActive(true);
                    }
                    if (eastWestChunks[i].transform.position.x == northSouthChunks[j].transform.position.x && eastWestChunks[i].transform.position.y == northSouthChunks[j].transform.position.y + 13)
                    {
                        eastWestChunks[i].transform.Find("Blockades").transform.Find("North").transform.gameObject.SetActive(true);
                    }
                }
            }




            //rng for spawn af enemies
            enemySpawner();
                ConnecterChunks.Add(chunkToSpawn.transform.position);
                spawnedChunks.Add(chunkToSpawn);
                chunkToSpawn = null;
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
                if (Randomizer_Enemy == 1)
                {
                    Instantiate(spider, new Vector2(Enemy_spawner.x, Enemy_spawner.y), Quaternion.identity);
                }
            }
        }
    }
}
