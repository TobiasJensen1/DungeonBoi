using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public HealthBar healthBar;
    public HealthBar guiHealth;
    public XpBar xpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;

    public float maxHealth;
    public float health;

    public float damage;

    public float playerXp;
    public float maxXp;
    float carriedXp;

    public float level;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        maxHealth = health;
        maxXp = 10;
    }

    // Update is called once per frame
    void Update()
    {

        
        healthBar.setHealth(health / maxHealth);
        guiHealth.setHealth(health / maxHealth);

        levelText.text = "" + level;
        healthText.text = health + "/" + maxHealth;

        xpBar.setXp(playerXp / maxXp);

        updateLevel();

    }

    void updateLevel()
    {
        if(playerXp >= maxXp)
        {
            level++;
            carriedXp = playerXp % maxXp;

            playerXp = carriedXp;
            maxXp = level * 15;

            maxHealth = maxHealth + 10;
            health = maxHealth;

            damage++;
        }
    }    
}
