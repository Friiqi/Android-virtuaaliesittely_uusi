﻿using UnityEngine;
using System;
using System.Collections;

using Vuforia;
using UnityEngine.EventSystems;

using System.Threading;

using ZXing;


[AddComponentMenu("System/VScannerButton")]
public class VScannerButton : MonoBehaviour
{    
         private bool cameraInitialized = false;
         private bool isDecoding = false;
         public bool contScan = true, restartScan = false,contScan2 = true;
        
    private BarcodeReader barCodeReader;
     public string pubdata = "";
     
    EventSystem cur_Event;
    buttonControl bcont; 
    private float secondsBetweenSpawns=1;
    void Start()
    
    {      
 
        cur_Event = EventSystem.current;
        bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
        
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
        StartCoroutine(ScanEveryXSecond());
        
    }   
    void Update() {
     if (restartScan){
         restartScan = false;
         StartCoroutine(InitializeCamera());
        StartCoroutine(ScanEveryXSecond());
     }
     
    }
 private IEnumerator ScanEveryXSecond(){
     while (bcont.bcontContScan){
        yield return new WaitForSeconds(secondsBetweenSpawns);
        Scan();   
     }
 }

private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(.75f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));
        
         //Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        
         
        cameraInitialized = true;
    }
  
    public void deSelectBtn(){
      cur_Event.SetSelectedGameObject(null);
    }
    
    public void DecodeQr(object state){
        isDecoding = true;
        var cameraFeed =CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.GRAYSCALE);
        var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
        if (data != null)
            {
            // QRCode detected.
           
            bcont.qrRecognized = true;
          
                isDecoding = false;
                bcont.inputUrlString = data.ToString();
                
                bcont.x = "pdfvid";
                data = null;
            }
        else
            {
                isDecoding = false;
                Debug.Log("No QR code detected !");
            }
}
    public void Scan()
    {

        if (cameraInitialized &&!isDecoding)
        {
            
            try
            {
                
                var cameraFeed = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.GRAYSCALE);
                if (cameraFeed == null)
                {
                    Debug.Log("camerafeed null");
                    return;
                }
             //kutsutaan DecodeQR-funktiota uuteen threadiin
                ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeQr), cameraFeed);
       
            }
                
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.Log(e);
                
            }
            
        }
        
    }
    }    
