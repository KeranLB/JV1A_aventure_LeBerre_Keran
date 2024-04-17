using UnityEngine;

public class ennemi : MonoBehaviour
{
    // attaque
    public int damage = 1;
    public GameObject player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
