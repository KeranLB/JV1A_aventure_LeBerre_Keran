using UnityEngine;

public class ennemi : MonoBehaviour
{
    // follow
    public bool isFollowing;
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public float distance;
    public Vector2 direction;

    private Vector2 infosAnim;

    // attaque
    public int damage = 1;

    // bare de vie
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public bool gotShield;

    //loot drop
    public GameObject PrefabFleche;
    public GameObject PrefabPotion;
    public GameObject PrefabBombe;
    public GameObject Menu;
    public GameObject GameOver;
    public GameObject GameWin;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);
        isFollowing = false;
    }

    void Update()
    {
        if (Menu.activeSelf == false)
        {
            Death();
            if (isFollowing == true)
            {
                Follow();
            }
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
        direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        if (distanceBetween > distance)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            //player.GetComponent<PlayerHealth>().StartCoroutine(player.GetComponent<PlayerHealth>().invunerability()); ;
        }
    }
}
