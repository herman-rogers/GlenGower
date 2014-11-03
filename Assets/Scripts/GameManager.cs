using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public PuzzleGrid puzzleGrid;

    void Start( )
    {
        PuzzleGrid newGridInstance = Instantiate( puzzleGrid ) as PuzzleGrid;
        newGridInstance.GenerateGrid( );
    }
}