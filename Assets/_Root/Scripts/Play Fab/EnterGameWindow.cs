using UnityEngine;
using UnityEngine.UI;

public class EnterGameWindow : MonoBehaviour
{
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    [SerializeField] private Canvas _signInCanvas;
    private void Start()
    {
        _enterInGameCanvas.enabled = true;
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
}
