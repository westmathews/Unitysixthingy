using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public float speed; // Movement speed
    public float gravity = -9.81f; // Gravity force
    public float jumpHeight; // Jump height
    public float ySpeed = 0f; // Vertical speed
    public bool isGrounded; // Check if the player is grounded
    public float jumpVelocity;
    public float sprintspd;
    public float wspeed;

    void Start()
    {
        
        jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            ySpeed = -1f;
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ySpeed = jumpVelocity; // Calculate jump speed
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintspd;
        }
        else
        {
            speed = wspeed;
        }

        // Apply gravity

       

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Move the player
        controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);

        
    }
    
}
