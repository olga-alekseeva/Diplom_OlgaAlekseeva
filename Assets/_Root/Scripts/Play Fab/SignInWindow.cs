using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class SignInWindow : AccountDataWindowBase
{
    [SerializeField] private Button _signInButton;
    private EnterGameWindow _enterGameWindow;

    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();
        _signInButton.onClick.AddListener(SignIn);
    }
    private void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _password
        }, result =>
        {
            Debug.Log($"Success: {_username}");
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
        }); 
    }
}
