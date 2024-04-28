using UnityEngine;

public class Katana : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Kappa")) || (collision.CompareTag("Kappa1")) || (collision.CompareTag("Kappa2")) || (collision.CompareTag("Kappa3")) || (collision.CompareTag("Kappa4")))
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

        if ((collision.CompareTag("TenguFantom")) || collision.CompareTag("TenguFantom1"))
        {
            collision.GetComponent<ennemi>().TakeDamage(100);
        }
    }
}
