using UnityEngine;

public class PewPew : MonoBehaviour
{
    public Camera playerCamera; // Assign your camera in the inspector
    public float shootingRange = 100f; // How far the ray can go
    public LayerMask targetLayer; // Specify which layer to detect hits

    public Vector3 target;
    void Update()
    {


        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);
        if (Input.GetMouseButtonDown(0)) // Default is left mouse button
        {
            This(transform.position, new Vector3(1, -1, 0));
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Shoot from the center of the screen
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootingRange, targetLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Add your hit logic here (e.g., damage, effects)
        }
    }

    void This(Vector3 playerPos, Vector3 offset)
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, shootingRange))
        {
            target = hit.transform.position;

            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                // Logic for hitting a player
                Debug.Log("Hit a player!");
                // You can add additional actions here, like applying damage or triggering an effect
            }
        }
        //Ray raytwo = new Vector3(target)(new Vector3(offset));
    }

}