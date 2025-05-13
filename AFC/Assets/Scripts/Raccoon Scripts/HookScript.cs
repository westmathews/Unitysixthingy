using UnityEngine;

public class HookScript : MonoBehaviour
{
    public GameObject shooter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0);
        if (other.gameObject.CompareTag("Ground"))
        {

        }
    }
}
