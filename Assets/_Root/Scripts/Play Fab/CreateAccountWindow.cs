using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField] private TMP_InputField _mailField;

    [SerializeField] private Button _createAccountButton;

    private string _mail;
    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();
        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        StartCoroutine(LoadSceneCor());
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _password
        }, result =>
        {
            Debug.Log($"Success: {_username}");
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
        });
    }

    private void UpdateMail(string mail)
    {
        _mail = mail;
    }
}
