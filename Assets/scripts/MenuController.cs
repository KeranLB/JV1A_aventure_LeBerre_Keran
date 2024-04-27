using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject player;
    private PlayerController control;
    private PlayerHealth health;
    public GameObject Menu;
    public GameObject GameOver;
    public GameObject Win;
    public GameObject tengu;

    private void Start()
    {
        control = player.GetComponent<PlayerController>();
        health = player.GetComponent<PlayerHealth>();
        Menu.SetActive(false);
        GameOver.SetActive(false);
        Win.SetActive(false);
    }

    private void Update()
    {
        if (control.Pause == true)
        {
            Menu.SetActive(true);
        }

        else if (health.dead == true)
        {
            GameOver.SetActive(true);
        }
        else if (tengu.GetComponent<Tengu>().dead == true)
        {
            Win.SetActive(true);
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

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
