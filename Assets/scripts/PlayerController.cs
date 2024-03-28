using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public bool useController;
    //public Animator animator;
    public GameObject crossHair;
    public GameObject arrowPrefab;
    public float shootingRange;
    public float AimRange;

    private Player player;
    private Vector3 movement;
    private Vector3 aim;
    private bool isAiming;
    private bool EndAiming;
    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        ProcessInputs();
        AimAndShoot();
        //Annimation()
        Move();
    }

    /*
    private void Annimation()
    {
        bottomAnimator.SetFloat("Horizontal", movement.x);
        bottomAnimator.SetFloat("Vertical", movement.y)
        bottomAnimator.SetFloat("Magnitude", movement.magnitude);

        topAnimator.SetFloat("Horizontal", movement.x);
        topAnimator.SetFloat("Vertical", movement.y)
        topAnimator.SetFloat("Magnitude", movement.magnitude);

        topAnimator.SetFloat("AimHorizontal", movement.x);
        topAnimator.SetFloat("AimVertical", movement.y)
        topAnimator.SetFloat("Aim", isAiming);
        
    }
    */

    private void Move()
    {
        transform.position = transform.position + movement * Time.deltaTime;
    }
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        if (aim.magnitude > 0.0f)
        {
            crossHair.transform.localPosition = aim * AimRange;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (EndAiming)
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, shootingRange);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
    private void ProcessInputs()
    {
        if (useController)
        {
            movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAiming = player.GetButton("Fire");
            EndAiming = player.GetButtonDown("Fire");
        }
        else
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement;
            if (aim.magnitude > AimRange)
            {
                aim.Normalize();
            }
            isAiming = Input.GetButton("Fire1");
            EndAiming = Input.GetButtonUp("Fire1");
        }

        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }
}
