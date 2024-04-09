using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int playerId = 0;
    public bool useController;
    public Animator animator;
    public Animator animatorArc;
    public Animator animatorKatna;
    public GameObject crossHair;
    public GameObject arrowPrefab;
    public GameObject bombePrefab;
    public float shootingRange;
    public float AimRange;
    public int moveSpeed;

    private Player player;
    private Vector3 movement;
    private Vector3 aim;
    private bool isAimingArc;
    private bool EndAimingArc;
    private bool isAimingBombe;
    private bool EndAimingBombe;
    private bool isAttacking;

    // inventaire
    private bool gotArc;
    public bool gotKatana;
    public bool gotKey;
    public float numArrow;
    public int numBombe;
    public int numPotion;


    private void Awake()
    {
        // initialise le player pour les inputs lier à rewired
        player = ReInput.players.GetPlayer(playerId);

        // cache et bloque le curseur de la souris en game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        gotArc = false;
        gotKatana = false;
        numArrow = 0;
        numBombe = 0;
        numPotion = 0;
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
        
        if ((gotKatana == true) && (isAttacking == true))
        {
            //animation attaque
        }
    }
    

    private void Move()
    {
        // déplace le personnage
        transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
    }
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        // active me crossHair et le déplace en fonction de où l'on vise et si l'on est en train de viser.
        if ((aim.magnitude > 0.0f) && ((isAimingArc == true) || (isAimingBombe == true)) )
        {
            crossHair.transform.localPosition = aim * AimRange;
            crossHair.SetActive(true);

            shootingDirection.Normalize();


            // créer une flèche et l'oriente dans le sens du tir
            if ((EndAimingArc) & (numArrow > 0))
            {
                numArrow -= 1;
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, shootingRange);
                Debug.Log("Vous avez tiré.");
            }

            if ((EndAimingBombe == true) & (numBombe > 0))
            {
                numBombe -= 1;
                GameObject bombe = Instantiate(bombePrefab, transform.position, Quaternion.identity);
                bombe.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                bombe.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(bombe, shootingRange);
                Debug.Log("Vous avez lancer une bombe.");
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
            if (gotArc == true)
            {
                isAimingArc = player.GetButton("AimArc");
                EndAimingArc = player.GetButtonUp("Fire");
            }

            isAimingBombe = player.GetButton("AimBombe");
            EndAimingBombe = player.GetButtonUp("LancerBombe");
            isAttacking = player.GetButtonDown("Attaque");
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
            if (gotArc == true)
            {
                isAimingArc = Input.GetButton("AimArc");
                EndAimingArc = Input.GetButtonUp("Fire");
            }
            isAimingBombe = Input.GetButton("AimBombe");
            EndAimingBombe = Input.GetButtonUp("LancerBombe");
            isAttacking = Input.GetButtonDown("Attaque");
        }
        
        // normalise le déplacement sur la trajectoire en diagonal
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("vous avez ramasser une flèche.");
            Destroy(collision.gameObject);
            numArrow += 1;
        }

        if (collision.CompareTag("Bombe"))
        {
            Debug.Log("vous avez ramasser une bombe.");
            Destroy(collision.gameObject);
            numBombe = numBombe + 1;
        }

        if (collision.CompareTag("Potion"))
        {
            Debug.Log("vous avez ramasser une potion de soin.");
            Destroy(collision.gameObject);
            numPotion += 1;
        }

        if (collision.CompareTag("Arc"))
        {
            Debug.Log("vous avez ramasser un Arc.");
            Destroy(collision.gameObject);
            gotArc = true;
        }

        if (collision.CompareTag("Katana"))
        {
            Debug.Log("vous avez ramasser un Katana.");
            Destroy(collision.gameObject);
            gotKatana = true;
        }

        if (collision.CompareTag("key"))
        {
            Debug.Log("vous avez ramasser une clé.");
            Destroy(collision.gameObject);
            gotKey = true;
        }
    }
    
}
