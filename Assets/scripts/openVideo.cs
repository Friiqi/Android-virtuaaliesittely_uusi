using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class openVideo : MonoBehaviour
{
    // Start is called before the first frame update
    //public Button QRBtn;
   
   public VideoPlayer video;
    buttonControl bcont;
    string currentUrl;
   void Start(){
     bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
     
   }
   
       //VScannerButton vsb = readQR.GetComponent<VScannerButton>();
   public void videoThingy()
    {
        
        //nettiyhteys toimii joten haetaan video suoraan netistä
         bcont.videoChosen = true;
         bcont.x = "loading";
     
        
        //string loc = Application.persistentDataPath +"/720/big_buck_bunny_720p_1mb.mp4";
       //  video.url = loc;
        
          //"https://c62506104e66a43471f5-70bb4dd68e9719e25721f0fcbc9e8812.ssl.cf1.rackcdn.com/videos/Fight_Back_with_HIV_Treatment.mp4";
          
          //bcont.videoChosen = true;
      
    //Invoke("videoUI", 3);
  
    }


    void videoUI() {
       bcont.x = "videoPlayerOpen";
    }

}
