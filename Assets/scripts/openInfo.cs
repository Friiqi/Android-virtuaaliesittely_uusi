using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInfo : MonoBehaviour
{
    buttonControl bcont;
    VScannerButton vScannerButton;
    private int counter=0;
    
    void Start() {
         bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
         vScannerButton = GameObject.Find("mainCanvas").GetComponent<VScannerButton>();
       
       
        
    }
    public void openInfoPanel() {
        if (counter == 0){
        bcont.infopanel.gameObject.SetActive(true);
        
        counter++;
        }
        else if (counter == 1){
             bcont.infopanel.gameObject.SetActive(false);
             bcont.bcontContScan = true;
             vScannerButton.restartScan = true;
             
        counter = 0;
        }
       bcont.infoBtn.gameObject.SetActive(true);
  
    }
  
}
