using UnityEngine;

/// <summary>
/// ������ ��� �������� ������ ������
/// </summary>
[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Character Info")]
    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterIcon;
    [SerializeField] private Sprite characterFullImage;

    [Header("Stats")]
    [SerializeField][Range(0, 100)] private int level = 1;


    public string CharacterName => characterName;
    public Sprite CharacterIcon => characterIcon;
    public Sprite CharacterFullImage => characterFullImage;
    public int Level => level;

}