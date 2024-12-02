using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewGameSettings", menuName = "BAHMAN/Task2/New Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    private static GameSettings _instance;

    [Range(2, 6)]
    public int XDimension = 2;
    [Range(3, 6)]
    public int YDimension = 3;
    [Range(2, 4)]
    public int DeckVariation = 2;
    [Range(0, 2)]
    public float XOffset = .5f;
    [Range(0, 2)]
    public float YOffset = .5f;
    public CardInfo[] AllCards;
    public Vector2 DefaultColliderSize;
    public Sprite CardBackSprite;
    public static void Initialize()
    {
        if (_instance == null)
        {
            _instance = Resources.Load<GameSettings>("GameSettings");
        }
    }
    public CardInfo GetRandomCard()
    {
        return AllCards[Random.Range(0, DeckVariation)];
    }
    public static GameSettings Instance
    {
        get
        {
            
            return _instance;
        }

    }
    


}
