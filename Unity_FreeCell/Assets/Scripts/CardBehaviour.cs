using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class CardBehaviour : MonoBehaviour
{
    public bool IsCorrect { get; private set; }

    private Sprite _flipSprite, _backSprite;
    private SpriteRenderer _render;
    private int _idx;

    public void Init(Sprite backSprite, Sprite flipSprite, int idx)
    {
        this._backSprite = backSprite;
        this._flipSprite = flipSprite;

        this._render = _render != null ? _render : GetComponent<SpriteRenderer>();
        this._render.sprite = _backSprite;

        this._idx = idx;

        this.IsCorrect = false;
    }

    public void Flip(bool isFlip)
    {
        this._render.sprite = isFlip ? _flipSprite : _backSprite;
    }

    public void Info(out Sprite flipSp, out int idx)
    {
        flipSp = _flipSprite;
        idx = _idx;
    }

    public void Correct()
    {
        this.IsCorrect = true;
    }
}
