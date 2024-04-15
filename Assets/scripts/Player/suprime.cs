using UnityEngine;

public class PlayerIneventaire : MonoBehaviour
{
    // inventaire
    private bool gotArc;
    public bool gotKatana;
    public bool gotKey;

    private bool isChanging;
    public bool isEquipKatana;
    public bool isEquipArc;

    public float numArrow;
    public int numBombe;


    void Start()
    {
        gotArc = false;
        gotKatana = false;
        isEquipArc = false;
        isEquipKatana = false;
        numArrow = 0;
        numBombe = 0;
    }


    void Update()
    {

    }
}
