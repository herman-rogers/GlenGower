using UnityEngine;
using System;
using System.Collections;

public struct GridBoundaries
{
    public static float upperXBoundary;
    public static float lowerXBoundary;
    public static float lowerYBoundary;
}

public enum PuzzleGridDirections
{
    NEXT_COLUMN_RIGHT,
    PREVIOUS_COLUMN_LEFT,
    NEXT_ROW_UP,
    PREVIOUS_ROW_DOWN,
}

public class PuzzleGrid : MonoBehaviour
{
    public GridTile puzzleGridPiece;
    [HideInInspector]
    public GridTile[ , ] gridTiles = new GridTile[ GRID_SIZE_X, GRID_SIZE_Y ];
    [HideInInspector]
    public float lowestGridPosition;
    public const int GRID_SIZE_X = 6;
    public const int GRID_SIZE_Y = 10;

    public void GenerateGrid( )
    {
        for ( int x = 0; x < GRID_SIZE_X; x++ )
        {
            for ( int y = 0; y < GRID_SIZE_Y; y++ )
            {
                CreateTile( x, y );
            }
        }
        SetGridBoundaries( );
    }

    public GridTile SearchForClosestGridTileByCoordinates( float xCoordinate, float yCoordinate )
    {
        float distance = 1000;
        GridTile closestTile = null;
        Vector3 currentPosition = new Vector3( xCoordinate, yCoordinate, 0 );
        for ( int x = 0; x < GRID_SIZE_X; x++ )
        {
            for ( int y = 0; y < GRID_SIZE_Y; y++ )
            {
                GridTile tile = gridTiles[ x, y ];
                float gridTileDistance = Vector3.Distance( tile.coordinates, currentPosition );
                if ( distance > gridTileDistance )
                {
                    closestTile = tile;
                    distance = gridTileDistance;
                }
            }
        }
        return closestTile;
    }

    public GridTile SearchForAdjacentTileByDirection( Vector2 tileCoordinates, PuzzleGridDirections direction )
    {
        GridTile currentTile = SearchForClosestGridTileByCoordinates( tileCoordinates.x, tileCoordinates.y );
        GridTile adjacentTile = null;
        switch ( direction )
        {
            case PuzzleGridDirections.NEXT_COLUMN_RIGHT:
                adjacentTile = GetTileFromGrid( ( currentTile.tileColumn + 1 ), currentTile.tileRow );
                break;
            case PuzzleGridDirections.PREVIOUS_COLUMN_LEFT:
                adjacentTile = GetTileFromGrid( ( currentTile.tileColumn - 1 ), currentTile.tileRow );
                break;
            case PuzzleGridDirections.NEXT_ROW_UP:
                adjacentTile = GetTileFromGrid( currentTile.tileColumn, ( currentTile.tileRow + 1 ) );
                break;
            case PuzzleGridDirections.PREVIOUS_ROW_DOWN:
                adjacentTile = GetTileFromGrid( currentTile.tileColumn, ( currentTile.tileRow - 1 ) );
                break;
        }
        return adjacentTile;
    }

    public bool IsNextCellOccupied( GridTile tile )
    {
        if ( PuzzlePieceIsAtLowerGridBoundary( tile ) )
        {
            return true;
        }
        if ( gridTiles[ tile.tileColumn, ( tile.tileRow - 1 ) ].currentState == GridState.GRID_IS_OCCUPIED )
        {
            return true;
        }
        return false;
    }

    private void CreateTile( int xColumnPosition, int yRowPosition )
    {
        GridTile newTile = Instantiate( puzzleGridPiece ) as GridTile;
        gridTiles[ xColumnPosition, yRowPosition ] = newTile;
        newTile.name = "Tile Column: " + xColumnPosition + "Row: " + yRowPosition;
        newTile.tileColumn = xColumnPosition;
        newTile.tileRow = yRowPosition;
        newTile.currentPuzzleGrid = this;
        newTile.transform.parent = transform;
        newTile.transform.localPosition = new Vector3( xColumnPosition - GRID_SIZE_X * 0.5f + 0.5f,
                                                       yRowPosition - GRID_SIZE_Y * 0.5f + 0.5f,
                                                       0 );
    }

    private void SetGridBoundaries( )
    {
        GridBoundaries.lowerYBoundary = gridTiles[ 0, 0 ].transform.position.y;
        GridBoundaries.lowerXBoundary = gridTiles[ 0, 0 ].transform.position.x;
        GridBoundaries.upperXBoundary = gridTiles[ ( GRID_SIZE_X - 1 ), 0 ].transform.position.x;
    }

    private GridTile GetTileFromGrid( int columnCoordinate, int rowCoordinate )
    {
        GridTile adjacentTile = null;
        try
        {
            adjacentTile = gridTiles[ columnCoordinate, rowCoordinate ];
        }
        catch ( IndexOutOfRangeException e )
        {
        }
        return adjacentTile;
    }

    private bool PuzzlePieceIsAtLowerGridBoundary( GridTile tile )
    {
        if ( ( tile.tileRow - 1 ) < 0 )
        {
            return true;
        }
        return false;
    }
}
