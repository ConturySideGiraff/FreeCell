using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private InputHandler _inputHandler;

    [Header("[ Card ]")]
    [SerializeField] private CardBehaviour _cardPrefab;
    [Space()]
    [SerializeField] private int _xCount = 4;
    [SerializeField] private int _yCount = 4;
    [Space()]
    [SerializeField] private float _xOffset = 3;
    [SerializeField] private float _yOffset = 4.5f;
    [SerializeField] private float _cOffset = 1.0f;

    [SerializeField, ReadOnly] private CardBehaviour _prevCard;
    [SerializeField, ReadOnly] private CardBehaviour _curCard;

    private CardBehaviour[] _cardArr;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        GridInit();
        GameInit();
    }

    private void Update()
    {
        if (_inputHandler.CurrentCard == null || (_inputHandler.CurrentCard != null && _inputHandler.CurrentCard.IsCorrect))
        {
            return;
        }

        if (_curCard != null)
        {
            _prevCard = _curCard;
            _curCard = _inputHandler.CurrentCard;
        }

        else
        {
            _curCard = _inputHandler.CurrentCard;
            _curCard.Flip(true);
        }

        if (_curCard != null && _prevCard != null)
        {
            bool isMatch = Match();

            if (isMatch)
            {
                _curCard.Flip(true);

                _curCard.Correct();
                _prevCard.Correct();

                bool isClear = IsClear();

                if (isClear)
                {
                    GameInit();
                }
            }

            else
            {
                _prevCard.Flip(false);
                _curCard.Flip(false);
            }

            _prevCard = null;
            _curCard = null;
        }
    }

    public void GridInit()
    {
        if (_cardArr != null)
        {
            foreach (CardBehaviour c in _cardArr)
            {
                Destroy(c.gameObject);
            }
        }

        _cardArr = new CardBehaviour[_xCount * _yCount];

        int idx;
        float xOffset = _xOffset + _cOffset;
        float yOffset = _yOffset + _cOffset;

        for (int y = 0; y < _yCount; y++)
        {
            for (int x = 0; x < _xCount; x++)
            {
                idx = y * _xCount + x;

                CardBehaviour card =CardBehaviour.Instantiate<CardBehaviour>(_cardPrefab, transform);
                card.transform.position = new Vector3(x * xOffset, y * yOffset) - new Vector3(_xOffset * _xCount * 0.5f + _xOffset, _yOffset * _yCount * 0.5f); ;
                card.gameObject.name = string.Format("Card_{0}", idx);

                _cardArr[idx] = card;
            }
        }
    }

    public void GameInit()
    {
        _curCard = null;
        _prevCard = null;

        int len = _xCount * _yCount;

        Sprite bgSp = Resources.Load<Sprite>("bg");
        List<Sprite> logoResourceList = Resources.LoadAll<Sprite>("Logo").ToList<Sprite>();
        
        List<Sprite> sourceSpList = new List<Sprite>();

        for (int i = 0; i < len / 2; i++)
        { 
            Pick(ref logoResourceList, out Sprite sourceSp);

            sourceSpList.Add(sourceSp);
            sourceSpList.Add(sourceSp);
        }

        for (int i = 0; i < len; i++)
        {
            Pick(ref sourceSpList, out Sprite flipSp);

            _cardArr[i].Init(bgSp, flipSp, i);
        }
    }

    private void Pick(ref List<Sprite> list, out Sprite sp)
    {
        int idx = Random.Range(0, list.Count);
        sp = list[idx];
        list.RemoveAt(idx);
    }

    private bool Match()
    {
        _prevCard.Info(out Sprite prevSp, out int prevIdx);
        _curCard.Info(out Sprite curSp, out int curIdx);

        return prevSp == curSp && prevIdx != curIdx;
    }

    private bool IsClear()
    {
        bool isClear = true;

        foreach (CardBehaviour c in _cardArr)
        {
            if (c.IsCorrect == false)
            {
                isClear = false;
                break;
            }
        }

        return isClear;
    }
}
