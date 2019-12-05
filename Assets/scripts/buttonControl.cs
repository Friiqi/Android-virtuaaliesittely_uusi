using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Paroxe.PdfRenderer;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.EventSystems;







public class buttonControl : MonoBehaviour
{
    
    //skripti olemassa että voi piilottaa/näyttää buttoneita, koska ilman viittauksia (napit sijoitetaan inspectorissa tähän scriptiin) niitä ei voinut enää .setactive(true) (saada näkyväksi) kun kerran piilotettu (.setactive(false))
    public Button pdf,vid,play,pause,stop, closePdf,menu,btnSizeUp,btnSizeDown,btnForceDownload;
    public Image menuBgImg;
   
     
     public Slider track;
      public PDFViewer pdfViewer;
     public string x="",inputUrlString ="",formattedUrlForPDF="",formattedUrlForMP4="",formattedUrl="",targetPathForPDF="",targetPathForMP4="",tempString,recImgName, recImgUrl;
     
     public Text infoText,loading;
     public string[] splitString;
     VScannerButton vScannerButton;
    public VideoPlayer video;
    public GameObject pdfrend,menuCanvas,infopanel;
     bool touchHappened=false,urlFormed = false,pressed;
     public bool netError = false,pdfChosen = false, videoChosen=false,firstDownloadDone =false, bcontContScan,videoPlayerIsOpen;
    public Camera ARcam;
    private int clickCounter = 0, menuClick = 0;
    public CloudRecoEventHandler cloudRecoEventHandler;
    public List<string> jsonList;
    public List<recognizedObject> recObjList;
    private bool urlOK;

    public class recognizedObject {
        public string name;
        public string url;
    }

