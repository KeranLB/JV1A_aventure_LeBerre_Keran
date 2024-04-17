using UnityEngine;

public class fleche : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ennemi"))
        {
            if (collision.GetComponent<triggerZone>().gotShield == true)
            {
                Destroy(gameObject);
                collision.GetComponent<triggerZone>().TakeDamage(10);
            }

            else
            {
                Destroy(gameObject);
                collision.GetComponent<triggerZone>().TakeDamage(25);
            }
        }
    }
}
