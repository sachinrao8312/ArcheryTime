using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShareButton : MonoBehaviour
{
    private bool isFocus = false;
    private bool isProcessing = false;

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    // Make the method public
    public void ShareTextInAndroid()
    {
#if UNITY_ANDROID
        if (!isProcessing)
        {
            StartCoroutine(ShareTextCoroutine());
        }
#else
        Debug.Log("No sharing set up for this platform.");
#endif
    }

#if UNITY_ANDROID
    private IEnumerator ShareTextCoroutine()
    {
        var shareSubject = "Check out My Game on the Play Store!"; // Subject text
        var shareMessage = "I'm playing this awesome game called POP IT! 🎮\n\n" +
                           "Get it now on the Google Play Store:\n" +
                           "https://play.google.com/store/apps/details?id=com.yourcompany.yourgame"; // Your actual Play Store link

        isProcessing = true;

        if (!Application.isEditor)
        {
            // Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            // Put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");

            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            // Call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your game");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
#endif
}
