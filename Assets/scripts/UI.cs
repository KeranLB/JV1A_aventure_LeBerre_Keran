using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Player
    public GameObject player;

    // image
    [SerializeField]
    public Image arc;
    [SerializeField]
    public Image Katana;
    [SerializeField]
    public Image Bombe;
    [SerializeField]
    public Image slotPotion;
    [SerializeField]
    public Image Key;

    // Start is called before the first frame update
    void Start()
    {
        arc.GetComponent<Image>().enabled = false;
        Katana.GetComponent<Image>().enabled = false;
        Bombe.GetComponent<Image>().enabled = false;
    }

    private void ChangementArme()
    {
        if (player.GetComponent<PlayerInventaire>().isEquipArc)
        {
            arc.GetComponent<Image>().enabled = true;
            arc.transform.GetChild(0).GetComponent<Text>().enabled = true;
            arc.transform.GetChild(1).GetComponent<Image>().enabled = true;
            Katana.GetComponent<Image>().enabled = false;
            Bombe.transform.GetChild(1).GetComponent<Image>().enabled = false;
            Bombe.transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
        else if (player.GetComponent<PlayerInventaire>().isEquipKatana)
        {
            arc.GetComponent<Image>().enabled = false;
            arc.transform.GetChild(0).GetComponent<Text>().enabled = false;
            arc.transform.GetChild(1).GetComponent<Image>().enabled = false;
            Katana.GetComponent<Image>().enabled = true;
            Bombe.transform.GetChild(1).GetComponent<Image>().enabled = false;
            Bombe.transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
        else if (player.GetComponent<PlayerInventaire>().isEquipBombe)
        {
            arc.GetComponent<Image>().enabled = false;
            arc.transform.GetChild(0).GetComponent<Text>().enabled = false;
            arc.transform.GetChild(1).GetComponent<Image>().enabled = false;
            Katana.GetComponent<Image>().enabled = false;
            Bombe.transform.GetChild(1).GetComponent<Image>().enabled = true;
            Bombe.transform.GetChild(0).GetComponent<Text>().enabled = true;
        }
    }

    private void SetText()
    {
        if (player.GetComponent<PlayerInventaire>().numPotion > 0)
        {
            slotPotion.GetComponent<Image>().enabled = true;
        }
        slotPotion.transform.GetChild(1).GetComponent<Text>().text = (player.GetComponent<PlayerInventaire>().numPotion + "");

        Bombe.transform.GetChild(0).GetComponent<Text>().text = (player.GetComponent<PlayerInventaire>().numBombe + "");
        arc.transform.GetChild(0).GetComponent<Text>().text = (player.GetComponent<PlayerInventaire>().numArrow + "");
    }

    private void SetKey()
    {
        if (player.GetComponent<PlayerInventaire>().gotKey == true)
        {
            Key.GetComponent<Image>().enabled = true;
        }

        else
        {
            Key.GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangementArme();
        SetText();
        SetKey();
    }
}
