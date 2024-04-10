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

    // UI 
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    // inventaire
    private bool gotArc;
    public bool gotKatana;
    private bool isEquipKatana;
    private bool isEquipArc;
    public bool gotKey;
    public float numArrow;
    public int numBombe;
    public int numPotion;


    private void Awake()
    {
        // initialise le player pour les inputs lier � rewired
        player = ReInput.players.GetPlayer(playerId);

        // cache et bloque le curseur de la souris en game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        gotArc = false;
        gotKatana = false;
        isEquipArc = false;
        isEquipKatana = false;
        numArrow = 0;
        numBombe = 0;
        numPotion = 0;
        currentHealth = maxHealth;
        healthBar.SetMAxHealth(maxHealth);

    }


    void Update()
    {
        ProcessInputs();
        Animation();
        Move();
        AimAndShoot();
        ContreAndAttack();
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    private void Animation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        

    }
    

    private void Move()
    {
        // d�place le personnage
        transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
    }

    private void ContreAndAttack()
    {
        if ((gotKatana == true) && (isAttacking == true))
        {
            //animation attaque
        }
    }
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        // active le crossHair et le d�place en fonction de o� l'on vise et si l'on est en train de viser.
        if ((aim.magnitude > 0.0f) && ((isAimingArc == true) || (isAimingBombe == true)) )
        {
            crossHair.transform.localPosition = aim * AimRange;
            crossHair.SetActive(true);

            shootingDirection.Normalize();


            // cr�er une fl�che et l'oriente dans le sens du tir
            if ((EndAimingArc == true) && (numArrow > 0) && (isAimingArc == true))
            {
                numArrow -= 1;
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, shootingRange);
                Debug.Log("Vous avez tir�.");
            }

            if ((EndAimingBombe == true) && (isAimingBombe = true) && (numBombe > 0))
            {
                numBombe -= 1;
                GameObject bombe = Instantiate(bombePrefab, transform.position, Quaternion.identity);
                bombe.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                bombe.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(bombe, shootingRange);
                Debug.Log("Vous avez lancer une bombe.");
            }
        }

        // d�sactive le crossHair quand il n'est pas utiliser
        else
        {
            crossHair.SetActive(false);
        }
    }
    private void ProcessInputs()
    {
        // d�placement manette
        if (useController)
        {
            movement = new Vector3(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"), 0.0f);
            aim = new Vector3(player.GetAxis("AimHorizontal"), player.GetAxis("AimVertical"), 0.0f);
            aim.Normalize();
            
            if (isEquipArc == true)
            {
                isAimingArc = player.GetButton("AimArc");
                EndAimingArc = player.GetButtonDown("Fire");
            }

            if (isAimingArc == false)
            {
                isAimingBombe = player.GetButton("AimBombe");
                EndAimingBombe = player.GetButtonDown("LancerBombe");
            }

            
            isAttacking = player.GetButtonDown("Attaque");
        
            // �quipe le katana
            if ( (player.GetButton("EquipKatana")) && (gotKatana == true) )
            {
                isEquipKatana = true;
                isEquipArc = false;
                animator.SetBool("EquipKatana", true);
                animator.SetBool("EquipArc", false);
            }

            // �quipe l'arc
            if ((player.GetButton("EquipArc")) && (gotArc == true))
            {
                isEquipArc = true;
                isEquipKatana = false;
                animator.SetBool("EquipArc", true);
                animator.SetBool("EquipKatana", false);
            }
        }
        
        // d�placement clavier & souris
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
        
        // normalise le d�placement sur la trajectoire en diagonal
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision avec une fleche collectible
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("vous avez ramasser une fl�che.");
            Destroy(collision.gameObject);
            numArrow += 1;
        }

        // collision avec une bombe collectible
        if (collision.CompareTag("Bombe"))
        {
            Debug.Log("vous avez ramasser une bombe.");
            Destroy(collision.gameObject);
            numBombe = numBombe + 1;
        }

        // collision avec une potion collectible
        if (collision.CompareTag("Potion"))
        {
            Debug.Log("vous avez ramasser une potion de soin.");
            Destroy(collision.gameObject);
            numPotion += 1;
        }

        // collision avec l'arc collectible
        if (collision.CompareTag("Arc"))
        {
            Debug.Log("vous avez ramasser un Arc.");
            Destroy(collision.gameObject);
            gotArc = true;
        }

        // collision avec le katana collectible
        if (collision.CompareTag("Katana"))
        {
            Debug.Log("vous avez ramasser un Katana.");
            Destroy(collision.gameObject);
            gotKatana = true;
        }

        // collision avec cl� collectible
        if (collision.CompareTag("key"))
        {
            Debug.Log("vous avez ramasser une cl�.");
            Destroy(collision.gameObject);
            gotKey = true;
        }
        if (collision.CompareTag("ennemi"))
            {
                TakeDamage(25);
                Destroy(collision.gameObject);
            }
        
    }
    
}
