using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour
{


    GameObject player;

    float distanceToPlayer;

    float playerXp;
    float spooderXp;

    float spooderDmg;
    float playerDmg;

    float spooderHealth;
    float playerHealth;

    float xp;

    bool combat;
    bool firstKill;
    static bool firstKilled;

    public Sprite noheart;

    GameObject spawnedItem;
    public static List<GameObject> itemsOnFloor = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

       
            spooderDmg = GetComponent<SpooderStats>().damage;
            playerDmg = player.GetComponent<PlayerStats>().damage;
        
        xp = player.GetComponent<PlayerStats>().playerXp;
        spooderXp = GetComponent<SpooderStats>().xp;
    }

    // Update is called once per frame
    void Update()
    {


        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 6)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2 * Time.fixedDeltaTime);
            Vector3 flip = player.transform.position - transform.position;
            if (flip.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (flip.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (distanceToPlayer <= 2 && !combat)
        {

            StartCoroutine("spooderAttack");
            StartCoroutine("playerAttack");
            combat = true;

        }
        else if (distanceToPlayer > 2)
        {
            StopCoroutine("spooderAttack");
            StopCoroutine("playerAttack");
            combat = false;
        } 
    }
    

    IEnumerator spooderAttack()
    {
        while (true)
        {
            player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().health - spooderDmg;
            if (player.GetComponent<PlayerStats>().health <= 0)
            {

                if (GameObject.Find("Player").GetComponent<PlayerStats>().revive)
                {
                    GameObject.Find("Player").GetComponent<PlayerStats>().health = GameObject.Find("Player").GetComponent<PlayerStats>().maxHealth;
                    GameObject.Find("Main Camera").transform.Find("UiHealth").transform.Find("Health").transform.Find("HealthBorder").transform.Find("Canvas").transform.Find("Revive").GetComponent<SpriteRenderer>().sprite = noheart;
                } else { 

                Destroy(player);
                SceneManager.LoadScene("DungeonBoi");
                }
            }
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator playerAttack()
    {
        while (true)
        {
            GetComponent<SpooderStats>().health = GetComponent<SpooderStats>().health - playerDmg;
            if(GetComponent<SpooderStats>().health <= 0)
            {
                    
                
                player.GetComponent<PlayerStats>().playerXp = player.GetComponent<PlayerStats>().playerXp + spooderXp;

                firstKill = true;
                dropItem(gameObject);

                if(gameObject.name == "Spider(Clone)")
                {
                    GameObject.Find("Player").GetComponent<PlayerStats>().coins += 10;
                } else
                {
                    GameObject.Find("Player").GetComponent<PlayerStats>().coins += 25;
                }

                Destroy(gameObject, 0f);

                

            }
            yield return new WaitForSeconds(1);
        }
    }

    void dropItem(GameObject enemyKilled)
    {
        if (firstKill && !firstKilled)
        {
            firstKilled = true;
            spawnedItem = Instantiate(Resources.Load<GameObject>("Heart"), new Vector2(enemyKilled.transform.position.x, enemyKilled.transform.position.y), Quaternion.identity);
            itemsOnFloor.Add(spawnedItem);
        }
    }



}
