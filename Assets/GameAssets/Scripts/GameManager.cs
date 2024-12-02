using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    IEnumerator Start()
    {
        GameSettings.Initialize();

        for (int i = 0; i < GameSettings.Instance.XDimension; i++)
        {
            for (int j = 0; j < GameSettings.Instance.YDimension; j++)
            {
                yield return 0;
                CreateCard(i, j);

            }
        }
    }
    

    public void CreateCard(int iXindex, int iYIndex)
    {
        GameObject GO = new GameObject();
        GO.AddComponent<CardController>()._LoadCard(GameSettings.Instance.GetRandomCard());
        GO.transform.position = new Vector3(iXindex * GameSettings.Instance.XOffset
            , iYIndex * GameSettings.Instance.YOffset);
        

    }

}
