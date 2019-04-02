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
                Destroy(player);
                SceneManager.LoadScene("DungeonBoi");
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


                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1);
        }
    }


}
