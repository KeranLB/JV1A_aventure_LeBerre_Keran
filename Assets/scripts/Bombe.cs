using UnityEngine;

public class Bombe : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ennemi"))
        {
            if (collision.GetComponent<triggerZone>().gotShield == true)
            {
                collision.GetComponent<triggerZone>().TakeDamage(50);
                Destroy(gameObject);
                collision.GetComponent<triggerZone>().gotShield = false;
            }

            else
            {
                collision.GetComponent<triggerZone>().TakeDamage(100);
                Destroy(gameObject);
            }
        }
    }
}
