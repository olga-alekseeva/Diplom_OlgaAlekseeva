using System;
using UnityEngine;
using UnityEngine.UI;

internal sealed class EnterGameWindow : MonoBehaviour
{
    [SerializeField] public Button _signInButton;
    [SerializeField] public Button _createAccountButton;
    [SerializeField] public Canvas _enterInGameCanvas;
    [SerializeField] public Canvas _createAccountCanvas;
    [SerializeField] public Canvas _signInCanvas;
    private void Start()
    {
        DefaultState();
        _signInButton.onClick.AddListener(OpenSignInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
    }

    private void OpenSignInWindow()
    {
        _enterInGameCanvas.enabled = false;
        _signInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
    private void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled= true;
        _enterInGameCanvas.enabled= false;
        _enterInGameCanvas.enabled = false;

    }
    private void DefaultState()
    {
        _enterInGameCanvas.enabled = true;
        _createAccountCanvas.enabled = false;
        _signInCanvas.enabled = false;
    }
}
