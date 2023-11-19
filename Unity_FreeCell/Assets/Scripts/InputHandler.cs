using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CardBehaviour CurrentCard { get; private set; }

    private bool _isInput;

    private Vector2 _screenPoint;


    private void Update()
    {
        CurrentCard = null;

        _isInput = false;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _screenPoint = Input.mousePosition;
            _isInput = true;
        }

#elif UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            //_screenPoint = Input.GetTouch(0).position;
            //_isInput = true;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                _screenPoint = touch.position;
                _isInput = true;
            }
        }
#endif

        if (!_isInput)
        {
            return;
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(_screenPoint);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector3.forward, Mathf.Infinity);

        if (hit.collider == null)
        {
            return;
        }

        CurrentCard = hit.collider.gameObject.GetComponent<CardBehaviour>();
    }
}
