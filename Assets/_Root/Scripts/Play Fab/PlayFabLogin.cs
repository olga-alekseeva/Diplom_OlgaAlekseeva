using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour

{
    [SerializeField] public Button loginButton;
    [SerializeField] public Text failAnswer;
    [SerializeField] public Text successAnswer;
   
    public void Start()
    {
        loginButton.onClick.AddListener(() => Login());
    }
   
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made successfulAPI call!");
        successAnswer.text = "Congratulations, you made successfulAPI call!";
        successAnswer.color = Color.green;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong:{errorMessage}");
        failAnswer.text = errorMessage;
        failAnswer.color = Color.red;
    }

    private void Login()
    {

        // Here we need to check whether TitleId propertyis configured
        // in settings or not
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /* * If not we need to assign it to the appropriate variable manually *
             * Otherwise we can just remove this if statement at all */
            PlayFabSettings.staticSettings.TitleId = " A823B";
        }
        var request = new LoginWithCustomIDRequest { CustomId = "GeekBrainsLesson3", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
}
