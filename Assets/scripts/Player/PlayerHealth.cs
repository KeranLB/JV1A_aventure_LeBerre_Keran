using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // UI 
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int numPotion;
    public bool isHealing;

    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
        numPotion = 0;
    }

    void Update()
    {
        Death();
        Heal();
    }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        if ((isHealing == true) && (numPotion > 0) && (currentHealth < maxHealth))
        {
            numPotion -= 1;
            if ((currentHealth + 30) > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += 30;
            }
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision avec une potion collectible
        if (collision.CompareTag("Potion"))
        {
            Destroy(collision.gameObject);
            numPotion += 1;
        }
    }
}
