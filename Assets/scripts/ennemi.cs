using UnityEngine;

public class ennemi : MonoBehaviour
{
    // follow
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private float distance;

    //loot drop
    public GameObject PrefabFleche;
    public GameObject PrefabPotion;
    public GameObject PrefabBombe;

    // attaque
    public int damage = 1;
    
    // bare de vie
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public bool gotShield;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
    }

    void Update()
    {
        Death();

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        //float angle = Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg;
        /*
        if (distanceBetween > distance)
        {
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        */
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Death()
    {
        if (currentHealth <= 0)
        {
            GameObject arrowCollectible = Instantiate(PrefabFleche, transform.position + new Vector3(1.0f,1.0f,0.0f), Quaternion.identity);
            GameObject BombeCollectible = Instantiate(PrefabBombe, transform.position + new Vector3(-1.0f, -1.0f, 0.0f), Quaternion.identity);
            GameObject PotionCollectible = Instantiate(PrefabPotion, transform.position + new Vector3(1.0f, -1.0f, 0.0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
