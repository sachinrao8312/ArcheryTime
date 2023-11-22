using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrientationHandler : MonoBehaviour
{   
    public string landScape;
    public string portrait;

    // Start is called before the first frame update
    void Start()
    {
        UpdateOrientation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation != ScreenOrientation.Unknown)
        {
            UpdateOrientation();
        }
    }

    void UpdateOrientation()
    {
        bool isPortrait = Screen.orientation == ScreenOrientation.Portrait ||
        Screen.orientation == ScreenOrientation.PortraitUpsideDown ;

        if(isPortrait)
        {
            SceneManager.LoadScene("PortraitScene");
        }
        else
        {
            SceneManager.LoadScene("LandScapeScene");
        }
    }


}
