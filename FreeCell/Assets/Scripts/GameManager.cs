using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextureVersions
{ 
    Normal,
}

public class GameManager : MonoBehaviour
{
    [Header("[ Option ]")]
    [SerializeField] private TextureVersions _textureVersion;
    [SerializeField] private ushort _productVersion = 1;

    [Header("[ Scripts ]")]
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private TextureManager _textureManager;

    [SerializeField] private CardBehaviour _cardBehavour;

    private Dictionary<byte, Card> _freeCellDic;
    private Dictionary<byte, Card> _homeCellDic;
    private Dictionary<int, List<CardBehaviour>> _cardBehavourDic;

    private byte _correctCount;

    private IEnumerator Start()
    {
        // texture
        yield return StartCoroutine(_textureManager.CoDownload(_textureVersion, _productVersion));

        // dic
        _freeCellDic = new Dictionary<byte, Card>();
        _homeCellDic = new Dictionary<byte, Card>();
        _cardBehavourDic = new Dictionary<int, List<CardBehaviour>>();

        // count
        _correctCount = 0;

        // card
        _cardManager.Init(out Dictionary<int, List<Card>> cardDic);
        _cardBehavour.Init(cardDic[0][0]);

    }
}
