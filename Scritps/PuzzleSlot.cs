using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{

    [SerializeField] private string _type; // Add this property for the slot type (e.g., paper, plastic)

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _completeClip;
    [SerializeField] private SpriteRenderer _renderer;

    public string Type // Expose the type via a public property
    {
        get { return _type; }
    }
    public void Placed()
    {
        _source.PlayOneShot(_completeClip);
    }

    public SpriteRenderer Renderer
    {
        get { return _renderer; }
    }
}
