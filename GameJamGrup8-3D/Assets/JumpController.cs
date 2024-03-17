using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header("Jump Variables")]//Ziplama ozellikleri
    public float jumpForceMin = 1f;
    public float jumpForceMax = 5f;
    [SerializeField] float maxJumpTime = 0.1f;

    [HideInInspector] public bool isGrounded;
    private bool isJumping;
    private float jumpTime;
    private bool hasJumped; 
    private int doubleJumpCount;

    private BoxCollider boxCollider;

    private float rbVelocity;
    private bool canJump = true;

    [Header("Double Jump Variables")]// �ift ziplama ozellikleri
    [SerializeField] int maxDoubleJumps = 1;
    [SerializeField] float doubleJumpForce = 5f;

    // Animator bileseni
    private Animator animator;

    [Header("LayerMask")]//LayerMask Bileseni
    [SerializeField] LayerMask groundlayerMask;
    [SerializeField] LayerMask enemyLayer;

    private Rigidbody rb;

    // Baslangic metodu - Oyun basladiginda bir kere �alisir
    void Start()
    {
        animator = GetComponent<Animator>(); //Animator Caching
        boxCollider = GetComponent<BoxCollider>(); //CapsuleCollider Caching
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckJumpInput();
        CheckGrounded();

    }
    void CheckGrounded()
    {
        // Karakterin yerde olup olmad���n� kontrol et
        RaycastHit raycastHit;
        bool isHit = Physics.Raycast(boxCollider.bounds.center, Vector2.down, out raycastHit, boxCollider.bounds.extents.y + 0.2f, groundlayerMask);

        Color rayColor;

        if (isHit)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + 0.2f), rayColor);
        isGrounded = isHit;

        // Karakter yerdeyse, z�plama durumunu s�f�rla
        if (isGrounded)
        {
            hasJumped = false;
            doubleJumpCount = 0;
        }
    }


    void CheckJumpInput()
    {
        // "Jump" tu�una bas�ld���nda
        if (Input.GetButtonDown("Jump") && canJump)
        {
            // E�er yerdeyse
            if (isGrounded)
            {
                // Z�plama ba�lat
                isJumping = true;
                jumpTime = 0f;
                rb.velocity = new Vector2(rb.velocity.x, jumpForceMin);
                animator.SetBool("isJumping", true);
            }
            // Yerde de�ilse ve �ift z�plama kullan�labilirse
            else if (!hasJumped && doubleJumpCount < maxDoubleJumps)
            {
                // �ift z�plama ba�lat
                hasJumped = true;
                doubleJumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                animator.SetBool("isJumping", true);
            }
        }

        // "Jump" tu�una bas�l� tutuldu�u s�rece ve z�plama zaman s�n�r�na ula��lmam��sa
        if (Input.GetButton("Jump") && isJumping && jumpTime < maxJumpTime && canJump)
        {
            jumpTime += Time.deltaTime;
            float jumpForce = Mathf.Lerp(jumpForceMin, jumpForceMax, jumpTime / maxJumpTime); // Lineer Interpolasyon
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            if (isGrounded)
            {
                animator.SetBool("isJumping", false);
            }
        }

        // "Jump" tu�u b�rak�ld���nda
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTime = 0f;
            animator.SetBool("isJumping", false);
        }
    }
}