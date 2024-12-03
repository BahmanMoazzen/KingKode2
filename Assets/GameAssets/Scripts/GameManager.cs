using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<GameObject> _cardsGameObjects = new List<GameObject>();
    Camera _camera;
    GameStat _gameStat = GameStat.LoadingLevel;
    GameObject _selectedCard = null;
    [SerializeField] Text _infoText;
    private void Awake()
    {
        _camera = Camera.main;
    }

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
    private void CardController_OnCardUnSelected(GameObject iUnselectedCard)
    {
        _selectedCard = null;
    }

    private void CardController_OnCardSelected(GameObject iSelectedCard)
    {
        if (_selectedCard == null)
        {
            _selectedCard = iSelectedCard;
        }
        else
        {
            if (_selectedCard.GetComponent<CardController>()._CardName == iSelectedCard.GetComponent<CardController>()._CardName)
            {
                _gameStat = GameStat.Searching;
                StartCoroutine(_destroyCard(iSelectedCard));
            }
            else
            {
                _gameStat = GameStat.Searching;
                StartCoroutine(_showCard(iSelectedCard));
            }
        }
    }

    IEnumerator _destroyCard(GameObject iSecondCard)
    {
        yield return new WaitForSeconds(GameSettings.Instance.AnimationWaitTime);
        _cardsGameObjects.Remove(_selectedCard);
        _cardsGameObjects.Remove(iSecondCard);
        Destroy(_selectedCard);
        Destroy(iSecondCard);
        _selectedCard = null;
        _gameStat = GameStat.Waiting;
        _infoText.text = "Correct!";
    }
    IEnumerator _showCard(GameObject iSecondCard)
    {
        yield return new WaitForSeconds(GameSettings.Instance.AnimationWaitTime);
        _selectedCard.GetComponent<CardController>()._MakeIdle();
        iSecondCard.GetComponent<CardController>()._MakeIdle();
        _selectedCard = null;
        _gameStat = GameStat.Waiting;
        _infoText.text = "Wrong!";
    }



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



    GameObject _createCard(int iXindex, int iYIndex)
    {
        GameObject GO = new GameObject();
        GO.AddComponent<CardController>()._LoadCard(GameSettings.Instance.GetRandomCard());
        GO.transform.position = new Vector3(iXindex * GameSettings.Instance.XOffset
            , iYIndex * GameSettings.Instance.YOffset);

        return GO;
    }
    void _resizeCamera()
    {
        Vector3 oCamCenter = Vector3.zero;
        float oCamSize = 0;
        (oCamCenter, oCamSize) = GameSettings.Instance.CalculateCamSize(_cardsGameObjects.ToArray());
        _camera.transform.position = oCamCenter;
        _camera.orthographicSize = oCamSize;
    }

}

public enum GameStat { LoadingLevel, Waiting, Searching };
