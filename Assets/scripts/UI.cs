using System.Collections;
using System.Collections.Generic;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerInventaire>().isEquipArc)
        {
            //arc.GetComponent<Image>().SetActive = false;
        }
        slotPotion.transform.GetChild(1).GetComponent<Text>().text = (player.GetComponent<PlayerInventaire>().numPotion + "");

    }
}
