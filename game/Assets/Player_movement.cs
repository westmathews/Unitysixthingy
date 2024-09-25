using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public float speed = 12f; // Movement speed
    public float gravity = -9.81f; // Gravity force
    public float jumpHeight = 3f; // Jump height

    private float ySpeed = 0f; // Vertical speed
    public bool isGrounded; // Check if the player is grounded
    private float jumpVelocity;

    void Start()
    {
        jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        // Reset vertical speed if grounded
        //if (isGrounded && ySpeed < 0)
        //{
        //ySpeed = 0f;
        //}

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ySpeed = jumpVelocity; // Calculate jump speed
        }

        // Apply gravity
        ySpeed += gravity * Time.deltaTime;

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the player
        controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);

        
    }
    
}
