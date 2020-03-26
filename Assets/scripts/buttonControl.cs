using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Paroxe.PdfRenderer;
using UnityEngine.Networking;
using System.IO;








public class buttonControl : MonoBehaviour
{
    
    //skripti olemassa että voi piilottaa/näyttää buttoneita ja muita, koska ilman viittauksia (gameobjektit sijoitetaan inspectorissa tähän scriptiin) niitä ei voinut enää .setactive(true) (saada näkyväksi) kun kerran piilotettu (.setactive(false))
    public Button pdf,vid,play,pause,stop, closePdf,menu,btnSizeUp,btnSizeDown,btnForceDownload,infoBtn,btnQrRecognized,btnImgRecognized,btnImgRecOff,menuBtn;
    public Image menuBgImg;
   
     
     public Slider track;
      public PDFViewer pdfViewer;
     public string x="",inputUrlString ="",formattedUrlForPDF="",formattedUrlForMP4="",formattedUrl="",targetPathForPDF="",targetPathForMP4="",tempString,recImgName, recImgUrl;
     
     public Text infoText,loading;
     public string[] splitString;
     VScannerButton vScannerButton;
    public VideoPlayer video;
    public GameObject pdfrend,menuCanvas,infopanel,imageTarget, cloudRecognition;
     bool touchHappened,pressed;
     public bool pdfChosen, videoChosen, bcontContScan,videoPlayerIsOpen,forceDownload,qrRecognized,bcontShowButton,mp4UrlExists,mp4HasBeenTested;
    public Camera camToUse;
    public CloudRecoEventHandler cloudRecoEventHandler;
    private bool imgRec,cloudRecoBtnVisible;
    private int clickCounter = 0, menuClick = 0,imgRecClick;
    
    
     bool urlOK;

 
    void Start()
    {
         cloudRecoEventHandler = GameObject.Find("Cloud Recognition").GetComponent<CloudRecoEventHandler>();
         cloudRecoBtnVisible = false;
         cloudRecoEventHandler.gameObject.SetActive(false);
        qrRecognized = false;
        bcontShowButton = false;
      imgRecClick = 0;
        forceDownload = false;
        infopanel.gameObject.SetActive(false);
        bcontContScan = true;
        mp4UrlExists = true;
        mp4HasBeenTested = false;
        menuCanvas = GameObject.Find("menuCanvas");
        menuCanvas.SetActive(false);
        vScannerButton = GameObject.Find("mainCanvas").GetComponent<VScannerButton>();
        
        pdfrend = GameObject.Find("PDFViewer");
        pdfrend.SetActive(false);
        closePdf.gameObject.SetActive(false);
        loading.gameObject.SetActive(false);
        pdf.gameObject.SetActive(false);
        vid.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        track.gameObject.SetActive(false);
             
        
        
    }
   

 
        
    public void buttonSizeUp(){
            if (clickCounter ==0){
            infoBtn.image.rectTransform.sizeDelta = new Vector2((infoBtn.image.rectTransform.sizeDelta.x*1.5f),(infoBtn.image.rectTransform.sizeDelta.y*1.5f));
            btnForceDownload.image.rectTransform.sizeDelta = new Vector2((btnForceDownload.image.rectTransform.sizeDelta.x*1.5f),(btnForceDownload.image.rectTransform.sizeDelta.y*1.5f));
            btnImgRecOff.image.rectTransform.sizeDelta = new Vector2((btnImgRecOff.image.rectTransform.sizeDelta.x*1.5f),(btnImgRecOff.image.rectTransform.sizeDelta.y*1.5f));
            closePdf.image.rectTransform.sizeDelta = new Vector2((closePdf.image.rectTransform.sizeDelta.x*1.5f),(closePdf.image.rectTransform.sizeDelta.y*1.5f));
            loading.fontSize = 30;
            infoText.fontSize = 30; 
             
              
             pdf.image.rectTransform.sizeDelta = new Vector2((pdf.image.rectTransform.sizeDelta.x*1.5f),(pdf.image.rectTransform.sizeDelta.y*1.5f));
             vid.image.rectTransform.sizeDelta = new Vector2((vid.image.rectTransform.sizeDelta.x*1.5f),(vid.image.rectTransform.sizeDelta.y*1.5f));
             play.image.rectTransform.sizeDelta = new Vector2((play.image.rectTransform.sizeDelta.x*1.5f),(play.image.rectTransform.sizeDelta.y*1.5f));
             pause.image.rectTransform.sizeDelta = new Vector2((pause.image.rectTransform.sizeDelta.x*1.5f),(pause.image.rectTransform.sizeDelta.y*1.5f));
             stop.image.rectTransform.sizeDelta = new Vector2((stop.image.rectTransform.sizeDelta.x*1.5f),(stop.image.rectTransform.sizeDelta.y*1.5f));
             menu.image.rectTransform.sizeDelta = new Vector2((menu.image.rectTransform.sizeDelta.x*1.5f),(menu.image.rectTransform.sizeDelta.y*1.5f));
             btnSizeDown.image.rectTransform.sizeDelta = new Vector2((btnSizeDown.image.rectTransform.sizeDelta.x*1.5f),(btnSizeDown.image.rectTransform.sizeDelta.y*1.5f));
             btnSizeUp.image.rectTransform.sizeDelta = new Vector2((btnSizeUp.image.rectTransform.sizeDelta.x*1.5f),(btnSizeUp.image.rectTransform.sizeDelta.y*1.5f));
             menuBgImg.rectTransform.sizeDelta = new Vector2((menuBgImg.rectTransform.sizeDelta.x*1.5f),(menuBgImg.rectTransform.sizeDelta.y*1.5f));
             
             clickCounter++;
            }
    }

