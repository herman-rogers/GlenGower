using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GridState
{ 
    GRID_IS_EMPTY,
    GRID_IS_OCCUPIED,
    GRID_IS_DESTROYED
}

public class GridTile : MonoBehaviour
{
    public GridState currentState;
    public Vector2 coordinates;
    public GamePuzzlePiece puzzlePieceOnTile;
    public PuzzleGrid currentPuzzleGrid;
    [HideInInspector]
    public int tileColumn;
    [HideInInspector]
    public int tileRow;

    private void Start( )
    {
        currentState = GridState.GRID_IS_EMPTY;
        coordinates = new Vector2( gameObject.transform.position.x,
                                   gameObject.transform.position.y );
    }

    public void SearchAdjacentTilesForMatches( )
    {
        List<GamePuzzlePiece> tileMatches = new List<GamePuzzlePiece>( );
        tileMatches.Add( puzzlePieceOnTile );
        for ( int x = 0; x < PuzzleGrid.GRID_SIZE_X; x++ )
        {
            for ( int y = 0; y < PuzzleGrid.GRID_SIZE_Y; y++ )
            {
                GridTile searchTile = currentPuzzleGrid.gridTiles[ x, y ];
                if ( searchTile.currentState != GridState.GRID_IS_OCCUPIED )
                {
                    continue;
                }
                if ( puzzlePieceOnTile.tag != searchTile.puzzlePieceOnTile.tag )
                {
                    continue;
                }
                if ( x == tileColumn && ( y == ( tileRow + 1 ) || y == ( tileRow - 1 ) ) )
                {
                    tileMatches.Add( searchTile.puzzlePieceOnTile );
                }
                else if( tileRow == y && ( x == ( tileColumn + 1 ) || x == ( tileColumn - 1 ) ) )
                {
                    tileMatches.Add( searchTile.puzzlePieceOnTile );
                }
            }
        }
        if ( tileMatches.Count < 2 )
        {
            return;
        }
        foreach ( GamePuzzlePiece tile in tileMatches )
        {
            tile.DestoryPuzzlePiece( );
        }
    }
}