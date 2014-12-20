using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int numberOfTilesToMatch = 3;
    [HideInInspector]
    public List<GamePuzzlePiece> piecesOnBoard = new List<GamePuzzlePiece>( );
    public PuzzleGrid puzzleGrid;
    private List<GamePuzzlePiece> totalMatches;
    private PuzzleGrid newGridInstance;
    private PuzzleSpawner puzzleSpawn;

    private void Start( )
    {
        newGridInstance = Instantiate( puzzleGrid ) as PuzzleGrid;
        puzzleSpawn = newGridInstance.GetComponent<PuzzleSpawner>( );
        newGridInstance.GenerateGrid( );
        puzzleSpawn.StartPuzzleSpawning( );
        StartCoroutine( CheckEachTileForMatches( ) );
    }

    private IEnumerator CheckEachTileForMatches( )
    {
        while ( true )
        {
            if ( BoardContainsActivePieces( ) )
            {
                yield return new WaitForFixedUpdate( );
            }
            else
            {
                FindPuzzlePieceMatches( );
                yield return StartCoroutine( DestroyMatchingPuzzlePieces( ) );
            }
        }
    }

    private void FindPuzzlePieceMatches( )
    {
        totalMatches = new List<GamePuzzlePiece>( );
        for ( int x = 0; x < PuzzleGrid.GRID_SIZE_X; x++ )
        {
            for ( int y = 0; y < PuzzleGrid.GRID_SIZE_Y; y++ )
            {
                GridTile tile = newGridInstance.gridTiles[ x, y ];
                List<GamePuzzlePiece> tileMatches = tile.SearchAdjacentTilesForMatches( );
                if ( tileMatches.Count < numberOfTilesToMatch )
                {
                    continue;
                }
                foreach ( GamePuzzlePiece match in tileMatches )
                {
                    if ( !totalMatches.Contains( match ) )
                    {
                        totalMatches.Add( match );
                    }
                }
            }
        }
    }

    private IEnumerator DestroyMatchingPuzzlePieces( )
    {
        foreach ( GamePuzzlePiece tileMatch in totalMatches )
        {
            StartCoroutine( tileMatch.DestoryPuzzlePiece( ) );
        }
        GarbageCollectOldPieces( );
        yield return new WaitForFixedUpdate( );
        if ( BoardContainsActivePieces( ) )
        {
            CheckEachTileForMatches( );
        }
        else
        {
            Subject.Notify( PuzzleSpawner.CREATE_NEW_PIECE );
        }
    }

    private bool BoardContainsActivePieces( )
    {
        return piecesOnBoard.Exists( item => item.puzzlePieceInactive == false );
    }

    private void GarbageCollectOldPieces( )
    {
        piecesOnBoard.RemoveAll( item => item == null );
    }
}