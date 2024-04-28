using UnityEngine;

public class fleche : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( (collision.CompareTag("Kappa")) || (collision.CompareTag("Kappa1")) || (collision.CompareTag("Kappa2")) || (collision.CompareTag("Kappa3")) || (collision.CompareTag("Kappa4")))
        {
            if (collision.GetComponent<ennemi>().gotShield == true)
            {
                Destroy(gameObject);
                collision.GetComponent<ennemi>().TakeDamage(5);
            }

            else
            {
                Destroy(gameObject);
                collision.GetComponent<ennemi>().TakeDamage(25);
            }
        }

        if (collision.CompareTag("Tengu"))
        {
            collision.GetComponent<ennemi>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
