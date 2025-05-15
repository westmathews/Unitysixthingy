using UnityEngine;
using Mirror;

public class Player_Movement : NetworkBehaviour
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
    public bool darted = false;
    public float dartTimer = 0;
    public bool hooked = false;
    public float hooktime = 0;
    void Start()
    {        
        //jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    [Server]
    public void grenadehityou(GameObject other, NetworkConnection connection)
    {
        Debug.Log("grenade knockback");
        hitdirection = ((transform.position - other.transform.position).normalized);
        if (hitdirection.y < .5f)
        {
            hitdirection = (hitdirection + new Vector3(0, 1.2f, 0)) * 2;
        }
        else
        {
            hitdirection = hitdirection * 3;
        }
        Debug.Log("igodisway:"+hitdirection);
        actuallymovingyou(connectionToClient, other);
    }
    [TargetRpc]
    void actuallymovingyou(NetworkConnection networkConnectionToClient, GameObject other)
    {
        Debug.Log("grenade knockback");
        hitdirection = ((transform.position - other.transform.position).normalized);
        if (hitdirection.y < .5f)
        {
            hitdirection = (hitdirection + new Vector3(0, 1.2f, 0)) * 2;
        }
        else
        {
            hitdirection = hitdirection * 3;
        }
        Debug.Log("igodisway:" + hitdirection);
    }
    [TargetRpc]
    public void HookMove(NetworkConnection networkconnectiontoclient, Vector3 HookVelocity)
    {
        hooked = true;
        hitdirection = HookVelocity/12 + new Vector3(0,2,0);
        Debug.Log("Triggeres hookmove. Velocity is: " + HookVelocity);
    }
    void Update()
    {

        if (isLocalPlayer)
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
            if (darted)
            {
                speed = speed * .66f;

                dartTimer = -Time.deltaTime;
            }

            //if (dartTimer <= 0)
            //{
              //  darted = false;
            //}

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
            if (moveX != 0 && moveZ != 0)
            {
                controller.Move(move * (speed * .50f) * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
            }

            if (gliding)
            {
                hitdirection = new Vector3(0, 0, 0);
            }
            if (grenadehit)
            {

                move = transform.right * moveX + transform.forward * moveZ;
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
            if (hooked)
            {
                if (hooktime>.1 && isGrounded)
                {
                    hooked = false;
                    hooktime = 0;
                }
                if (hooktime > 2)
                {
                    hooked = false;
                    hooktime = 0;
                }
                move = transform.right * moveX + transform.forward * moveZ;
                
                controller.Move(hitdirection * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
                hooktime += Time.deltaTime;

            }
            controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);


            
    }
       }   
}
