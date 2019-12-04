using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class closeButtonFunctions : MonoBehaviour
{
    buttonControl bcont; 
    VScannerButton vScannerButton;
  
    void Start(){
         bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
        vScannerButton = GameObject.Find("mainCanvas").GetComponent<VScannerButton>();
    }
 public void setXtoDefault(){
     bcont.x = "default";
      bcont.pdfChosen = false;
       bcont.videoChosen = false;
 }
  public void PDFoff(){
     // vScannerButton.restartScan = true;
     bcont.pdfrend.SetActive(false);
 
    setXtoDefault();
 }


 
}
