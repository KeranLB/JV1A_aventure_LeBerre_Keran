using UnityEngine;

public class triggerZone : MonoBehaviour
{
    // follow
    public bool isFollowing;
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private float distance;

    // bare de vie
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public bool gotShield;

    //loot drop
    public GameObject PrefabFleche;
    public GameObject PrefabPotion;
    public GameObject PrefabBombe;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
        isFollowing = false;
    }

    void Update()
    {
        Death();
        if (isFollowing == true)
        {
            Follow();
        }
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
            GameObject arrowCollectible = Instantiate(PrefabFleche, transform.position + new Vector3(1.0f, 1.0f, 0.0f), Quaternion.identity);
            GameObject BombeCollectible = Instantiate(PrefabBombe, transform.position + new Vector3(-1.0f, -1.0f, 0.0f), Quaternion.identity);
            GameObject PotionCollectible = Instantiate(PrefabPotion, transform.position + new Vector3(1.0f, -1.0f, 0.0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void Follow()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distanceBetween > distance)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = false;
        }
    }
}
