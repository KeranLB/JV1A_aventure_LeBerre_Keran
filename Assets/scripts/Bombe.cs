using UnityEngine;

public class Bombe : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ennemi"))
        {
            if (collision.GetComponent<ennemi>().gotShield == true)
            {
                collision.GetComponent<ennemi>().TakeDamage(50);
                Destroy(gameObject);
                collision.GetComponent<ennemi>().gotShield = false;
            }

            else
            {
                collision.GetComponent<ennemi>().TakeDamage(100);
                Destroy(gameObject);
            }
        }
    }
}
