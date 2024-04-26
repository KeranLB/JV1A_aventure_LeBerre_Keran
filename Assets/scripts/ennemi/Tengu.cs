using UnityEngine;

public class Tengu : MonoBehaviour
{
    public Animator animator;
    public ennemi ennemi;
    public ATK atk;
    public GameObject TrigerAtk;
    public GameObject fantom;
    public GameObject Menus;
    private Vector3 movement;

    private void Start()
    {
        ennemi = gameObject.GetComponent<ennemi>();
        atk = TrigerAtk.GetComponent<ATK>();
    }
    private void animationMovement()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    void GetMovement()
    {
        movement = new Vector3(ennemi.direction.x, ennemi.direction.y, 0.0f);
    }

    void ATK()
    {
        if (atk.isAttacking == true)
        {
            animator.Play("cri");
            if (atk.cpt == Time.frameCount)
            {
                GameObject fantomTengu = Instantiate(fantom, transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                atk.isAttacking = false;
            }
        }
    }

    private void Update()
    {
        GetMovement();
        animationMovement();
        ATK();
        death();
    }

    private void death()
    {
        if (ennemi.currentHealth < 1)
        {
            Menus.SetActive(true);
        }
    }
}
