using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<PuzzleSlot> _slots;
    [SerializeField] private List<PuzzlePiece> _piecePrefabs;

    private Queue<PuzzlePiece> _piecesQueue = new Queue<PuzzlePiece>();
    private int _currentSpawnCount = 2;
    private int _placedPieces = 0;
    private HashSet<PuzzlePiece> _currentlySpawnedPieces = new HashSet<PuzzlePiece>();

    public static PuzzleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializePieceQueue();
        SpawnPieces(_currentSpawnCount);
    }

    public void GameOver()
    {
        Debug.Log("Game Over! All pieces have been placed.");
        CameraController.instance.endGame();
    }

    void InitializePieceQueue()
    {
        List<PuzzlePiece> allPieces = new List<PuzzlePiece>();

        foreach (var piecePrefab in _piecePrefabs)
        {
            for (int i = 0; i < 6; i++)
            {
                var piece = Instantiate(piecePrefab);
                piece.gameObject.SetActive(false);
                allPieces.Add(piece);
            }
        }

        allPieces = ShuffleList(allPieces);

        foreach (var piece in allPieces)
        {
            _piecesQueue.Enqueue(piece);
        }
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    void SpawnPieces(int count)
    {
        if (Score.instance == null)
        {
            Debug.LogError("Score.instance is null!");
        }

        if (CameraController.instance == null)
        {
            Debug.LogError("CameraController.instance is null!");
        }

        if (Score.instance.GetScore() >= 26)
        {
            Debug.Log("Game Over! All pieces placed.");
            CameraController.instance.endGame();
            return;
        }

        HashSet<PuzzlePiece> spawnedThisRound = new HashSet<PuzzlePiece>();

        for (int i = 0; i < count; i++)
        {
            if (_piecesQueue.Count > 0)
            {
                PuzzlePiece piece = null;
                do
                {
                    piece = _piecesQueue.Dequeue();
                    if (_currentlySpawnedPieces.Contains(piece) || spawnedThisRound.Contains(piece))
                    {
                        _piecesQueue.Enqueue(piece);
                        piece = null;
                    }
                }
                while (piece == null && _piecesQueue.Count > 0);

                if (piece != null)
                {
                    piece.gameObject.SetActive(true);
                    _currentlySpawnedPieces.Add(piece);
                    spawnedThisRound.Add(piece);

                    foreach (var slot in _slots)
                    {
                        if (piece.Type == slot.Type)
                        {
                            piece.Init(slot);
                            break;
                        }
                    }
                }

                    else
    {
        Debug.LogError("Null piece found!");
    }
            }
        }
    }

    public void PiecePlaced(PuzzlePiece piece)
    {
        _placedPieces++;
        _currentlySpawnedPieces.Remove(piece);

        if (_placedPieces == _currentSpawnCount)
        {
            _placedPieces = 0;
            if (_currentSpawnCount == 2)
            {
                _currentSpawnCount = 4;
            }
            else if (_currentSpawnCount == 4)
            {
                _currentSpawnCount = 8;
            }
            else if (_currentSpawnCount == 8)
            {
                _currentSpawnCount = 10;
            }

            SpawnPieces(_currentSpawnCount);
        }
    }
}
