using UnityEngine;
using Vuforia;
using UnityEngine.UI;


public class CloudRecoEventHandler : MonoBehaviour, IObjectRecoEventHandler
{
   
    CloudRecoBehaviour m_CloudRecoBehaviour;
    ObjectTracker m_ObjectTracker;
    TargetFinder m_TargetFinder;
  
    public buttonControl bcont;
    public Button imgRecBtn;
    public ImageTargetBehaviour m_ImageTargetBehaviour;
    public UnityEngine.UI.Image m_CloudActivityIcon;
    public UnityEngine.UI.Image m_CloudIdleIcon;
    public string recogImgName, recogImgUrl;
      bool mIsScanning,showButton;



//tämä scipta hoitaa vuforian pilvitunnistuksen.

    void Start()
    {
        
         bcont = GameObject.Find("mainCanvas").GetComponent<buttonControl>();
         showButton = false;
        // Register this event handler at the CloudRecoBehaviour
        m_CloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (m_CloudRecoBehaviour)
        {
            m_CloudRecoBehaviour.RegisterEventHandler(this);
        }

        /*
        if (m_CloudActivityIcon)
        {
            m_CloudActivityIcon.enabled = false;
        } */
    }

    void Update()
    {
        if (m_CloudRecoBehaviour.CloudRecoInitialized && m_TargetFinder != null)
        {
            SetCloudActivityIconVisible(m_TargetFinder.IsRequesting());
        }
        if (showButton) {
            imgRecBtn.gameObject.SetActive(true);
            imgRecBtn.gameObject.GetComponentInChildren<Text>().text = "Kohde tunnistettu: " + bcont.recImgName + "\n" +" Paina jatkaaksesi kuvatunnistusta.";
        }
        if (!showButton){
            imgRecBtn.gameObject.SetActive(false);
        }
        /*
        if (m_CloudIdleIcon)
        {
            m_CloudIdleIcon.color = m_CloudRecoBehaviour.CloudRecoEnabled ? Color.white : Color.gray;
        } */
    }
  


    public void OnInitialized()
    {
        Debug.Log("Cloud Reco initialized successfully.");

        m_ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        m_TargetFinder = m_ObjectTracker.GetTargetFinder<ImageTargetFinder>();
    }

    public void OnInitialized(TargetFinder targetFinder)
    {
        Debug.Log("Cloud Reco initialized successfully.");

        m_ObjectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        m_TargetFinder = targetFinder;
    }

    // Error callback methods implemented in CloudErrorHandler
    public void OnInitError(TargetFinder.InitState initError) { }
    public void OnUpdateError(TargetFinder.UpdateState updateError) { }



    public void OnStateChanged(bool scanning)
    {
        Debug.Log("<color=blue>OnStateChanged(): </color>" + scanning);
        mIsScanning = true;
        // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call:
        // 1. TargetFinder.Stop()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with false.

        // Changing CloudRecoBehaviour.CloudRecoEnabled to true will call:
        // 1. TargetFinder.StartRecognition()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with true.
    }

  
    /// <param name="targetSearchResult"></param>
    public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
    {
        Debug.Log("<color=blue>OnNewSearchResult(): </color>" + targetSearchResult.TargetName);

        TargetFinder.CloudRecoSearchResult cloudRecoResult = (TargetFinder.CloudRecoSearchResult)targetSearchResult;

        // This code demonstrates how to reuse an ImageTargetBehaviour for new search results
        // and modifying it according to the metadata. Depending on your application, it can
        // make more sense to duplicate the ImageTargetBehaviour using Instantiate() or to
        // create a new ImageTargetBehaviour for each new result. Vuforia will return a new
        // object with the right script automatically if you use:
        // TargetFinder.EnableTracking(TargetSearchResult result, string gameObjectName)

        // Check if the metadata isn't null
        if (cloudRecoResult.MetaData == null)
        {
            Debug.Log("Target metadata not available.");
        }
        else
        {
            
         
          bcont.recImgName = targetSearchResult.TargetName;
          bcont.recImgUrl = cloudRecoResult.MetaData;
           //bcont.objectCreation();
            bcont.x = "imgRecognized";
            
           
            Debug.Log("MetaData: " + cloudRecoResult.MetaData);
            Debug.Log("TargetName: " + cloudRecoResult.TargetName);
            Debug.Log("Pointer: " + cloudRecoResult.TargetSearchResultPtr);
            //Debug.Log("TargetSize: " + cloudRecoResult.TargetSize);
            Debug.Log("TrackingRating: " + cloudRecoResult.TrackingRating);
            Debug.Log("UniqueTargetId: " + cloudRecoResult.UniqueTargetId);
        }

        // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call TargetFinder.Stop()
        // and also call all registered ICloudRecoEventHandler.OnStateChanged() with false.
        m_CloudRecoBehaviour.CloudRecoEnabled = false;
        mIsScanning = false;
        showButton = true;
        // Clear any existing trackables
        m_TargetFinder.ClearTrackables(false);

        // Enable the new result with the same ImageTargetBehaviour:
        m_TargetFinder.EnableTracking(cloudRecoResult, m_ImageTargetBehaviour.gameObject);

        // Pass the TargetSearchResult to the Trackable Event Handler for processing
        m_ImageTargetBehaviour.gameObject.SendMessage("TargetCreated", cloudRecoResult, SendMessageOptions.DontRequireReceiver);
        
    }
 void OnGUI() {
    // Display current 'scanning' status
    //GUI.Box (new Rect(100,100,200,50), mIsScanning ? "Scanning" : "Not scanning");
    // Display metadata of latest detected cloud-target
    //GUI.Box (new Rect(100,200,200,50), "Metadata: " + mTargetMetadata);
    // If not scanning, show button
    // so that user can restart cloud scanning
    /*
    if (!mIsScanning) {
        if (GUI.Button(new Rect(440,600,400,100), "Kohde tunnistettu: " + bcont.recImgName + "\n" +" Jatka kuvatunnistusta.")) {
        // Restart TargetFinder
        m_CloudRecoBehaviour.CloudRecoEnabled = true;
        }
    }
    */
}

public void restartScan(){
   
    m_CloudRecoBehaviour.CloudRecoEnabled = true;
    showButton = false;

}
   
    void SetCloudActivityIconVisible(bool visible)
    {
        /*
        if (!m_CloudActivityIcon) return;

        m_CloudActivityIcon.enabled = visible; */
    }
  
}