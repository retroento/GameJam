using UnityEngine;

public class TwoDimensionalMovement : MonoBehaviour
{
    public float speed = 5f; // Karakterin yatay hareket h�z�
    public float jumpForce = 10f; // Z�plama kuvveti
    public Transform groundCheck; // Yerde olup olmad���m�z� kontrol etmek i�in bir nokta
    public LayerMask groundLayer; // Yer katman�

    public bool isGrounded = false; // Yerde olup olmad���m�z� tutar
    private Rigidbody rb;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator> ();
    }

    void Update()
    {
        // Yerde olup olmad���m�z� kontrol et
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.4f, groundLayer);

        // Sa� ve sol giri�leri al
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Hareket vekt�r�n� olu�tur
        Vector3 movement = new Vector3(0f, 0f, moveHorizontal).normalized;

        // Karakteri yatay eksende hareket ettir
        rb.velocity = new Vector3(0f, rb.velocity.y, movement.z * speed);

        // Z�plama kontrol�
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Yerdeyken ve space tu�una bas�ld���nda z�plama
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
