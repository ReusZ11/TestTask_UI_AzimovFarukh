using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// Скрипт для управления персами на экране выбора
/// </summary>
public class CharactersManager : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private List<CharacterData> availableCharacters = new List<CharacterData>();
    [SerializeField] private Character characterPrefab;

    [Header("UI")]
    [SerializeField] private Transform charactersPanelContent;
    [SerializeField] private Image selectedCharacterDisplay;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI characterLevelText;

    private List<Character> spawnedCharacters = new List<Character>();
    private Character currentSelectedCharacter;

    private bool animationStarted = false;

    private void Start()
    {
        SpawnCharacters();

        if (spawnedCharacters.Count > 0)
        {
            SelectCharacter(spawnedCharacters[0]);
        }
    }

    private void SpawnCharacters()
    {
        foreach (var character in spawnedCharacters)
        {
            if (character != null)
            {
                character.OnCharacterSelected -= OnCharacterSelected;
                Destroy(character.gameObject);
            }
        }
        spawnedCharacters.Clear();

        for (int i = 0; i < availableCharacters.Count; i++)
        {
            if (availableCharacters[i] == null) continue;

            Character newCharacter = Instantiate(characterPrefab, charactersPanelContent);

            newCharacter.Initialize(availableCharacters[i]);

            newCharacter.OnCharacterSelected += OnCharacterSelected;

            newCharacter.transform.localScale = Vector3.one;

            spawnedCharacters.Add(newCharacter);

            foreach (var character in spawnedCharacters)
            {
                if (character != null)
                {
                    DOTween.Sequence()
                        .AppendInterval(2.0f)
                        .AppendCallback(() => {
                            character.AnimateExperienceUpdate(character.Data.Level / 100f);
                        });
                }
            }
        }
    }


    private void OnCharacterSelected(Character character)
    {
        SelectCharacter(character);
    }


    private void SelectCharacter(Character character, bool animate = true)
    {
        if (character == currentSelectedCharacter)
            return;

        if (currentSelectedCharacter != null)
            currentSelectedCharacter.SetSelected(false);

        currentSelectedCharacter = character;
        character.SetSelected(true);

        if (animate)
        {
            AnimateSwitchCharacter(character.Data);
        }
        else
        {
            UpdateSelectedCharacterDisplay(character.Data);
        }
    }

    private void UpdateSelectedCharacterDisplay(CharacterData data)
    {

        selectedCharacterDisplay.sprite = data.CharacterFullImage;
        characterNameText.text = data.CharacterName;
        characterLevelText.text = $"Level {data.Level}";
    }


    public void AnimateCharactersAppear(float initialDelay = 0.3f, float delayBetweenCharacters = 0.1f)
    {
        if (animationStarted) return;
        animationStarted = true;

        foreach (var character in spawnedCharacters)
        {
            character.transform.localScale = Vector3.one;   
        }
    }

    private void AnimateSwitchCharacter(CharacterData data)
    {
        Sequence switchSequence = DOTween.Sequence();

        switchSequence.Append(
            selectedCharacterDisplay.transform.DOScale(0, 0.4f / 2)
                .SetEase(Ease.OutQuad)
        );

        switchSequence.AppendCallback(() => {
            selectedCharacterDisplay.sprite = data.CharacterFullImage;
            characterNameText.text = data.CharacterName;
            characterLevelText.text = $"Level {data.Level}";
        });

        switchSequence.Append(
            selectedCharacterDisplay.transform.DOScale(1, 0.4f / 2)
                .SetEase(Ease.OutBack)
        );

        switchSequence.Play();
    }

    public Character GetSelectedCharacter()
    {
        return currentSelectedCharacter;
    }

}