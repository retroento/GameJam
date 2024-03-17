using UnityEngine;

public class TwoDimensionalMovement : MonoBehaviour
{
    public float speed = 5f; // Karakterin yatay hareket hýzý
    public float jumpForce = 10f; // Zýplama kuvveti
    public Transform groundCheck; // Yerde olup olmadýðýmýzý kontrol etmek için bir nokta
    public LayerMask groundLayer; // Yer katmaný

    public bool isGrounded = false; // Yerde olup olmadýðýmýzý tutar
    private Rigidbody rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator> ();
    }

    void Update()
    {
        // Yerde olup olmadýðýmýzý kontrol et
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.4f, groundLayer);

        // Sað ve sol giriþleri al
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Hareket vektörünü oluþtur
        Vector3 movement = new Vector3(0f, 0f, moveHorizontal).normalized;

        // Karakteri yatay eksende hareket ettir
        rb.velocity = new Vector3(0f, rb.velocity.y, movement.z * speed);

        // Zýplama kontrolü
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Yerdeyken ve space tuþuna basýldýðýnda zýplama
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
