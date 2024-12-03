using UnityEngine;
/// <summary>
/// Uses to store card information
/// </summary>
[CreateAssetMenu(fileName ="NewCard",menuName ="BAHMAN/Task2/New Card",order =1)]
public class CardInfo : ScriptableObject
{
    /// <summary>
    /// The name of the card uses for compare means
    /// </summary>
    public string CardName;

    /// <summary>
    /// The sprite to show on card
    /// </summary>
    public Sprite CardFaceSprite;
}
