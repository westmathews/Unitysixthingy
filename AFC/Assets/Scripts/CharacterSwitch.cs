using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public GameObject self;
    public GameObject Other;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Other.SetActive(true);
            self.SetActive(false);
           
        }
    }
}
