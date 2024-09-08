using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private string _type; 

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _pickUpClip, _dropClip, _successClip;

    private bool _dragging = false;
    private bool _placed = false;
    private Vector2 _offset, originalPosition;
    private PuzzleSlot _slot;

    public string Type 
    {
        get { return _type; }
    }
    public void Init(PuzzleSlot slot)
    {
        _slot = slot;
    }

    void Awake()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {

        if (_placed) return;
        if (!_dragging) return;

        var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePosition - _offset;
    }

    void OnMouseDown()
    {
        _dragging = true;
        _source.PlayOneShot(_pickUpClip);

        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            _placed = true;
        }
        else
        {


            transform.position = originalPosition;
            _source.PlayOneShot(_dropClip);
            _dragging = false;


        }
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

