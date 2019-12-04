using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openURL : MonoBehaviour
{
    public string url;

  public void OnMouseOver(){

    if(Input.GetMouseButtonDown(0) ){
        Application.OpenURL('"' + url +'"');
    Debug.Log(url);

    }
 }

}
