using UnityEngine;

public class Kappa : MonoBehaviour
{
    public Animator animator;
    public ennemi ennemi;
    private Vector3 movement;

    private void Start()
    {
        animator.SetBool("Carapace", true);
        ennemi = GetComponent<ennemi>();
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
    private void Update()
    {
        GetMovement();
        animationMovement();
        if (ennemi.gotShield == false)
        {
            animator.SetBool("Carapace", false);
        }
    }

}
