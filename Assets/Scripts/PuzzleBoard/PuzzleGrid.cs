using UnityEngine;
using System.Collections;

public struct GridBoundaries
{
    public static float upperXBoundary;
    public static float lowerXBoundary;
    public static float lowerYBoundary;
}

public class PuzzleGrid : MonoBehaviour
{
    public GridTile puzzleGridPiece;
    [HideInInspector]
    public GridTile[ , ] gridTiles = new GridTile[ gridSizeX, gridSizeY ];
    [HideInInspector]
    public float lowestGridPosition;
    private const int gridSizeX = 6;
    private const int gridSizeY = 10;

    public void GenerateGrid( )
    {
        for ( int i = 0; i < gridSizeX; i++ )
        {
            for ( int y = 0; y < gridSizeY; y++ )
            {
                CreateTile( i, y );
            }
        }
        SetGridBoundaries( );
    }

    private void CreateTile( int xPosition, int yPosition )
    {
        GridTile newTile = Instantiate( puzzleGridPiece ) as GridTile;
        gridTiles[ xPosition, yPosition ] = newTile;
        newTile.name = "Tile Column: " + xPosition + "Row: " + yPosition;
        newTile.tileXGridPosition = xPosition;
        newTile.tileYGridPosition = yPosition;
        newTile.transform.parent = transform;
        newTile.transform.localPosition = new Vector3( xPosition - gridSizeX * 0.5f + 0.5f,
                                                       yPosition - gridSizeY * 0.5f + 0.5f,
                                                       0 );
    }

    private void SetGridBoundaries( )
    {
        GridBoundaries.lowerYBoundary = gridTiles[ 0, 0 ].transform.position.y;
        GridBoundaries.lowerXBoundary = gridTiles[ 0, 0 ].transform.position.x;
        GridBoundaries.upperXBoundary = gridTiles[ ( gridSizeX - 1 ), 0 ].transform.position.x;
    }
}
