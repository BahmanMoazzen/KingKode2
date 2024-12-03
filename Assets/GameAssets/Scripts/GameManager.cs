using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
/// <summary>
/// General management of the game is done by this class
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// All the cards in game
    /// </summary>
    List<GameObject> _cardsGameObjects = new List<GameObject>();
    /// <summary>
    /// the main camera reference
    /// </summary>
    Camera _camera;
    /// <summary>
    /// Stat of the game
    /// </summary>
    GameStat _gameStat = GameStat.LoadingLevel;
    /// <summary>
    /// Selected card to compare to
    /// </summary>
    GameObject _selectedCard = null;
    /// <summary>
    /// The UI information on screen
    /// </summary>
    [SerializeField] Text _infoText;
    private void Awake()
    {
        _camera = Camera.main;
    }
    /// <summary>
    /// Loading cards based on GameSettings of the game
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {

        GameSettings.Initialize();

        for (int i = 0; i < GameSettings.Instance.XDimension; i++)
        {
            for (int j = 0; j < GameSettings.Instance.YDimension; j++)
            {
                yield return 0;
                _cardsGameObjects.Add(_createCard(i, j));

            }
        }
        _gameStat = GameStat.Waiting;
        _resizeCamera();
        

    }
    private void OnEnable()
    {
        CardController.OnCardSelected += CardController_OnCardSelected;
        CardController.OnCardUnSelected += CardController_OnCardUnSelected;
    }
    private void OnDisable()
    {
        CardController.OnCardSelected -= CardController_OnCardSelected;
        CardController.OnCardUnSelected -= CardController_OnCardUnSelected;
    }
    /// <summary>
    /// Fires when any card unselected
    /// </summary>
    /// <param name="iUnselectedCard">The card GameObject</param>
    private void CardController_OnCardUnSelected(GameObject iUnselectedCard)
    {
        _selectedCard = null;
    }
    /// <summary>
    /// Fires when any card selected
    /// </summary>
    /// <param name="iSelectedCard">The card GameObject</param>
    private void CardController_OnCardSelected(GameObject iSelectedCard)
    {
        if (_selectedCard == null)
        {
            _selectedCard = iSelectedCard;
        }
        else
        {
            // Check if the cards are similar
            if (_selectedCard.GetComponent<CardController>()._CardName == iSelectedCard.GetComponent<CardController>()._CardName)
            {
                // Cards are similar
                _gameStat = GameStat.Searching;
                StartCoroutine(_destroyCard(iSelectedCard));
            }
            else
            {
                // Cards are different
                _gameStat = GameStat.Searching;
                StartCoroutine(_showCard(iSelectedCard));
            }
        }
    }
    /// <summary>
    /// Destroys the cards
    /// </summary>
    /// <param name="iSecondCard">The reference to the last card clicked</param>
    /// <returns>Nothing</returns>
    IEnumerator _destroyCard(GameObject iSecondCard)
    {
        _infoText.text = "Correct!";
        yield return new WaitForSeconds(GameSettings.Instance.AnimationWaitTime);
        _cardsGameObjects.Remove(_selectedCard);
        _cardsGameObjects.Remove(iSecondCard);
        Destroy(_selectedCard);
        Destroy(iSecondCard);
        _selectedCard = null;
        _gameStat = GameStat.Waiting;
        _infoText.text = "Click to continue!";
    }
    /// <summary>
    /// Showing the second card properly
    /// </summary>
    /// <param name="iSecondCard">The second card</param>
    /// <returns>Nothing</returns>
    IEnumerator _showCard(GameObject iSecondCard)
    {
        _infoText.text = "Wrong!";
        yield return new WaitForSeconds(GameSettings.Instance.AnimationWaitTime);
        _selectedCard.GetComponent<CardController>()._MakeIdle();
        iSecondCard.GetComponent<CardController>()._MakeIdle();
        _selectedCard = null;
        _gameStat = GameStat.Waiting;
        
        _infoText.text = "Click to continue!";
    }


    /// <summary>
    /// Fires when player clicks or taps the screen
    /// </summary>
    /// <param name="iContext"></param>
    public void _Clicked(InputAction.CallbackContext iContext)
    {
        if (iContext.performed && _gameStat == GameStat.Waiting)
        {
            foreach (GameObject GO in _cardsGameObjects)
            {
                var col = GO.GetComponent<Collider2D>();
                if (col != null)
                {
                    if (col.OverlapPoint(_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue())))
                    {
                        GO.GetComponent<CardController>()._TakeClick();
                        return;
                    }
                }
            }
        }
    }


    /// <summary>
    /// The assembly of the cards. no prefab included
    /// </summary>
    /// <param name="iXindex">X place on gride</param>
    /// <param name="iYIndex">U place on gride</param>
    /// <returns>A constructed card</returns>
    GameObject _createCard(int iXindex, int iYIndex)
    {
        GameObject GO = new GameObject();
        GO.AddComponent<CardController>()._LoadCard(GameSettings.Instance.GetRandomCard());
        GO.transform.position = new Vector3(iXindex * GameSettings.Instance.XOffset
            , iYIndex * GameSettings.Instance.YOffset);

        return GO;
    }
    /// <summary>
    /// Resizes the camera to fit all cards
    /// </summary>
    void _resizeCamera()
    {
        Vector3 oCamCenter = Vector3.zero;
        float oCamSize = 0;
        (oCamCenter, oCamSize) = GameSettings.Instance.CalculateCamSize(_cardsGameObjects.ToArray());
        _camera.transform.position = oCamCenter;
        _camera.orthographicSize = oCamSize;
    }

}
/// <summary>
/// All stats of the game
/// </summary>
public enum GameStat { LoadingLevel, Waiting, Searching };
