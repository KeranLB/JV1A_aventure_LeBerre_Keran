using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject mainCamera;


    private void Awake()
    {
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }    
    }
}
