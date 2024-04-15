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


    public bool isHealing;

    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
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
        if ((isHealing == true) && (gameObject.GetComponent<PlayerInventaire>().numPotion > 0) && (currentHealth < maxHealth))
        {
            gameObject.GetComponent<PlayerInventaire>().numPotion -= 1;
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
}
