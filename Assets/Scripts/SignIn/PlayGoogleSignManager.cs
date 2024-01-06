using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class PlayGoogleSignManager : MonoBehaviour  {

    public TextMeshProUGUI detailsText;
    public void Start() {
        signIn();
    }
    
    void signIn() {
    PlayGamesPlatform.Instance.Authenticate((SignInStatus signInStatus) => {
        if (signInStatus == SignInStatus.Success) {
            Debug.Log("Signed in!");
        } else {
            Debug.Log("Sign-in failed");
        }
    });
    }


    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        // Continue with Play Games Services
        
        string name = PlayGamesPlatform.Instance.GetUserDisplayName();
        string id = PlayGamesPlatform.Instance.GetUserId();
        string imgurl = PlayGamesPlatform.Instance.GetUserImageUrl();

        detailsText.text = "Welcome " + name + " " + id + " " + imgurl;
      } else {
        // Disable your integration with Play Games Services or show a login button
        detailsText.text = "Sign-in failed";
        // to ask users to sign-in. Clicking it should call
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
      }

      
    }
}