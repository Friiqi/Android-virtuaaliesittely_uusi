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

   void Start(){
     bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
     
   }
   
      
   public void videoThingy()
    {
 
         bcont.videoChosen = true;
         bcont.x = "loading";

    }


    void videoUI() {
       bcont.x = "videoPlayerOpen";
    }

}
