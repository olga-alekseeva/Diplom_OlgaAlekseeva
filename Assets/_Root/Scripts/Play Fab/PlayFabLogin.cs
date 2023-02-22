using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayFabLogin : MonoBehaviour

{
    [SerializeField] private TMP_Text _createErrorLabel;
    [SerializeField] private TMP_Text _signInErrorLabel;
    private string _mail;
    private string _pass;
    private string _username;
    private const string AuthGuidKey = "authorization-guid";
    public void Start()
    {
        // Here we need to check whether TitleId property is configured in settings or not
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            * If not we need to assign it to the appropriate variable manually
            * Otherwise we can just remove this if statement at all
            */
            PlayFabSettings.staticSettings.TitleId = " A823B";
        }
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = id,
            CreateAccount = !needCreation
        }, success => { PlayerPrefs.SetString(AuthGuidKey, id); }, OnFailure);
    }

    public void UpdateUsername(string username)
    {
        _username = username;
    }
    public void UpdateEmail(string mail)
    {
        _mail = mail;
    }
    public void UpdatePassword(string pass)
    {
        _pass = pass;
    }
    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _pass,
            RequireBothUsernameAndEmail = true
        }, OnCreateSuccess, OnFailure);
    }
    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _pass
        }, OnSignInSuccess, OnFailure);
    }

    public void Back()
    {
        _createErrorLabel.text = "";
        _signInErrorLabel.text = "";
    }
    private void OnCreateSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"Creation Success: {_username}");
    }
    private void OnSignInSuccess(LoginResult result)
    {
        Debug.Log($"Sign In Success: {_username}");
        SetUserData(result.PlayFabId);
    }

    private void SetUserData(string playFabId)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"_time_receive_daily_reward", DateTime.UtcNow.ToString()}
            }
        },
        result =>
        {
            Debug.Log("SetUserData");
            GetUserData(playFabId, "_time_receive_daily_reward");
        }, OnFailure);
    }

    private void GetUserData(string playFabId, string keyData)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = playFabId
        }, result =>
        {
            if (result.Data.ContainsKey(keyData))
                Debug.Log($"{keyData}: {result.Data[keyData].Value}");
        }, OnFailure);
    }

    private void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
        _createErrorLabel.text = errorMessage;
        _signInErrorLabel.text = errorMessage;
    }

}
