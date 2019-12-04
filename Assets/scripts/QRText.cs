using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using Paroxe.PdfRenderer.Internal.Viewer;
using Paroxe.PdfRenderer.WebGL;
public class QRText : MonoBehaviour
{
    // Start is called before the first frame update
    public Button QRBtn;
    
    public VScannerButton scanScript;

    // Update is called once per frame
    //tää vaan pistää skannatun tekstin/urlin testailua varten siihen isoon buttoniin ruudulla
    void Start() {
        scanScript = GameObject.Find("mainCanvas").GetComponentInChildren<VScannerButton>();
       
    }
   public void Update()
    {
        //QRBtn = GameObject.Find("QRTextBtn").GetComponent<Button>();
       
        
        string texti = scanScript.pubdata;
    
       
        // GameObject.Find("QRTextBtn").GetComponentInChildren<Text>().text = texti;

        
        //Debug.Log(texti);
        //scanScript.data;
        
    }
}
