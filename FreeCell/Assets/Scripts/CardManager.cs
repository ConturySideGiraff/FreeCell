using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum Symbols
{
    Heart   = 0,
    Diamond = 1,
    Spade   = 2,
    Clover  = 3,
}

public class CardManager : MonoBehaviour
{
    public void Init(out Dictionary<int, List<Card>> cardDic)
    {
        // ¼ÅÇÃ
        byte[] cardArr = new byte[52];
        for (byte i = 0; i < 52; i++)
            cardArr[i] = i;

        List<byte> cardList = cardArr.ToList<byte>();

        int randomPick;
        for (byte i = 0; i < 52; i++)
        {
            randomPick = Random.Range(0, cardList.Count);
            cardArr[i] = cardList[randomPick];
            cardList.RemoveAt(randomPick);
        }

        // Ä«µåÈ­
        cardDic = new Dictionary<int, List<Card>>();

        cardDic.Add(0, ByteListToCardList(cardArr[00..07]));
        cardDic.Add(1, ByteListToCardList(cardArr[07..14]));
        cardDic.Add(2, ByteListToCardList(cardArr[14..21]));
        cardDic.Add(3, ByteListToCardList(cardArr[21..28]));

        cardDic.Add(4, ByteListToCardList(cardArr[28..34]));
        cardDic.Add(5, ByteListToCardList(cardArr[34..40]));
        cardDic.Add(6, ByteListToCardList(cardArr[40..46]));
        cardDic.Add(7, ByteListToCardList(cardArr[46..52]));
    }

    private List<Card> ByteListToCardList(byte[] bArr)
    {
        List<Card> cList = new List<Card>();
        foreach (byte b in bArr)
        { 
            cList.Add(new Card(b));
        }

        return cList;
    }
}

[Serializable]
public class Card
{
    public byte NumIdx { get; private set; }
    public string Num { get; private set; }

    public byte SymbolIdx { get; private set; }
    public Symbols Symbol { get; private set; }

    public int Group { get; protected set; }
    public Sprite Sp { get; private set; }

    public Card(byte idx)
    {
        this.NumIdx = (byte)(idx % 13);
        this.Num = GetName(this.NumIdx);

        this.SymbolIdx = (byte)(idx / 13);
        this.Symbol = GetSymbol(this.SymbolIdx);

        this.Group = SymbolIdx > 1 ? 1 : 0;
        this.Sp = TextureManager.SymbolSpriteDic[this.Symbol];
    }

    public string GetName(byte index) => index switch
    {
        0 => "A",
        1 => "2",
        2 => "3",
        3 => "4",
        4 => "5",

        5 => "6",
        6 => "7",
        7 => "8",
        8 => "9",
        9 => "10",

        10 => "J",
        11 => "Q",
        12 => "K",

        _ => default,
    };

    public Symbols GetSymbol(byte index) => index switch
    {
        0 => Symbols.Heart,
        1 => Symbols.Spade,
        2 => Symbols.Clover,
        3 => Symbols.Diamond,

        _ => default,
    };
}
