using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpooderMove : MonoBehaviour
{


    GameObject player;
    float distanceToPlayer;

    float spooderDmg;
    float playerDmg;

    float spooderHealth;
    float playerHealth;

    bool combat;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spooderDmg = GetComponent<SpooderStats>().damage;
        playerDmg = player.GetComponent<PlayerStats>().damage;
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

        if(distanceToPlayer <= 2 && !combat)
        {
            
            StartCoroutine("spooderAttack");
            StartCoroutine("playerAttack");
            combat = true;
            
        }
        else if(distanceToPlayer > 2)
        {
            StopCoroutine("spooderAttack");
            StopCoroutine("playerAttack");
            combat = false;
        }

        if(GetComponent<SpooderStats>().health <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
        if (player.GetComponent<PlayerStats>().health <= 0)
        {
            Destroy(player, 0.5f);
            SceneManager.LoadScene("DungeonBoi");
        }
        
    }
    

    IEnumerator spooderAttack()
    {
        while (true)
        {
            player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().health - spooderDmg;
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator playerAttack()
    {
        while (true)
        {
            GetComponent<SpooderStats>().health = GetComponent<SpooderStats>().health - playerDmg;
            yield return new WaitForSeconds(1);
        }
    }


}
