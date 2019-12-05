
using UnityEngine;
using UnityEngine.EventSystems;
using System.Timers;

public class buttonPressed : MonoBehaviour
{
    buttonControl bcont;
    bool pressed = false;
    public int pressCount = 0;


    void Start(){
           bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
    }    
    public void btnDown()
    {
        Debug.Log("painettu");
        pressed = true;
      
    }

    public void btnReleased()
    {
        pressed = false;
    }
   
    
    void Update()
    {
        if (pressed){
            pressCount++;
            }
        if (pressCount ==5) {
       
            pressCount= 0;
        }

    }
}
