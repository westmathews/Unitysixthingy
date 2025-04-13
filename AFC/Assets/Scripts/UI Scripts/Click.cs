using UnityEngine;
using UnityEngine.UI;
public class Click : MonoBehaviour
{
   public GameObject canvas;
   public bool click = false;
   public Button self;
   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
        self.onClick.AddListener(clicked);
   }

   // Update is called once per frame
   void Update()
   {
        
   }
   void clicked()
   { 
       click = true;
   }
    
}
