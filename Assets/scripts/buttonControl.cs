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
    public Button pdf,vid,play,pause,stop, closePdf,menu,btnSizeUp,btnSizeDown,btnForceDownload,infoBtn;
    public Image menuBgImg;
   
     
     public Slider track;
      public PDFViewer pdfViewer;
     public string x="",inputUrlString ="",formattedUrlForPDF="",formattedUrlForMP4="",formattedUrl="",targetPathForPDF="",targetPathForMP4="",tempString,recImgName, recImgUrl;
     
     public Text infoText,loading;
     public string[] splitString;
     VScannerButton vScannerButton;
    public VideoPlayer video;
    public GameObject pdfrend,menuCanvas,infopanel;
     bool touchHappened,pressed;
     public bool pdfChosen, videoChosen, bcontContScan,videoPlayerIsOpen,forceDownload;
    public Camera camToUse;
    private int clickCounter = 0, menuClick = 0;
    //public CloudRecoEventHandler cloudRecoEventHandler;
  
     bool urlOK;

 
    void Start()
    { 
        
      
        //cloudRecoEventHandler = GameObject.Find("Cloud Recognition").GetComponent<CloudRecoEventHandler>();
       
        infopanel.gameObject.SetActive(false);
        bcontContScan = true;
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
       //jatkuva skannaus poist päältä jos infopanel auki.
        if(infopanel.gameObject.activeInHierarchy){
            bcontContScan = false;
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
      
        
        //x-arvo vaihdetaan VScannerButton-scriptin kautta
        if (x != ""){
      
      //tämä switch case osio on UI elementtien piilotus/näyttämistä, skannauksen uudelleen aloitusta, url/paikallispolkujen muodostuksen laukaisua jne.
        switch (x) {
            
            case "default":
                hideVideoMenus();
                bcontContScan = true;
                infoText.gameObject.SetActive(true);
                infoBtn.gameObject.SetActive(true);
                loading.gameObject.SetActive(false);
                infoText.text = "Osoita QR koodia.  Onnistunut skannaus avaa valikon oikeaan reunaan.";
                videoPlayerIsOpen = false;
                x = "";
                
                closePdf.gameObject.SetActive(false);
                vScannerButton.restartScan = true;
                break;
                //näytä pdf ja vid-napit
            case "pdfvid":
              urlForming(inputUrlString);
                pdf.gameObject.SetActive(true);
                vid.gameObject.SetActive(true);
                
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
                break;

           
            case "videoPlayerOpen":
                videoPlayerIsOpen = true;
                infoText.gameObject.SetActive(false);
                loading.gameObject.SetActive(false);
                play.gameObject.SetActive(true);
            
                pause.gameObject.SetActive(true);
                stop.gameObject.SetActive(true);
                track.gameObject.SetActive(true);
                infoBtn.gameObject.SetActive(false);
                pdf.gameObject.SetActive(false);
                vid.gameObject.SetActive(false);
             
              x = "";
                break;
            case "playing":
                infoBtn.gameObject.SetActive(false);
                loading.gameObject.SetActive(false);
                hideVideoMenus();
       
                x = "";
                break;
            case "pdfopen":
                infoBtn.gameObject.SetActive(false);
                infoText.gameObject.SetActive(false);
                bcontContScan = false;
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
                break;
            
        }
       
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
        urlOK = true;
        formattedUrlForPDF = inputUrl+ splitString[splitString.Length-2]+".pdf";
     
                
        formattedUrlForMP4 = inputUrl+ splitString[splitString.Length-2]+".mp4";
        
               
        targetPathForPDF = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".pdf";
     
        targetPathForMP4 = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".mp4";
        
       
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
            

        else if (!File.Exists(t) ){
            if (t.EndsWith(".pdf")){
                
                StartCoroutine(waitCoroutine(formattedUrlForPDF));
                
            }
            else if (t.EndsWith(".mp4") ){
                
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
          
       
            UnityWebRequest requ =  UnityWebRequest.Get(formattedUrlForPDF);
            //luo kansio toiseksi viimeisen /-merkin jälkeisellä tekstillä nimettynä (tarkoitus olisi että se tulisi olemaan ko. esineen nimi, esim url muotoa www.savonia.fi/esine/ jolloin kansion nimi olisi "esine" ja oletamme että sieltä löytyy esine.mp4 ja esine.pdf ja että kaikki ko. esineeseen viitaavat asiat löytyvät sieltä) 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/" + splitString[splitString.Length-2]);

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
           
     
            UnityWebRequest requ2 = UnityWebRequest.Get(formattedUrlForMP4);
            //luo kansio toiseksi viimeisen /-merkin jälkeisellä tekstillä nimettynä (tarkoitus olisi että se tulisi olemaan ko. esineen nimi, esim url muotoa www.savonia.fi/esine/ jolloin kansion nimi olisi "esine" ja oletamme että sieltä löytyy esine.mp4 ja esine.pdf ja että kaikki ko. esineeseen viitaavat asiat löytyvät sieltä) 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/" + splitString[splitString.Length-2]);

            yield return requ2.SendWebRequest();
            if(!requ2.isNetworkError || !requ2.isHttpError) {  
                try {
              
                    
                    File.WriteAllBytes(targetPathForMP4, requ2.downloadHandler.data);
                }
         
              catch {
                  Debug.Log("virhe: downloadMP4fromqr()");
              }
            }

            else if(requ2.isNetworkError || requ2.isHttpError) {
                 infoText.text = "Ongelma internetyhteydessä!";
             }


    }
}

