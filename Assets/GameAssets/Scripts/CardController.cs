using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class CardController : MonoBehaviour
{
    CardInfo _cardInfo;
    BoxCollider2D _collider;
    SpriteRenderer _renderer;
    public void _LoadCard(CardInfo iCardInfo)
    {
        _cardInfo = iCardInfo;
        _collider =  gameObject.AddComponent<BoxCollider2D>();
        _collider.size = GameSettings.Instance.DefaultColliderSize;
        _renderer = gameObject.AddComponent<SpriteRenderer>();
        //_renderer.sprite = GameSettings.Instance.CardBackSprite;
        _renderer.sprite = _cardInfo.CardFaceSprite;
    }
    



}
