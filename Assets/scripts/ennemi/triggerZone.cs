using UnityEngine;

public class triggerZone : MonoBehaviour
{
    public GameObject ennemi;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ennemi.GetComponent<ennemi>().isFollowing = true;
        }
    }
}
