using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of rotation

    void Update()
    {
        // Rotate the skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}