using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;


    private bool isFacingRight = true;


    private Animator animator;


    private Rigidbody rb;

    [HideInInspector]public Vector3 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        MovementInput();
    }

    void MovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0f, rb.velocity.y, horizontalInput * moveSpeed);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        // Update the rigidbody's velocity with the movement vector
        rb.velocity = movement;

        // Karakterin yuzunu cevir
        FlipCharacter(horizontalInput);
    }

    // Karakteri cevirme metodu
    void FlipCharacter(float horizontalInput)
    {
        // Karakterin yüzünü çevirme
        if ((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
