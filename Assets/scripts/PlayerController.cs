using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public bool useController;
    public Animator animator;
    public GameObject crossHair;
    public GameObject arrowPrefab;
    public float shootingRange;
    public float AimRange;
    public int moveSpeed;

    private Player player;
    private Vector3 movement;
    private Vector3 aim;
    private bool isAiming;
    private bool EndAiming;

    // inventaire
    private bool gotCrossbow;
    private bool gotKatana;
    public float numArrow;
    private float numPotion;

    private void Awake()
    {
        // initialise le player pour les inputs lier à rewired
        player = ReInput.players.GetPlayer(playerId);

        // cache et bloque le curseur de la souris en game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        ProcessInputs();
        Animation();
        Move();
        AimAndShoot();
    }

    
    private void Animation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        //topAnimator.SetFloat("Horizontal", movement.x);
        //topAnimator.SetFloat("Vertical", movement.y)
        //topAnimator.SetFloat("Magnitude", movement.magnitude);

        //topAnimator.SetFloat("AimHorizontal", movement.x);
        //topAnimator.SetFloat("AimVertical", movement.y)
        //topAnimator.SetFloat("Aim", isAiming);
        
    }
    

    private void Move()
    {
        // déplace le personnage
        transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
    }
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        // active me crossHair et le déplace en fonction de où l'on vise
        if ((aim.magnitude > 0.0f) & (isAiming == true))
        {
            crossHair.transform.localPosition = aim * AimRange;
            crossHair.SetActive(true);

            shootingDirection.Normalize();

            // créer un projectile et l'oriente dans le sens du tir
            if ((EndAiming) & (numArrow > 0))
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, shootingRange);
                Debug.Log("Vous avez tiré.");
            }
        }

        // désactive le crossHair quand il n'est pas utiliser
        else
        {
            crossHair.SetActive(false);
        }
    }
    private void ProcessInputs()
    {
        // déplacement manette
        if (useController)
        {
            movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            isAiming = player.GetButton("Aim");
            EndAiming = player.GetButtonUp("Fire");
        }

        // déplacement clavier & souris
        else
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement;
            if (aim.magnitude > AimRange)
            {
                aim.Normalize();
            }
            isAiming = Input.GetButton("Aim");
            EndAiming = Input.GetButtonUp("Fire1");
        }

        // normalise le déplacement sur la trajectoire en diagonal
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            Debug.Log("vous avez ramasser une flèche.");
            Destroy(collision.gameObject);
        }
    }
    */
}
