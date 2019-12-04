using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class openPDF : MonoBehaviour
{
    
    buttonControl bcont;
    
    
    void Start() {
         bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
       
        
    }
    public void openPdfRenderer() {
        bcont.pdfChosen = true;
    
        bcont.x = "pdfopen";

    }
  

}

