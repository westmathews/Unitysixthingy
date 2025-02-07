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
    public float moveX;
    public float moveZ;
    public bool gliding = false;
    public bool grenadehit = false;
    Vector3 move;
    Vector3 hitdirection;
    public float grenknockback;
    void Start()
    {        
        //jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    void OnTriggerEnter(Collider other)
    {
        hitdirection = ((transform.position - other.transform.position).normalized);
        if (hitdirection.y < .5f)
        {
            hitdirection = (hitdirection + new Vector3(0, 1, 0)) * 2;
        }
        else
        {
            hitdirection = hitdirection * 2;
        }
        Debug.Log("igodisway:"+hitdirection);
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
        if (gliding)
        {
            moveX = 0;
            moveZ = 1;    
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }
        // Create movement vector
        move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
        if (gliding)
        {
            hitdirection = new Vector3(0, 0, 0);
        }
        if (grenadehit)
        {

            move = transform.right * moveX + transform.forward * moveZ;
            if (gliding)
            {

            }
            if (isGrounded && grenknockback > .1f)
            {
                grenadehit = false;
                grenknockback = 0;
            }
            if (grenknockback > 3)
            {
                grenadehit = false;
                grenknockback = 0;
            }
            controller.Move(hitdirection * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
            grenknockback += Time.deltaTime;
        }
        controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);


    }
    
}
