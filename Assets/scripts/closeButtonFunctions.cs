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
     bcont.pdfrend.SetActive(false);
     setXtoDefault();
 }

public void closeInfoPanel(){
    bcont.infopanel.gameObject.SetActive(false);
    //pidetään huoli ettei infoikkunan sulkeminen eri tilanteissa piilota vääriä nappeja jne
    if (bcont.pdfrend.activeInHierarchy){
        bcont.x ="pdfOpen";
    }
    else if (bcont.videoPlayerIsOpen) {
        bcont.x ="videoPlayerOpen";
    }
    else {
        setXtoDefault();
    }
    }
 
}
