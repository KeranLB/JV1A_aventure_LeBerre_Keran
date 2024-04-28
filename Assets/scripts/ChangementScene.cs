using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public string Target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (collision.GetComponent<PlayerInventaire>().gotKey == true))
        {
            SceneManager.LoadScene(Target);
            collision.GetComponent<PlayerInventaire>().gotKey = false;
        }
    }
}
