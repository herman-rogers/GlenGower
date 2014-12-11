using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public enum PuzzlePieceRotation
{
    RIGHT_POSITION,
    LEFT_POSITION,
    UP_POSITION,
    DOWN_POSITION
}

public class SecondPuzzlePiece : GamePuzzlePiece
{
    public FirstPuzzlePiece firstPuzzlePiecePair;
    public PuzzlePieceRotation pieceRotation = PuzzlePieceRotation.RIGHT_POSITION;

    public override void IntitializePuzzlePiece( )
    {
        GetComponent<TapGesture>( ).Tapped += TappedHandler;
        GetComponent<PanGesture>( ).PanStarted += StartDragCoroutine;
        GetComponent<PanGesture>( ).PanCompleted += StopDraggingPuzzlePiece;
    }

    public override void StartDragCoroutine( object sender, System.EventArgs e )
    {
        firstPuzzlePiecePair.StartDragCoroutine( sender, e );
    }

    public override void StopDraggingPuzzlePiece( object sender, System.EventArgs e )
    {
        firstPuzzlePiecePair.StopDraggingPuzzlePiece( sender, e );
    }

    public bool PuzzlePieceVertical( )
    {
        return ( pieceRotation == PuzzlePieceRotation.DOWN_POSITION ) ||
               ( pieceRotation == PuzzlePieceRotation.UP_POSITION );
    }

    public bool PuzzlePieceHorizontal( )
    {
        return ( pieceRotation == PuzzlePieceRotation.LEFT_POSITION ) ||
               ( pieceRotation == PuzzlePieceRotation.RIGHT_POSITION );
    }

    public GridTile DragSecondPuzzlePiece( float firstPiecePreviousPosition, float firstPieceNewPosition )
    {
        GridTile adjacentGridTile = null;
        if ( firstPuzzlePiecePair.puzzlePieceInactive )
        {
            return null;
        }
        if ( firstPiecePreviousPosition < firstPieceNewPosition )
        {
            adjacentGridTile = currentPuzzleGrid.SearchForAdjacentTileByDirection( transform.position,
                                                                                   PuzzleGridDirections.NEXT_COLUMN_RIGHT );
        }
        else if ( firstPiecePreviousPosition > firstPieceNewPosition )
        {
            adjacentGridTile = currentPuzzleGrid.SearchForAdjacentTileByDirection( transform.position,
                                                                                   PuzzleGridDirections.PREVIOUS_COLUMN_LEFT );
        }
        return adjacentGridTile;
    }

    public override void TappedHandler( object sender, System.EventArgs e )
    {
        if ( puzzlePieceInactive || firstPuzzlePiecePair.puzzlePieceInactive )
        {
            return;
        }
        if ( pieceRotation == PuzzlePieceRotation.RIGHT_POSITION )
        {
            RotatePuzzlePieceOnClick( 0, -1, PuzzlePieceRotation.DOWN_POSITION );
        }
        else if ( pieceRotation == PuzzlePieceRotation.DOWN_POSITION )
        {
            RotatePuzzlePieceOnClick( -1, 0, PuzzlePieceRotation.LEFT_POSITION );
        }
        else if ( pieceRotation == PuzzlePieceRotation.LEFT_POSITION )
        {
            RotatePuzzlePieceOnClick( 0, 1, PuzzlePieceRotation.UP_POSITION );
        }
        else if ( pieceRotation == PuzzlePieceRotation.UP_POSITION )
        {
            RotatePuzzlePieceOnClick( 1, 0, PuzzlePieceRotation.RIGHT_POSITION );
        }
    }

    protected override IEnumerator EndTileLife( )
    {
        moveTileDown = false;
        OccupyTileGrid( );
        RemoveTouchEvents( );
        SetTileRotationToFaceCamera( );
        yield return StartCoroutine( SetTilePositionToClosestGridTile( ) );
        currentGridTileLocation.SearchAdjacentTilesForMatches( );
    }

    private void RotatePuzzlePieceOnClick( int xRotation, int yRotation, PuzzlePieceRotation rotation )
    {
        Vector3 newPosition = new Vector3( firstPuzzlePiecePair.transform.position.x + xRotation,
                                         ( firstPuzzlePiecePair.transform.position.y + yRotation ),
                                           0 );
        GridTile newPositionGridTile = currentPuzzleGrid.SearchForClosestGridTileByCoordinates( newPosition.x, newPosition.y );
        //If Grid Tile is occupied or it is a boundary
        if ( newPositionGridTile.currentState == GridState.GRID_IS_OCCUPIED
             || newPositionGridTile.coordinates.x == transform.position.x  )
        {
            return;
        }
        pieceRotation = rotation;
        transform.position = newPosition;
    }
}