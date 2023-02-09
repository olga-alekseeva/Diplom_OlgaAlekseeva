using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountDataWindowBase : MonoBehaviour
{
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Canvas _loadingCanvas;
    [SerializeField] private TMP_Text _loadingText;
    AsyncOperation asyncOperation;

    protected string _username;
    protected string _password;

    private void Start()
    {
        SubscriptionsElementsUi();
    }
    public IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        while(!asyncOperation.isDone)
        {
            _loadingText.text = "loading";
            yield return 0;
        }

    }
    protected virtual void SubscriptionsElementsUi()
    {
        _usernameField.onValueChanged.AddListener(UpdateUsername);
        _passwordField.onValueChanged.AddListener(UpdatePassword);
    }
    private void UpdatePassword(string password)
    {
        _password = password;
    }
    private void UpdateUsername(string username)
    {
        _username = username;
    }
}
