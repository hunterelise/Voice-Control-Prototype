using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSetX;
    public float playerSetY;
    public float playerSetZ;

    public float jumpForce = 7f;
    private bool isGrounded = true;

    private Rigidbody2D rb;

    // Runs once at the start, sets spawn and gets rigidbody
    void Start()
    {
        transform.position = new Vector3(playerSetX, playerSetY, 0f);
        rb = GetComponent<Rigidbody2D>();
    }

    // Checks each frame for jump input
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            Jump();
        }
    }

    // Makes the player jump
    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // Detects when player touches the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
