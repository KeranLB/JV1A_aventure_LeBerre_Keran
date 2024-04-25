using UnityEngine;

public class fleche : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kappa"))
        {
            if (collision.GetComponent<ennemi>().gotShield == true)
            {
                Destroy(gameObject);
                collision.GetComponent<ennemi>().TakeDamage(10);
            }

            else
            {
                Destroy(gameObject);
                collision.GetComponent<ennemi>().TakeDamage(25);
            }
        }
    }
}
