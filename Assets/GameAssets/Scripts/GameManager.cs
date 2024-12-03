using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> _cardsGameObjects = new List<GameObject>();
    Camera _camera;

    IEnumerator Start()
    {

        GameSettings.Initialize();

        for (int i = 0; i < GameSettings.Instance.XDimension; i++)
        {
            for (int j = 0; j < GameSettings.Instance.YDimension; j++)
            {
                yield return 0;
                _cardsGameObjects.Add(CreateCard(i, j));
                _resizeCamera();
            }
        }


    }
    private void Awake()
    {
        _camera = Camera.main;
    }


    GameObject CreateCard(int iXindex, int iYIndex)
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