    public void buttonSizeDown(){
            if (clickCounter ==1){
            infoBtn.image.rectTransform.sizeDelta =new Vector2(80, 80);
            btnForceDownload.image.rectTransform.sizeDelta =new Vector2(80, 80);
            btnImgRecOff.image.rectTransform.sizeDelta = new Vector2(80, 80);
            closePdf.image.rectTransform.sizeDelta =  new Vector2(80, 80);
            loading.fontSize = 20;   
            pdf.image.rectTransform.sizeDelta = new Vector2(80, 80);
            btnSizeDown.image.rectTransform.sizeDelta = new Vector2(80, 80);
            btnSizeUp.image.rectTransform.sizeDelta = new Vector2(80, 80);
            infoText.fontSize = 20;
              
             vid.image.rectTransform.sizeDelta =  new Vector2(80, 80);
             play.image.rectTransform.sizeDelta =  new Vector2(80, 80);
             pause.image.rectTransform.sizeDelta =  new Vector2(80, 80);
             stop.image.rectTransform.sizeDelta =  new Vector2(80, 80);
             menu.image.rectTransform.sizeDelta =  new Vector2(80, 80);
             menuBgImg.rectTransform.sizeDelta =  new Vector2(194, 344);
             clickCounter++;
            }
            clickCounter = 0;
    }
   
    public void openMenu(){
        if (menuClick ==0) {
            menuCanvas.SetActive(true);
            menuClick++;
        }
        else if (menuClick ==1) {
            menuCanvas.SetActive(false);
            menuClick = 0;
        }
    }
  
