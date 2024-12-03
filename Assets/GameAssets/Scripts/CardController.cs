using UnityEngine;
using UnityEngine.Events;
public class CardController : MonoBehaviour
{
    CardInfo _cardInfo;
    BoxCollider2D _collider;
    SpriteRenderer _renderer;
    CardStat _cardStat;
    public static event UnityAction<GameObject> OnCardSelected;
    public static event UnityAction<GameObject> OnCardUnSelected;
    public void _LoadCard(CardInfo iCardInfo)
    {
        _cardInfo = iCardInfo;
        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.size = GameSettings.Instance.DefaultColliderSize;
        _renderer = gameObject.AddComponent<SpriteRenderer>();
        //_renderer.sprite = GameSettings.Instance.CardBackSprite;

        _changeStat(CardStat.Idle);
    }

    public void _TakeClick()
    {
        Debug.Log("Clicked on:" + _cardInfo.CardName);
        if (_cardStat == CardStat.Idle)
        {
            _changeStat(CardStat.Selected);
            OnCardSelected?.Invoke(gameObject);

        }
        else
        {
            _changeStat(CardStat.Idle);
            OnCardUnSelected?.Invoke(gameObject);
        }
    }
    void _changeStat(CardStat iNewStat)
    {


        // do start stat action
        switch (iNewStat)
        {
            case CardStat.Selected:
                
                _renderer.sprite = _cardInfo.CardFaceSprite;
                break;
            case CardStat.Idle:
                
                _renderer.sprite = GameSettings.Instance.CardBackSprite;
                break;
        }
        _cardStat = iNewStat;
    }
    public void _MakeIdle()
    {
        _changeStat(CardStat.Idle);

    }
    public CardStat _CardStat
    {
        get { return _cardStat; }

    }
    public string _CardName
    {
        get
        {
            return _cardInfo.CardName;
        }
    }
}

public enum CardStat { Idle, Selected };
