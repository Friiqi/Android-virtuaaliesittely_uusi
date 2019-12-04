﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;

using System.Threading;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

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
    //piilotetaan pdf&video napit alussa
    {      
       
       
        cur_Event = EventSystem.current;
        bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
        
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
        StartCoroutine(ScanEveryXSecond());
        //InvokeRepeating("Scan", 0, 1.0f);
    }   
    void Update() {
        /*
       if (bcont.bcontContScan){
           Debug.Log("scannerbuttonin sisällä contscan false");
       }
       */
        if (restartScan){
        StartCoroutine(InitializeCamera());
        StartCoroutine(ScanEveryXSecond());
        restartScan = false;
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
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));
        
         //Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        
         //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
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
            bcont.x = "pdfvid";
                isDecoding = false;
                bcont.inputUrlString = data.ToString();
                Debug.Log("data: " + data);
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
    
    //tää tekee buttonControl-scriptissä pdf ja vid napit näkyviksi. se pitäisi olla tuolla try-catch lohkon sisällä sitten kun testailuvaihe ohi, nyt tässä kohtaa että voi testata ilman QR koodin tunnistustapahtumaa.
    //bcont.x = "pdfvid";
    
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
              // var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeQr), cameraFeed);
                
               
            /*
                
                if (data != null)
                {
                pubdata = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8).ToString();
                bcont.inputUrlString = pubdata;
                    // QRCode detected.
                   bcont.x = "pdfvid";
                    //readQr.Select();
                    
                   
                    
                }
                else
                {
                    //Invoke("deSelectBtn", 1.0f);
                }
                */
            }
                
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.Log(e);
                
            }
            
        }
        
    }
    }    
