using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARon_deactivate_pdf : MonoBehaviour
{
    // Start is called before the first frame update
    buttonControl bcont;
   void Start(){
     bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
     
   }

   public void ARcamOn() {
       //bcont.ARcameraonoff(true);
        bcont.pdfrend.SetActive(false);
        bcont.x = "default";
   }
      public void ARcamOff() {
       //bcont.ARcameraonoff(false);
        bcont.pdfrend.SetActive(false);
        bcont.x = "default";
   }
}

