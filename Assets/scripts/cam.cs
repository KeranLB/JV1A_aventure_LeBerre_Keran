using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField]
    public Transform Subject;

    Vector2 startPosition;

    float startZ;


    Vector2 travel => (Vector2)Subject.transform.position - startPosition;

    void Start()
    {
        startPosition = transform.position;
        startZ = transform.localPosition.z;

    }

    void Update()
    {
        Vector2 newPos = startPosition + travel;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tilemap"))
        {
            Debug.Log("votre caméra est sortie de la zone de jeu.");
        }
    }
}