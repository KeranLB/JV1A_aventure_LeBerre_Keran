using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject body;

    private void Update()
    {
        transform.position = body.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            body.GetComponent<ennemi>().isFollowing = true;
        }
    }
}