    public void Update()
    {
       
         if (bcontShowButton && !videoPlayerIsOpen && !cloudRecoEventHandler.showButton && inputUrlString != "") {
            
            btnQrRecognized.gameObject.SetActive(true);
            if (btnQrRecognized.IsActive()){
                 if (btnQrRecognized.gameObject.GetComponentInChildren<Text>().IsActive() && splitString != null) {
                     btnQrRecognized.gameObject.GetComponentInChildren<Text>().text = "Kohde tunnistettu: "+ splitString[splitString.Length-1] + "\n" +" Paina nollataksesi tunnistetiedot.";
      }
            }
           
             
        }
        else if (!bcontShowButton){
            btnQrRecognized.gameObject.SetActive(false);
        }
       //jatkuva skannaus poist päältä jos infopanel auki.
        if(infopanel.gameObject.activeInHierarchy){
            bcontContScan = false;
        }
        if (!cloudRecognition.gameObject.activeInHierarchy) {
            btnImgRecognized.gameObject.SetActive(false);
        }
      
         //allaolevat 3 iffiä ovat videoUIn piilotus/näyttämistä varten videon pyöriessä
         if (Input.GetMouseButton(0)){
            touchHappened = true;
            Invoke("touchTimer",2);
        }
        
       if (video.isPlaying && !touchHappened ) {
          Invoke("playing",2 );
       }
        
        if ((touchHappened && video.isPlaying) || (touchHappened && video.isPaused)){
        x = "videoPlayerOpen";
    }
        if (mp4HasBeenTested && !File.Exists(targetPathForMP4)){
            vid.gameObject.SetActive(false);
        }
      if (imgRec) {
          btnImgRecOff.image.color = Color.white;
      }
       if (!imgRec) {
          btnImgRecOff.image.color = Color.red;
      }  
        //x-arvo vaihdetaan VScannerButton-scriptin kautta
        if (x != ""){
      
      //tämä switch case osio on UI elementtien piilotus/näyttämistä, skannauksen uudelleen aloitusta, url/paikallispolkujen muodostuksen laukaisua jne.
        switch (x) {
            
            case "default":
                play.gameObject.SetActive(false);
                menuBtn.gameObject.SetActive(true);
                pause.gameObject.SetActive(false);
                stop.gameObject.SetActive(false);
                track.gameObject.SetActive(false); 
                bcontContScan = true;
                infoText.gameObject.SetActive(true);
                infoBtn.gameObject.SetActive(true);
                loading.gameObject.SetActive(false);
                infoText.text = "Osoita QR koodia.  Onnistunut skannaus avaa valikon oikeaan reunaan.";
                videoPlayerIsOpen = false;

               
                pdf.gameObject.SetActive(true);
           
                closePdf.gameObject.SetActive(false);
                vScannerButton.restartScan = true;
                x = "";
                break;
                //näytä pdf ja vid-napit
            case "pdfvid":
              if (inputUrlString != ""){
                  Debug.Log("kutsuttu urlformia " + inputUrlString);
                 urlForming(inputUrlString);
               }
                pdf.gameObject.SetActive(true);
              
               
                if (!mp4HasBeenTested || File.Exists(targetPathForMP4)){
                    vid.gameObject.SetActive(true);
                }
              
                infoBtn.gameObject.SetActive(true);
                menuBtn.gameObject.SetActive(true);
                 if ((!File.Exists(targetPathForMP4) && mp4UrlExists) || !mp4HasBeenTested) {
                    Debug.Log("nyt pitaisi alkaa coroutine");
                    StartCoroutine(downloadMP4FromQr(formattedUrlForMP4));
                     
                    mp4HasBeenTested = true;
                }
            
                x= "";
               
                break;
            
            case "imgRecognized":
           
            if(recImgUrl != null) {
                urlForming(recImgUrl);
                pdf.gameObject.SetActive(true);
                vid.gameObject.SetActive(true);
              x="";
            }
                break;
               
            case "loading":
            
                bcontContScan = false;
                loading.gameObject.SetActive(true);
                hideVideoMenus();
              
                //jos inputurlstring == "" niin silloin on url jo muodostettu urlFormin(recImgUrl) kautta.
                if (!forceDownload){
                    if (inputUrlString != ""){
                        urlForming(inputUrlString);
                    }
                        fileInLocalDevice(targetPathForMP4);
                        x = "";
                }
                else if (forceDownload) {
                    loading.gameObject.SetActive(true);
                }
                 
                 x= "";
                break;

           
            case "videoPlayerOpen":
                videoPlayerIsOpen = true;
                infoText.gameObject.SetActive(false);
                loading.gameObject.SetActive(false);
                play.gameObject.SetActive(true);
                menuBtn.gameObject.SetActive(false);
                pause.gameObject.SetActive(true);
                stop.gameObject.SetActive(true);
                track.gameObject.SetActive(true);
                infoBtn.gameObject.SetActive(false);
                pdf.gameObject.SetActive(false);
                vid.gameObject.SetActive(false);
                btnQrRecognized.gameObject.SetActive(false);
             
              x = "";
                break;
            case "playing":
            if (!bcontContScan){
                infoBtn.gameObject.SetActive(false);
                loading.gameObject.SetActive(false);
                play.gameObject.SetActive(false);
                pdf.gameObject.SetActive(false);
                vid.gameObject.SetActive(false);
                pause.gameObject.SetActive(false);
                stop.gameObject.SetActive(false);
                track.gameObject.SetActive(false);
               
            }
            else if (bcontContScan){
                x = "default";
              
            }
                 x= "";
                break;
            case "pdfopen":
                infoBtn.gameObject.SetActive(false);
                infoText.gameObject.SetActive(false);
                bcontContScan = false;
                menuBtn.gameObject.SetActive(false);
                //jos inputurlstring == "" niin silloin on url jo muodostettu urlForm(recImgUrl) kautta.
               if (inputUrlString != ""){
                 urlForming(inputUrlString);
               }
                fileInLocalDevice(targetPathForPDF);
              
                closePdf.gameObject.SetActive(true);
     
                x = "";
                break;
           
            default:
             x = "";
             Debug.Log("defaultissa");
            
                break;
            
        }
       
    }
    }
  
