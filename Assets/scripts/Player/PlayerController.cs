using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    #region inputs
    public bool useController;
    #endregion
    #region animation
    public Animator animator;
    #endregion
    #region GameObject
    public GameObject crossHair;
    public GameObject arrowPrefab;
    public GameObject bombePrefab;
    #endregion    
    #region rewired
    private Player player;
    public int playerId = 0;
    #endregion
    #region deplacement
    private Vector3 movement;
    private Vector3 aim;

    public float shootingRange;
    public float AimRange;
    public int moveSpeed;
    #endregion
    #region Inventaire
    PlayerInventaire Inventaire;
    public bool isChangingUp;
    public bool isChangingDown;
    #endregion
    #region attaque
    private bool isAimingArc;
    private bool EndAimingArc;

    private bool isAimingBombe;
    private bool EndAimingBombe;

    private bool isAttacking;
    private bool isParing;
    #endregion

    private void Awake()
    {
        Inventaire = GetComponent<PlayerInventaire>();
        // initialise le player pour les inputs lier � rewired
        player = ReInput.players.GetPlayer(playerId);

        // cache et bloque le curseur de la souris en game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        ProcessInputs();
        gameObject.GetComponent<PlayerInventaire>().ChangementArme();
        Animation();
        Move();
        AimAndShoot();
        ContreAndAttack();
    }

    private void Animation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (Inventaire.isEquipArc == true)
        {
            animator.SetBool("EquipArc", true);
            animator.SetBool("EquipKatana", false);
        }

        if (Inventaire.isEquipKatana == true)
        {
            animator.SetBool("EquipKatana", true);
            animator.SetBool("EquipArc", false);
            if (isAttacking == true)
            {
                animator.SetBool("isAttacking", true);
                if (animator.GetBool("ATK1") == true)
                {
                    animator.SetBool("ATK1", false);
                }
            }
        }

    }


    private void Move()
    {
        // normalise le d�placement sur la trajectoire en diagonal
        if (movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        // d�place le personnage
        transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
    }

    private void ContreAndAttack()
    {
        if (isParing == true)
        {
            Debug.Log("Tu es en train de parer.");
        }

        else if (isAttacking == true)
        {
            Debug.Log("tu as attaquer avec ton Katana.");
        }
    }
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);

        // active le crossHair et le d�place en fonction de o� l'on vise et si l'on est en train de viser.
        if ((aim.magnitude > 0.0f) && ((isAimingArc == true) || (isAimingBombe == true)))
        {
            crossHair.transform.localPosition = aim * AimRange;
            crossHair.SetActive(true);

            shootingDirection.Normalize();


            // cr�er une fl�che et l'oriente dans le sens du tir
            if ((EndAimingArc == true) && (Inventaire.numArrow > 0) && (isAimingArc == true))
            {
                gameObject.GetComponent<PlayerInventaire>().numArrow -= 1;
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * 3.0f;
                arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
                Destroy(arrow, shootingRange);
                Debug.Log("Vous avez tir�.");
            }

            if ((EndAimingBombe == true) && (isAimingBombe = true) && (Inventaire.numBombe > 0))
            {
                gameObject.GetComponent<PlayerInventaire>().numBombe -= 1;
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

            isChangingUp = player.GetButtonDown("ChangementArmeUp");
            isChangingDown = player.GetButtonDown("ChangementArmeDown");
            gameObject.GetComponent<PlayerHealth>().isHealing = player.GetButtonDown("Heal");

            if (Inventaire.isEquipArc == true)
            {
                isAimingBombe = false;
                isAimingArc = (aim != new Vector3(0.0f, 0.0f, 0.0f));
                EndAimingArc = player.GetButtonDown("Attaque");
            }

            else if (Inventaire.isEquipKatana == true)
            {
                isAimingArc = false;
                isAimingBombe = false;
                isAttacking = player.GetButtonDown("Attaque");
            }

            else if (Inventaire.isEquipBombe == true)
            {
                isAimingArc = false;
                isAimingBombe = (aim != new Vector3(0.0f, 0.0f, 0.0f));
                EndAimingBombe = player.GetButtonDown("Attaque");
            }
        }

        // d�placement clavier & souris
        else
        {
            // vecteur de d�placement
            movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

            // curser de vis�
            Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
            aim += mouseMovement;
            if (aim.magnitude > AimRange)
            {
                aim.Normalize();
            }


            isChangingUp = Input.GetKeyDown(KeyCode.E);
            isChangingDown = Input.GetKeyDown(KeyCode.A);
            gameObject.GetComponent<PlayerHealth>().isHealing = Input.GetKeyDown(KeyCode.X);


            if (Inventaire.isEquipArc == true)
            {
                isAimingArc = Input.GetMouseButton(1);
                EndAimingArc = Input.GetMouseButtonDown(0);
            }

            if (Inventaire.isEquipKatana == true)
            {
                isAttacking = Input.GetMouseButton(1);
            }

            if (Inventaire.isEquipBombe == true)
            {
                isAimingBombe = Input.GetMouseButton(1);
                EndAimingBombe = Input.GetMouseButtonDown(0);
            }



        }
    }    
}
