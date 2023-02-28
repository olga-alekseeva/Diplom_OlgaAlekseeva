using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class SlotCharacterWidget : MonoBehaviour
{
    [SerializeField] private Button _createChar;
    [SerializeField] private GameObject _emptySlot;
    [SerializeField] private GameObject _infoCharacterSlot;

    [SerializeField] private TMP_Text _nameLabel;
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private TMP_Text _goldLabel;

    [SerializeField] private TMP_Text _healthLabel;
    [SerializeField] private TMP_Text _experienceLabel;
    [SerializeField] private TMP_Text _damageLabel;
    public Button SlotButton => _createChar;
    public void ShowInfoCharacterSlot(string name, string level, string gold, string health, string experience, string damage)
    {
        _nameLabel.text = name;
        _levelLabel.text = $"Level: {level}";
        _goldLabel.text = $"Gold: {gold}";
        _healthLabel.text = $"Health: {health}";
        _experienceLabel.text = $"EXP: {experience}";
        _damageLabel.text = $"damage: {damage}";
        _infoCharacterSlot.SetActive(true);
        _emptySlot.SetActive(false);
    }
    public void ShowEmptySlot()
    {
        _infoCharacterSlot.SetActive(false);
        _emptySlot.SetActive(true);
    }
}