   public void resetInfo(string hideButtonName){
       targetPathForMP4 = "";
       targetPathForPDF = "";
       inputUrlString = "";
       splitString = null;
       formattedUrl = "";
       formattedUrlForMP4 = "";
       formattedUrlForPDF = "";
       pdf.gameObject.SetActive(false);
       vid.gameObject.SetActive(false);
       mp4UrlExists = true;
        mp4HasBeenTested = false;
       if (hideButtonName == "bcontShowButton") {
           
             bcontShowButton = false;
              Debug.Log("bcontshowbutton: " + bcontShowButton);
       }
        if (hideButtonName == "showButton") {
           
            cloudRecoEventHandler.showButton = false;
       }
      
      
       
   }
    public void imgRecOnOff(){
        if (!imgRec && imgRecClick == 0){
             imageTarget.gameObject.SetActive(true);
            cloudRecognition.gameObject.SetActive(true);
            
            imgRec = true;
        Debug.Log("cloud rec: " + cloudRecognition.gameObject.activeInHierarchy);
        Debug.Log("imgtarget: " + imageTarget.gameObject.activeInHierarchy);
            imgRecClick++;
         
        }
        else if (imgRec && imgRecClick == 1) {
            Debug.Log("toisen iffin sisalla");
          
          imageTarget.gameObject.SetActive(false);
            cloudRecognition.gameObject.SetActive(false);
            imgRec = false;
           Debug.Log("cloud rec: " + cloudRecognition.gameObject.activeInHierarchy);
           Debug.Log("imgtarget: " + imageTarget.gameObject.activeInHierarchy);
           imgRecClick = 0;
        }
    }
   
    void touchTimer(){
       touchHappened = false;
       
    }
    void playing() {
        x = "playing";
    }
    void waiting() {
        x = "videoPlayerOpen";
    }
    public void hideVideoMenus(){
            play.gameObject.SetActive(false);
            pdf.gameObject.SetActive(false);
            vid.gameObject.SetActive(false);
            pause.gameObject.SetActive(false);
            stop.gameObject.SetActive(false);
            track.gameObject.SetActive(false); 
            pdf.gameObject.SetActive(false);
            vid.gameObject.SetActive(false);
               
    }

    public void updateUrl(string t) {
    
       
            pdfViewer.m_FileURL = t;
           
            video.url = t;
            video.Prepare();
         
  
    }
        
        //jakaa syötetyn urlin / merkkien kohdalta jotta saadaan esineen nimi (urlit pitää olla muotoa www.savonia/fi/esine/ jotta saataisiin esine-nimi talteen, muutoin urlin muodolla ei väliä kunhan se osoittaa aina kansioon, ei esim. "esine/esine.pdf").
        public void stringSplitting(string t) {
          
            splitString = t.Split('/');
              
      
                
    }
    //muodostetaan urlit ja kohdepolut.
    public void urlForming(string inputUrl){
        stringSplitting(inputUrl);
        Debug.Log("splitattu: " + splitString[splitString.Length-1]);
        urlOK = true;
          bcontShowButton = true;
        formattedUrlForMP4 = inputUrl.Substring(0, inputUrl.Length-4)+".mp4";
     Debug.Log("formattedformp4 url: " + formattedUrlForMP4);
     formattedUrlForPDF = inputUrl;
     
                
       
        
               //ei tarvi erikseen lisätä .pdf koska tulos sisältää sen
        targetPathForPDF = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-1];
        //leikataan .pdf pois nimestä ja korvataan se .mp4:llä
        targetPathForMP4 = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-1].Substring(0, splitString[splitString.Length -1].Length-4)+".mp4";
        Debug.Log("targetpathmp4 : " + targetPathForMP4);
        Debug.Log("targetpathpdf : " + targetPathForPDF);
        
        
       
    }
 
    public void fileInLocalDevice(string t) {
       
            if (File.Exists(t)) {
                
                    if (t.EndsWith(".pdf")){
                        pdfrend.SetActive(true);
                        pdfViewer.LoadDocumentFromFile(t);
                    }
                    else if (t.EndsWith(".mp4")){
                  
                        video.url = t;
                        video.Prepare();
                        video.prepareCompleted += PrepareCompleted;       
                }
            
                    }
            
//filuja ei ole paikallisessa laitteessa, ladataan ne.
        else if (!File.Exists(t) ){
            if (t.EndsWith(".pdf")){
                
                StartCoroutine(waitCoroutine(formattedUrlForPDF));
                
            }
            else if (t.EndsWith(".mp4") ){
                Debug.Log("fileinlocal !file.existin sisalla");
                StartCoroutine(waitCoroutine(formattedUrlForMP4));
                
               
            }
                
                }
          else  {
              
            Debug.Log("virhe: fileInLocalDevice() ");
                }

    }

