using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleSpawner : MonoBehaviour
{
    public GamePuzzlePiece gamePiece;
    private Vector3 firstSpawnPosition;
    private Vector3 secondSpawnPosition;
    private PuzzleGrid puzzleGrid;

    public void StartPuzzleSpawing(  )
    {
        puzzleGrid = GetComponent<PuzzleGrid>( );
        firstSpawnPosition = puzzleGrid.gridTiles[ 2, 9 ].transform.localPosition;
        secondSpawnPosition = puzzleGrid.gridTiles[ 3, 9 ].transform.localPosition;
        StartCoroutine( StartTilePlacement( ) );
    }

    private IEnumerator StartTilePlacement( )
    {
        while ( true )
        {
            GamePuzzlePiece firstPuzzlePiece = CreateNewPuzzlePiece( );
            GamePuzzlePiece secondPuzzlePiece = CreateNewPuzzlePiece( );
            firstPuzzlePiece.transform.position = firstSpawnPosition;
            secondPuzzlePiece.transform.position = secondSpawnPosition;
            firstPuzzlePiece.MoveTilePiece( puzzleGrid.lowestGridPosition );
            secondPuzzlePiece.MoveTilePiece( puzzleGrid.lowestGridPosition );
            yield return new WaitForSeconds( 1.0f );
        }
    }

    private GamePuzzlePiece CreateNewPuzzlePiece( )
    {
        GamePuzzlePiece newPieceInstance = Instantiate( gamePiece ) as GamePuzzlePiece;
        newPieceInstance.transform.parent = puzzleGrid.transform;
        return newPieceInstance;
    }
}