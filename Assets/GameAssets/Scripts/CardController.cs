using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Controlls the actions on cards
/// </summary>
public class CardController : MonoBehaviour
{
    /// <summary>
    /// Informations about the card
    /// </summary>
    CardInfo _cardInfo;

    /// <summary>
    /// Collider of the cards
    /// </summary>
    BoxCollider2D _collider;

    /// <summary>
    /// Renderer in witch cards showing fore and back
    /// </summary>
    SpriteRenderer _renderer;

    /// <summary>
    /// The current stat of the card
    /// </summary>
    CardStat _cardStat;

    /// <summary>
    /// Fires when card is in select mode
    /// </summary>
    public static event UnityAction<GameObject> OnCardSelected;
    /// <summary>
    /// Fires when card is unselected
    /// </summary>
    public static event UnityAction<GameObject> OnCardUnSelected;

    /// <summary>
    /// Loads the information of the card visually
    /// </summary>
    /// <param name="iCardInfo">The information to load</param>
    public void _LoadCard(CardInfo iCardInfo)
    {
        _cardInfo = iCardInfo;
        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.size = GameSettings.Instance.DefaultColliderSize;
        _renderer = gameObject.AddComponent<SpriteRenderer>();
        _changeStat(CardStat.Idle);
    }
    /// <summary>
    /// This fires from GameManager whenever clicks on the card
    /// </summary>
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
    /// <summary>
    /// Changing the stat of the card
    /// </summary>
    /// <param name="iNewStat">The new stat of the card</param>
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
    /// <summary>
    /// Forces the card back to idle stat
    /// </summary>
    public void _MakeIdle()
    {
        _changeStat(CardStat.Idle);

    }
    /// <summary>
    /// Returns the name of the card
    /// </summary>
    public string _CardName
    {
        get
        {
            return _cardInfo.CardName;
        }
    }
}
/// <summary>
/// all the stats of cards
/// </summary>
public enum CardStat { Idle, Selected };
