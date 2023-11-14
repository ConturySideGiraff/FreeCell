using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    [Header("[Debug View]")]
    [SerializeField] private byte _num;
    [SerializeField] private Symbols _symbol;
    [SerializeField] private bool _isMove = false;

    private Card _card;

    public void Init(Card card)
    {
        _card = card;
        _num = 0;

        switch (_num)
        {
            case 0: Visual_0(); break;
        }
    }

    private void Visual_0()
    {

    }
}
