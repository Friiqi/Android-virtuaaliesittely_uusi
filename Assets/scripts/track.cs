using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;



public class track : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   //public GameObject vid2 = GameObject.Find("VideoPlayer");
    //public AudioSource audioVol = vid2.GetComponent<;
    
    public VideoPlayer video;
    public Slider audioVolumeSlider; //= video.SetDirectAudioVolume(0, Volume().audioVolumeSlider.value);
    Slider tracking;
    bool slide = false;

    // Start is called before the first frame update
    void Start()
    {
        tracking = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData a) {
        slide = true;
    }

    public void OnPointerUp(PointerEventData a) {
        float frame = (float) tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
         slide = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!slide && video.isPlaying){
        tracking.value = (float)video.frame / (float)video.frameCount;
        }

    }

    public void Volume() {
        //audioVol.volume = audioVolumeSlider.value;
    }
}
