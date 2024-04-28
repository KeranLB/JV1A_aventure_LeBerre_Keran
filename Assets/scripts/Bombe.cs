using UnityEngine;

public class Bombe : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Kappa")) || (collision.CompareTag("Kappa1")) || (collision.CompareTag("Kappa2")) || (collision.CompareTag("Kappa3")) || (collision.CompareTag("Kappa4")))
        {
            if (collision.GetComponent<ennemi>().gotShield == true)
            {
                collision.GetComponent<ennemi>().TakeDamage(10);
                Destroy(gameObject);
                collision.GetComponent<ennemi>().gotShield = false;
            }

            else
            {
                collision.GetComponent<ennemi>().TakeDamage(5);
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Tengu"))
        {
            collision.GetComponent<ennemi>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
