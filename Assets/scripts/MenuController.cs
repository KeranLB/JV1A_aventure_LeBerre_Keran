using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    private PlayerController control;
    private PlayerHealth health;
    public GameObject Menu;
    public GameObject End;

    private void Start()
    {
        control = player.GetComponent<PlayerController>();
        health = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (control.Pause == true)
        {
            Menu.SetActive(true);
        }

        if (health.dead == true)
        {
            End.gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Zone 0");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
