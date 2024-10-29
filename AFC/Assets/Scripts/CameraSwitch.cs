using UnityEngine;

public class CameraSwitchWithRotation : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 firstPersonOffset = new Vector3(0, 1.8f, 0); // First-person view position (near player's head)
    public Vector3 thirdPersonOffset = new Vector3(0, 2, -5); // Third-person view offset (behind the player)
    public float followSpeed = 5f; // Speed of camera following the player
    public float smoothTransitionSpeed = 5f; // Speed of smooth camera transition
    public float mouseSensitivity = 100f; // Sensitivity for mouse rotation
    public float minVerticalAngle = -30f; // Minimum vertical camera angle (looking down)
    public float maxVerticalAngle = 60f; // Maximum vertical camera angle (looking up)

    private Vector3 currentOffset; // Current camera offset
    public bool isThirdPerson = false; // Check if in third-person view
    private float pitch = 0f; // Camera pitch (vertical rotation)
    private float yaw = 0f; // Camera yaw (horizontal rotation)
    private bool isSwitchingToThirdPerson = false; // Check if we're transitioning to third-person

    void Start()
    {
        // Start with first-person view
        currentOffset = firstPersonOffset;
        transform.position = player.position + player.TransformDirection(currentOffset);
        transform.rotation = player.rotation;

        // Initialize yaw to match the player's current rotation
        yaw = player.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Check if we're switching to third-person view
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Check if we're switching for the first time (to sync camera yaw with player rotation)
            if (!isThirdPerson)
            {
                isThirdPerson = true;
                isSwitchingToThirdPerson = true;
            }

            if (isSwitchingToThirdPerson)
            {
                // Set initial yaw to match the player's current rotation
                yaw = player.eulerAngles.y;
                isSwitchingToThirdPerson = false;
            }

            // Handle mouse rotation in third-person
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Update yaw (horizontal rotation) and pitch (vertical rotation) based on mouse input
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle); // Clamp vertical rotation

            // Calculate new rotation around the player
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 rotatedOffset = rotation * thirdPersonOffset;

            // Move and rotate camera smoothly around the player
            transform.position = Vector3.Lerp(transform.position, player.position + rotatedOffset, followSpeed * Time.deltaTime);
            transform.LookAt(player.position + Vector3.up * 1.8f); // Keep the camera looking at the player
        }
        else
        {
            // Transition back to first-person view
            isThirdPerson = false;
            currentOffset = Vector3.Lerp(currentOffset, firstPersonOffset, smoothTransitionSpeed * Time.deltaTime);

            // Smoothly move camera to the player's head and match player rotation
            Vector3 targetPosition = player.position + player.TransformDirection(currentOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, 0);
        }
    }
}
