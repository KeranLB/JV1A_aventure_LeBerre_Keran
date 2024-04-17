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
    private int indiceArmePrincipal;
    public bool isEquipKatana;
    public bool isEquipArc;
    public bool isEquipBombe;





    void Start()
    {
        gotArc = false;
        gotKatana = false;
        gotKey = false;
        indiceArmePrincipal = 3;
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
            indiceArmePrincipal = 2;
        }

        // collision avec le katana collectible
        if (collision.CompareTag("Katana"))
        {
            Debug.Log("vous avez ramasser un Katana.");
            Destroy(collision.gameObject);
            gotKatana = true;
            indiceArmePrincipal = 1;
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

    void ChangementArmeUp()
    {
        indiceArmePrincipal += 1;

        if (indiceArmePrincipal >= 4)
        {
            indiceArmePrincipal = 1;
        }

        if ((gotArc == false) && (indiceArmePrincipal == 2))
        {
            indiceArmePrincipal = 3;
        }

        else if ((gotKatana == false) && (gotArc == false) && (indiceArmePrincipal == 1))
        {
            indiceArmePrincipal = 3;
        }

        else if ((gotKatana == false) && (gotArc == true) && (indiceArmePrincipal == 1))
        {
            indiceArmePrincipal = 2;
        }


    }

    void ChangementArmeDown()
    {
        indiceArmePrincipal -= 1;

        if (indiceArmePrincipal <= 0)
        {
            indiceArmePrincipal = 3;
        }

        if ((gotKatana == false) && (indiceArmePrincipal == 1))
        {
            indiceArmePrincipal = 3;
        }

        else if ((gotArc == false) && (gotKatana == false) && (indiceArmePrincipal == 2))
        {
            indiceArmePrincipal = 3;
        }

        else if ((gotArc == false) && (gotKatana == true) && (indiceArmePrincipal == 2))
        {
            indiceArmePrincipal = 1;
        }
    }
    public void ChangementArme()
    {

        if (gameObject.GetComponent<PlayerController>().isChangingUp == true)
        {
            ChangementArmeUp();
        }

        if (gameObject.GetComponent<PlayerController>().isChangingDown == true)
        {
            ChangementArmeDown();
        }

        // équipe le katana
        else if (indiceArmePrincipal == 1)
        {
            isEquipKatana = true;
            isEquipArc = false;
            isEquipBombe = false;
        }

        // équipe l'arc
        else if (indiceArmePrincipal == 2)
        {
            isEquipArc = true;
            isEquipKatana = false;
            isEquipBombe = false;
        }

        // équipe les bombes
        else if (indiceArmePrincipal == 3)
        {
            isEquipBombe = true;
            isEquipKatana = false;
            isEquipArc = false;
        }
    }
}
