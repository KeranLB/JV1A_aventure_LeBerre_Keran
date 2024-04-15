using UnityEngine;

public class PlayerInventaire : MonoBehaviour
{
    // inventaire
    public bool gotArc;
    public bool gotKatana;
    public bool gotKey;
    public int numArrow;
    public int numBombe;
    public int numPotion;

    // équipé
    public bool isEquipKatana;
    public bool isEquipArc;
    public bool isEquipBombe;





    void Start()
    {
        gotArc = false;
        gotKatana = false;
        isEquipArc = false;
        isEquipKatana = false;
        isEquipBombe = false;
        numArrow = 0;
        numBombe = 0;
        numPotion = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision avec une fleche collectible
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("vous avez ramasser une flèche.");
            Destroy(collision.gameObject);
            numArrow += 1;
        }

        // collision avec une bombe collectible
        if (collision.CompareTag("Bombe"))
        {
            Debug.Log("vous avez ramasser une bombe.");
            Destroy(collision.gameObject);
            numBombe = numBombe + 1;
        }

        // collision avec l'arc collectible
        if (collision.CompareTag("Arc"))
        {
            Debug.Log("vous avez ramasser un Arc.");
            Destroy(collision.gameObject);
            gotArc = true;
            isEquipArc = true;
            isEquipKatana = false;
        }

        // collision avec le katana collectible
        if (collision.CompareTag("Katana"))
        {
            Debug.Log("vous avez ramasser un Katana.");
            Destroy(collision.gameObject);
            gotKatana = true;
            isEquipKatana = true;
            isEquipArc = false;
        }

        // collision avec clé collectible
        if (collision.CompareTag("key"))
        {
            Debug.Log("vous avez ramasser une clé.");
            Destroy(collision.gameObject);
            gotKey = true;
        }

        // collision avec une potion collectible
        if (collision.CompareTag("Potion"))
        {
            Destroy(collision.gameObject);
            numPotion += 1;
        }
    }
    public void ChangementArme()
    {
        if (gameObject.GetComponent<PlayerController>().isChanging == true)
        {
            // équipe le katana
            if ((gotKatana == true) && (isEquipArc == true))
            {
                isEquipKatana = true;
                isEquipArc = false;
            }

            // équipe l'arc
            else if ((gotArc == true) && (isEquipKatana == true))
            {
                isEquipArc = true;
                isEquipKatana = false;
            }
        }
    }
}
