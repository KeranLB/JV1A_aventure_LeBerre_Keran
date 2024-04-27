using UnityEngine;

public class ATK : MonoBehaviour
{
    public bool isAttacking;
    public int cpt;

    private void Start()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
            cpt = Time.frameCount;
        }
    }
}
