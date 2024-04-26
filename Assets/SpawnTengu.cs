using UnityEngine;

public class SpawnTengu : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Tengu").transform.position = transform.position;
    }
}