    void Start()
    { 
        
      
        cloudRecoEventHandler = GameObject.Find("Cloud Recognition").GetComponent<CloudRecoEventHandler>();
       
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
   
    /*
   public void objectCreation(){
       recognizedObject recObj = new recognizedObject();
       recObj.name = cloudRecoEventHandler.recogImgName;
       recObj.url = cloudRecoEventHandler.recogImgUrl;
       tempString = JsonUtility.ToJson(recObj);
       recObjList.Add(recObj);
     jsonList.Add(tempString);
   
     
   }
*/
 
        
    public void buttonSizeUp(){
            if (clickCounter ==0){
             closePdf.image.rectTransform.sizeDelta = new Vector2((closePdf.image.rectTransform.sizeDelta.x*1.5f),(closePdf.image.rectTransform.sizeDelta.y*1.5f));
             loading.fontSize = 30;
             infoText.fontSize = 30;    //rectTransform.sizeDelta = new Vector2(closePdf.image.rectTransform.sizeDelta.x*1.5f,closePdf.image.rectTransform.sizeDelta.y*1.5f);
             pdf.image.rectTransform.sizeDelta = new Vector2((pdf.image.rectTransform.sizeDelta.x*1.5f),(pdf.image.rectTransform.sizeDelta.y*1.5f));
             vid.image.rectTransform.sizeDelta = new Vector2((vid.image.rectTransform.sizeDelta.x*1.5f),(vid.image.rectTransform.sizeDelta.y*1.5f));
             play.image.rectTransform.sizeDelta = new Vector2((play.image.rectTransform.sizeDelta.x*1.5f),(play.image.rectTransform.sizeDelta.y*1.5f));
             pause.image.rectTransform.sizeDelta = new Vector2((pause.image.rectTransform.sizeDelta.x*1.5f),(pause.image.rectTransform.sizeDelta.y*1.5f));
             stop.image.rectTransform.sizeDelta = new Vector2((stop.image.rectTransform.sizeDelta.x*1.5f),(stop.image.rectTransform.sizeDelta.y*1.5f));
             menu.image.rectTransform.sizeDelta = new Vector2((menu.image.rectTransform.sizeDelta.x*1.5f),(menu.image.rectTransform.sizeDelta.y*1.5f));
             btnSizeDown.image.rectTransform.sizeDelta = new Vector2((btnSizeDown.image.rectTransform.sizeDelta.x*1.5f),(btnSizeDown.image.rectTransform.sizeDelta.y*1.5f));
             btnSizeUp.image.rectTransform.sizeDelta = new Vector2((btnSizeUp.image.rectTransform.sizeDelta.x*1.5f),(btnSizeUp.image.rectTransform.sizeDelta.y*1.5f));
             menuBgImg.rectTransform.sizeDelta = new Vector2((menuBgImg.rectTransform.sizeDelta.x*1.5f),(menuBgImg.rectTransform.sizeDelta.y*1.5f));
             //track.image.rectTransform.sizeDelta = new Vector2((closePdf.image.rectTransform.sizeDelta.x*1.5f),(closePdf.image.rectTransform.sizeDelta.y*1.5f));
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
       
        if(infopanel.gameObject.activeInHierarchy){
            bcontContScan = false;
        }
         
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
        //Debug.Log(x);
        switch (x) {
            
            case "default":
                hideVideoMenus();
                bcontContScan = true;
                infoText.gameObject.SetActive(true);
               
                infoText.text = "Osoita QR koodia.  Onnistunut skannaus avaa valikon oikeaan reunaan.";
                videoPlayerIsOpen = false;
                x = "";
                btnForceDownload.gameObject.SetActive(false);
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
            Debug.Log("recImgUrl = " + recImgUrl);
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
                if (inputUrlString != ""){
                    urlForming(inputUrlString);
                }
                fileInLocalDevice(targetPathForMP4);
        
                x = "";
               
                break;

           
            case "videoPlayerOpen":
                videoPlayerIsOpen = true;
                infoText.gameObject.SetActive(false);
                loading.gameObject.SetActive(false);
                play.gameObject.SetActive(true);
            
                pause.gameObject.SetActive(true);
                stop.gameObject.SetActive(true);
                track.gameObject.SetActive(true);
                
                pdf.gameObject.SetActive(false);
                vid.gameObject.SetActive(false);
             
              x = "";
                break;
            case "playing":
                loading.gameObject.SetActive(false);
                hideVideoMenus();
       
                x = "";
                break;
            case "pdfopen":
                
                infoText.gameObject.SetActive(false);
                bcontContScan = false;
                //jos inputurlstring == "" niin silloin on url jo muodostettu urlFormin(recImgUrl) kautta.
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
            Debug.Log("updateurl : " + t);
            video.url = t;
            video.Prepare();
         
  
    }
        public void stringSplitting(string t) {
            Debug.Log("splitataan: "+t);
            splitString = t.Split('/');
        foreach (string item in splitString) {
                Debug.Log(item);
                }
            Debug.Log(splitString[splitString.Length-1]);
                
    }
    //muodostetaan urlit ja kohdepolut.
    public void urlForming(string inputUrl){
        stringSplitting(inputUrl);
        urlOK = true;
        formattedUrlForPDF = inputUrl+ splitString[splitString.Length-2]+".pdf";
        Debug.Log("formattedUrlForPDF: " + formattedUrlForPDF);
                
        formattedUrlForMP4 = inputUrl+ splitString[splitString.Length-2]+".mp4";
        Debug.Log("formattedUrlForMp4: " + formattedUrlForMP4);
               
        targetPathForPDF = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".pdf";
        Debug.Log("targetPathForPDF: " + targetPathForPDF);
        targetPathForMP4 = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".mp4";
        Debug.Log("targetPathForMP4: " + targetPathForMP4);
       
    }
   
    public void fileInLocalDevice(string t) {
       
            if (File.Exists(t)) {
                Debug.Log("file existed : "+ t);
                    if (t.EndsWith(".pdf")){
                        pdfrend.SetActive(true);
                        pdfViewer.LoadDocumentFromFile(t);
                    }
                    else if (t.EndsWith(".mp4")){
                        Debug.Log("valmistaudutaan toistamaan videoa" );
                        video.url = t;
                        video.Prepare();
                        video.prepareCompleted += PrepareCompleted;       
                }
            
                    }
            

        else if (!File.Exists(t) ){
            if (t.EndsWith(".pdf")){
                Debug.Log("file " +t +" ei olemassa,ladataan netistä" + formattedUrlForPDF);
                StartCoroutine(waitCoroutine(formattedUrlForPDF));
                
            }
            else if (t.EndsWith(".mp4") ){
                Debug.Log("file " +t +" ei olemassa,ladataan netistä" + formattedUrlForMP4);
                StartCoroutine(waitCoroutine(formattedUrlForMP4));
                
               
            }
                
                }
          else  {
              
            Debug.Log("virhe: fileInLocalDevice() ");
                }

    }

    public void updateFiles(){
        if (urlOK){
            Debug.Log ("FORCED DOWNLOAD!");
        StartCoroutine(downloadMP4FromQr(formattedUrlForPDF));
        StartCoroutine(downloadPDFFromQr(formattedUrlForMP4));
        }
    }
    public IEnumerator waitCoroutine(string s){
        //ladataan paikalliselle laitteelle
        if (s.EndsWith(".pdf")){
            Debug.Log("PDF, ladataan polkuun: " + targetPathForPDF);
            yield return StartCoroutine((downloadPDFFromQr(s)));
            }
        else if (s.EndsWith(".mp4")){
            Debug.Log("MP4, ladataan: polkuun" + targetPathForMP4);   
            yield return StartCoroutine((downloadMP4FromQr(s)));
            }
       
       //avataan paikalliselta laitteelta
        yield return new WaitForSeconds(1);
        if (s.EndsWith(".pdf")){
                Debug.Log("PDF, fileInLocalDevice syöte: " + targetPathForPDF);
                pdfrend.SetActive(true);
               fileInLocalDevice(targetPathForPDF);
            }
            else if (s.EndsWith(".mp4")){
                 Debug.Log("MP4, fileInLocalDevice syöte: " + targetPathForMP4);
               
                fileInLocalDevice(targetPathForMP4);
            }
        yield return null;
         
    }

    void PrepareCompleted(VideoPlayer video){
        x ="videoPlayerOpen";
    }
   
   /*
       public IEnumerator downloadFileToLocal(string t) {
           Debug.Log("inputurlstring: "+inputUrlString);
           urlForming(inputUrlString);
         
           string target ="";
           Debug.Log("downloadtolocalin sisalla");
             
               Debug.Log("splitstring: 1 "+ splitString[splitString.Length-1] + " splitstring: 2 " + splitString[splitString.Length-2]);

               
            UnityWebRequest requ = UnityWebRequest.Get(formattedUrl);
            //luo kansio toiseksi viimeisen /-merkin jälkeisellä tekstillä nimettynä (tarkoitus olisi että se tulisi olemaan ko. esineen nimi, esim url muotoa www.savonia.fi/esineennimi/ jolloin kansion nimi olisi "esineennimi" ja oletamme että sieltä löytyy esine.mp4 ja esine.pdf ja että kaikki ko. esineeseen viitaavat asiat löytyvät sieltä) 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/" + splitString[splitString.Length-2]);

            yield return requ.SendWebRequest();
            
            //jos virhe tulee latausvaiheessa, tarkastetaan löytyykö esineen nimellä kansiota paikallisesta laitteesta ja sieltä tiedostoa
            if(requ.isNetworkError || requ.isHttpError) {
                Debug.Log("virhe: www errori: " +requ.error);
                netError = true;
                try {
                    if (pdfChosen) {
                        target = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".pdf";
                          
                         fileInLocalDevice(target);
                         pdfChosen = false;
                          netError = false;
                    }
                    else if (videoChosen) {
                        target = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".mp4";
                         fileInLocalDevice(target);
                         videoChosen = false;
                          netError = false;
                         
                    }
                   
                }
                catch {
                    Debug.Log("virhe: downloadFileToLocal() try fileInLocalDevice() catch");
                }
            } 
            else {
                try {
              if (pdfChosen){
                string targetP = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".pdf";
                Debug.Log("target path: " +targetP);
                File.WriteAllBytes(targetP, requ.downloadHandler.data);
               
                  fileInLocalDevice(target);
                         pdfChosen = false;
                         
              }
            
             else if (videoChosen){
                string targetP = Application.persistentDataPath + "/downloadedfiles/" + splitString[splitString.Length-2] +"/"+ splitString[splitString.Length-2]+".mp4";
                Debug.Log("target path: " +targetP);
                File.WriteAllBytes(targetP, requ.downloadHandler.data);
                 fileInLocalDevice(target);
                         videoChosen = false;
                         
               
              }
                }
              catch {
                  Debug.Log("virhe: downloadFiloeToLocal() sisällä");
              }
            }
        }
*/
      
         public IEnumerator downloadPDFFromQr(string t) {
           Debug.Log("downloadPDFfromqr syote : "+t);
       
            UnityWebRequest requ =  UnityWebRequest.Get(formattedUrlForPDF);
            //luo kansio toiseksi viimeisen /-merkin jälkeisellä tekstillä nimettynä (tarkoitus olisi että se tulisi olemaan ko. esineen nimi, esim url muotoa www.savonia.fi/esineennimi/ jolloin kansion nimi olisi "esineennimi" ja oletamme että sieltä löytyy esine.mp4 ja esine.pdf ja että kaikki ko. esineeseen viitaavat asiat löytyvät sieltä) 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/" + splitString[splitString.Length-2]);

            yield return requ.SendWebRequest();
           if(!requ.isNetworkError || !requ.isHttpError) {
                try {
                    Debug.Log("downloadPDFformQR target path: " +targetPathForPDF);
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
           Debug.Log("downloadMP4fromqr syote : "+t);
     
            UnityWebRequest requ2 = UnityWebRequest.Get(formattedUrlForMP4);
            //luo kansio toiseksi viimeisen /-merkin jälkeisellä tekstillä nimettynä (tarkoitus olisi että se tulisi olemaan ko. esineen nimi, esim url muotoa www.savonia.fi/esineennimi/ jolloin kansion nimi olisi "esineennimi" ja oletamme että sieltä löytyy esine.mp4 ja esine.pdf ja että kaikki ko. esineeseen viitaavat asiat löytyvät sieltä) 
          
            System.IO.Directory.CreateDirectory(Application.persistentDataPath+ "/downloadedfiles/" + splitString[splitString.Length-2]);

            yield return requ2.SendWebRequest();
            if(!requ2.isNetworkError || !requ2.isHttpError) {  
                try {
              
                    Debug.Log("downloadMP4fromqr target path: " +targetPathForMP4);
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

