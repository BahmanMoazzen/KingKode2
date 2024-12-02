using UnityEngine;
using UnityEngine.InputSystem;
public class CardController : MonoBehaviour
{
    Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext iContext)
    {
        if (!iContext.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (rayHit)
        {
            Debug.Log("Test");
        }
        else
        {


        }

    }
    

    
}
