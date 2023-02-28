using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

 public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleLabel;

    [SerializeField] private GameObject _newCharacterCreatePanel;
    [SerializeField] private Button _createCharacterButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private List<SlotCharacterWidget> _slots;
    private string _characterName;
    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
        OnGetAccountSuccess, OnFailure);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(),
            OnGetCatalogSuccess, OnFailure);
        

        GetCharacters();
        foreach (var slot in _slots)
            slot.SlotButton.onClick.AddListener(OpenCreateNewCharacter);
        _inputField.onValueChanged.AddListener(OnNameChanged);
        _createCharacterButton.onClick.AddListener(CreateCharacter);
    }

    private void CreateCharacter()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = _characterName,
            ItemId = "character_token"
        }, result =>
        {
            UpdateCharacterStatistics(result.CharacterId);
            Debug.Log($"Success granted char");
        }, OnFailure);
    }

    private void UpdateCharacterStatistics(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics(new UpdateCharacterStatisticsRequest
        {
            CharacterId = characterId,
            CharacterStatistics = new Dictionary<string, int>
            {
                {"Level", 1 },
                {"Gold", 0 },
                {"health", 10 },
                {"damage", 10 },
                {"exp", 10 }
            }
        }, result =>
        {
            Debug.Log("Complete updating");
            CloseCreateNewCharacter();
            GetCharacters();
        }, OnFailure);
    }

    private void OnNameChanged(string changedName)
    {
        _characterName = changedName;
    }

    private void OpenCreateNewCharacter()
    {
        _newCharacterCreatePanel.SetActive(true);
    }
    private void CloseCreateNewCharacter()
    {
        _newCharacterCreatePanel.SetActive(true);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
            result =>
            {
                Debug.Log($"Character count : {result.Characters.Count}");
                ShowCharactersInSlot(result.Characters);
            }, OnFailure);
    }

    private void ShowCharactersInSlot(List<CharacterResult> characters)
    {
        if (characters.Count == 0)
        {
            foreach (var slot in _slots)
                slot.ShowEmptySlot();
        }
        else if (characters.Count > 0 && characters.Count < _slots.Count)
        {
            PlayFabClientAPI.GetCharacterStatistics(new GetCharacterStatisticsRequest
            {
                CharacterId = characters.First().CharacterId
            },
            result =>
            {
                var level = result.CharacterStatistics["Level"].ToString();
                var gold = result.CharacterStatistics["Gold"].ToString();
                var health = result.CharacterStatistics["health"].ToString();
                var experience = result.CharacterStatistics["experience"].ToString();
                var damage = result.CharacterStatistics["damage"].ToString();






                _slots.First().ShowInfoCharacterSlot(characters.First().CharacterName, level, gold, health,experience,damage);

            }, OnFailure);
        }
        else
        {
            Debug.LogError("add slots for characters");
        }
    }

    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        Debug.Log("OnGetCatalogSuccess");
        ShowItems(result.Catalog);
    }

    private void ShowItems(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            Debug.Log($"{item.ItemId}");
        }
    }

    private void OnGetAccountSuccess(GetAccountInfoResult result)
    {
        _titleLabel.text = $"Welcome back, Player ID {result.AccountInfo.PlayFabId}, Player accout {result.AccountInfo.Username}";
    }
    private void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }
}
