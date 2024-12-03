using UnityEngine;
/// <summary>
/// Uses to store game information and performs general functionality
/// </summary>
[CreateAssetMenu(fileName = "NewGameSettings", menuName = "BAHMAN/Task2/New Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    /// <summary>
    /// The pointer to the class as singletone
    /// </summary>
    private static GameSettings _instance;
    /// <summary>
    /// Default Camera Size
    /// </summary>
    public int DefaultCameraWidth = 480;
    public int DefaultCameraHeight = 800;

    /// <summary>
    /// Game Grid Size
    /// </summary>
    [Range(2, 6)]
    public int XDimension = 2;
    [Range(3, 6)]
    public int YDimension = 3;

    /// <summary>
    /// Deck Variation on game start
    /// </summary>
    [Range(2, 4)]
    public int DeckVariation = 2;
    /// <summary>
    /// card distances from each other
    /// </summary>
    [Range(0, 2)]
    public float XOffset = .5f;
    [Range(0, 2)]
    public float YOffset = .5f;
    /// <summary>
    /// camera margin when fitted
    /// </summary>
    [Range(0, 2)]
    public float CameraBuffer = 1f;

    /// <summary>
    /// wait time before clearing the cards on match
    /// </summary>
    [Range(0, 2)]
    public float AnimationWaitTime = .8f;

    /// <summary>
    /// All of the game cards
    /// </summary>
    public CardInfo[] AllCards;
    /// <summary>
    /// The default collider size of the cards
    /// </summary>
    public Vector2 DefaultColliderSize;
    /// <summary>
    /// The back of the cards
    /// </summary>
    public Sprite CardBackSprite;

    /// <summary>
    /// Loads the setting into singletone from resources folder
    /// </summary>
    public static void Initialize()
    {
        if (_instance == null)
        {
            _instance = Resources.Load<GameSettings>("GameSettings");
        }
    }

    /// <summary>
    /// Retuns a random CardInfo from all cards regarding variation
    /// </summary>
    /// <returns>The card info to show</returns>
    public CardInfo GetRandomCard()
    {
        return AllCards[Random.Range(0, DeckVariation)];
    }

    /// <summary>
    /// Calculating the size and center of the camera
    /// </summary>
    /// <param name="iGameCards">All of the cards of the game</param>
    /// <returns>The center and the size of the camera</returns>
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

    /// <summary>
    /// returning the instance of the GameSettings
    /// </summary>
    public static GameSettings Instance
    {
        get
        {

            return _instance;
        }

    }




}
