using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRecoControl : MonoBehaviour
{
    GameObject cloudreco;
    void Start()
    {
        cloudreco = GameObject.Find("Cloud Recognition");
    }

  public void recoOn() {
      cloudreco.SetActive(true);
  }
    public void recoOff() {
      cloudreco.SetActive(false);
  }
}
