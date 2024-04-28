using UnityEngine;

public class SpawnTengu : MonoBehaviour
{
    public string name;
    private void Start()
    {
        GameObject.FindGameObjectWithTag(name).transform.position = transform.position;
    }
}
