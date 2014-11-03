using UnityEngine;
using System.Collections;

public class PuzzleGrid : MonoBehaviour
{
    public GridTile puzzleGridPiece;
    private GridTile[ , ] gridTiles = new GridTile[ gridSizeX, gridSizeY ];
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
    }

    private void CreateTile( int xPosition, int yPosition )
    {
        GridTile newTile = Instantiate( puzzleGridPiece ) as GridTile;
        gridTiles[ xPosition, yPosition ] = newTile;
        newTile.name = "Tile Column: " + xPosition + "Row: " + yPosition;
        newTile.transform.parent = transform;
        newTile.transform.localPosition = new Vector3( xPosition - gridSizeX * 0.5f + 0.5f,
                                                       yPosition - gridSizeY * 0.5f + 0.5f,
                                                       0 );
    }
}
