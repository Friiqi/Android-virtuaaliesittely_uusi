using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInfo : MonoBehaviour
{
    buttonControl bcont;
    
    
    void Start() {
         bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
       
        
    }
    public void openInfoPanel() {
        bcont.infopanel.gameObject.SetActive(true);
    
      

    }
  
}
