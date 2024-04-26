using UnityEngine;

public class Katana : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kappa"))
        {
            if (collision.GetComponent<ennemi>().gotShield == true)
            {
                collision.GetComponent<ennemi>().TakeDamage(5);
            }

            else
            {
                collision.GetComponent<ennemi>().TakeDamage(50);
            }
        }

        if (collision.CompareTag("Tengu"))
        {
            collision.GetComponent<ennemi>().TakeDamage(5);
        }

        if (collision.CompareTag("TenguFantom"))
        {
            collision.GetComponent<ennemi>().TakeDamage(100);
        }
    }
}
