using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // UI 
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public bool dead;

    public bool isHealing;

    /*
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    */

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
        dead = false;
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
            dead = true;
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
        //StartCoroutine(invunerability());
    }

    /*
    public IEnumerator invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
    */
}
