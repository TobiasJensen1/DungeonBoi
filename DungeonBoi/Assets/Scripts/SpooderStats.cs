using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpooderStats : MonoBehaviour
{
    public HealthBar healthBar;

    public float health;
    public float damage;
    public float xp;

    float maxHealth;

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.setHealth(health / maxHealth);
    }
}
