using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "NewGameSettings", menuName = "BAHMAN/Task2/New Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    private static GameSettings _instance;
    public int DefaultCameraWidth = 480;
    public int DefaultCameraHeight = 800;
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
    [Range(0, 2)]
    public float CameraBuffer = 1f;
    [Range(0, 2)]
    public float AnimationWaitTime = .8f;
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
    public (Vector3 oCenter, float oSize) CalculateCamSize(GameObject[] iGameCards)
    {
        var bounds = new Bounds();

        foreach (var iGameCard in iGameCards)
        {
            bounds.Encapsulate(iGameCard.GetComponent<Collider2D>().bounds);
        }

        bounds.Expand(CameraBuffer);
        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * (DefaultCameraHeight / DefaultCameraWidth);
        var size = Mathf.Max(horizontal, vertical);
        var center = bounds.center + new Vector3(0, 0, -10);
        return (center, size);
    }
    public static GameSettings Instance
    {
        get
        {

            return _instance;
        }

    }




}
