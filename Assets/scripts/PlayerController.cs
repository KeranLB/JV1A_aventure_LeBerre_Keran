using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    //public Animator animator;
    public GameObject crossHair;

    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    void Update()
    {
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y)
        //animator.SetFloat("Magnitude", movement.magnitude);

        Vector3 movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;

        if (player.GetButton("Fire"))
        {
            Debug.Log("FIRE !");
        }

        MoveCrossHair();
    }

    private void MoveCrossHair()
    {
        Vector3 aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);

        if (aim.magnitude > 0.0f)
        {
            aim.Normalize();
            aim *= 0.4f;
            crossHair.transform.localPosition = aim;
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
}
