using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Player
    public GameObject player;

    // image
    public Image arc;
    public Image Katana;
    public Image slotPotion;

    // Start is called before the first frame update
    void Start()
    {
        arc.GetComponent<Image>().enabled = false;
        Katana.GetComponent<Image>().enabled = false;
        slotPotion.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerInventaire>().isEquipArc)
        {
            arc.GetComponent<Image>().enabled = true;
            Katana.GetComponent<Image>().enabled = false;
        }
        else if (player.GetComponent<PlayerInventaire>().isEquipKatana)
        {
            arc.GetComponent<Image>().enabled = false;
            Katana.GetComponent<Image>().enabled = true;
        }

        if (player.GetComponent<PlayerInventaire>().numPotion > 0)
        {
            slotPotion.GetComponent<Image>().enabled = true;
        }
        slotPotion.transform.GetChild(1).GetComponent<Text>().text = (player.GetComponent<PlayerInventaire>().numPotion + "");

    }
}