//"pakotettu" lataus.
    public void updateFiles(){
     
          
            StartCoroutine(waitCoroutine(inputUrlString));
           
        
    }
    public IEnumerator waitCoroutine(string s){
       
        //ladataan paikalliselle laitteelle
        if (s.EndsWith(".pdf")){
          
            yield return StartCoroutine((downloadPDFFromQr(s)));
            }
         if (s.EndsWith(".mp4")){
             
            yield return StartCoroutine((downloadMP4FromQr(s)));
            }

         else if (s.EndsWith("/")){
             forceDownload = true;
              x = "loading";
              //ensin ladataan pdf, sen valmistuttua ladataan mp4, sen valmistumisen jälkeen x-arvo vaihtuu jne. 
            yield return StartCoroutine(downloadPDFFromQr(formattedUrlForPDF));
            yield return StartCoroutine(downloadMP4FromQr(formattedUrlForMP4));
           x = "default";
           forceDownload =false;
            
            }
       
       //avataan paikalliselta laitteelta latauksen jälkeen.
        yield return new WaitForSeconds(1);
        if (s.EndsWith(".pdf")){
              
                pdfrend.SetActive(true);
               fileInLocalDevice(targetPathForPDF);
            }
            else if (s.EndsWith(".mp4")){
                
               
                fileInLocalDevice(targetPathForMP4);
            }
        yield return null;
         
    }

    void PrepareCompleted(VideoPlayer video){
        x ="videoPlayerOpen";
    }
   
  
      
         public IEnumerator downloadPDFFromQr(string t) {
          Debug.Log("dlpdf  kutsuttu");
       
            UnityWebRequest requ =  UnityWebRequest.Get(formattedUrlForPDF);
            //luo kansio downloadedfiles asennuspolkuun jonne kaikki tiedostot ladataan 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/");

            yield return requ.SendWebRequest();
           if(!requ.isNetworkError || !requ.isHttpError) {
                try {
                   
                    File.WriteAllBytes(targetPathForPDF, requ.downloadHandler.data);
                
                }
         
              catch {
                     Debug.Log("virhe: downloadPDFformQR()");
              }
            }
           else if(requ.isNetworkError || requ.isHttpError) {
                 infoText.text = "Ongelma internetyhteydessä!";
             }
         }
   public IEnumerator downloadMP4FromQr(string t) {
            Debug.Log("dlmp4 kutsuttu");
                    
            UnityWebRequest requ2 = UnityWebRequest.Get(formattedUrlForMP4);
          
            //luo kansio downloadedfiles asennuspolkuun jonne kaikki tiedostot ladataan 
             
        
                System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/");

                yield return requ2.SendWebRequest();
                if(!requ2.isNetworkError || !requ2.isHttpError) {  
                    try {
                        if(requ2.downloadHandler.data.Length > 2000){
                                File.WriteAllBytes(targetPathForMP4, requ2.downloadHandler.data);
                                mp4HasBeenTested = true;
                            }
                        else {
                            mp4UrlExists = false;
                            x = "default";
                            Debug.Log("tiedostoa ei olemassa");
                            mp4HasBeenTested = true;
                            yield break;
                            
                        }
                        }
            
                catch {
                    Debug.Log("virhe: downloadMP4fromqr()");
                }
                }

                else if(requ2.isNetworkError || requ2.isHttpError ) {
                      mp4UrlExists = false;
                        x = "default";
                       
                    infoText.text = "Ongelma internetyhteydessä!";
                     yield break;
                }
   }
}

