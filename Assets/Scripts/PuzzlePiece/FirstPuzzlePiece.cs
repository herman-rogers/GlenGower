using UnityEngine;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public class FirstPuzzlePiece : GamePuzzlePiece
{
    public SecondPuzzlePiece secondPuzzlePiecePair;

    public override void TappedHandler( object sender, System.EventArgs e )
    {
        secondPuzzlePiecePair.TappedHandler( this, new System.EventArgs( ) );
    }

    public override void StartDragCoroutine( object sender, System.EventArgs e )
    {
        StartCoroutine( dragPuzzlePiece );
    }

    public override void StopDraggingPuzzlePiece( object sender, System.EventArgs e )
    {
        StopCoroutine( dragPuzzlePiece );
    }

    private IEnumerator DragFirstPuzzlePiece( )
    {
        while ( true )
        {
            if ( puzzlePieceInactive || secondPuzzlePiecePair.puzzlePieceInactive )
            {
                yield return null;
            }
            Vector3 dragTouchPosition = mainCamera.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, 0 ) );
            GridTile dragFirstPieceToTile = currentPuzzleGrid.SearchForClosestGridTileByCoordinates( dragTouchPosition.x, dragTouchPosition.y );
            GridTile dragSecondPieceToTile = secondPuzzlePiecePair.DragSecondPuzzlePiece( transform.position.x, dragFirstPieceToTile.coordinates.x );
            if ( dragFirstPieceToTile.currentState != GridState.GRID_IS_OCCUPIED &&
                 dragSecondPieceToTile != null &&
                 ( secondPuzzlePiecePair.PuzzlePieceVertical( ) || 
                   !DoesPuzzlePieceOccupySameColumn( dragFirstPieceToTile, dragSecondPieceToTile ) ) )
            {
                SetPuzzlePieceCoordinates( dragFirstPieceToTile.coordinates.x, transform.position.y );
                secondPuzzlePiecePair.SetPuzzlePieceCoordinates( dragSecondPieceToTile.coordinates.x, secondPuzzlePiecePair.transform.position.y );
            }
            yield return new WaitForSeconds( 0.009f );
        }
    }

    public override void IntitializePuzzlePiece( )
    {
        dragPuzzlePiece = DragFirstPuzzlePiece( );
        GetComponent<TapGesture>( ).Tapped += TappedHandler;
        GetComponent<PanGesture>( ).PanStarted += StartDragCoroutine;
        GetComponent<PanGesture>( ).PanCompleted += StopDraggingPuzzlePiece;
    }

    protected override IEnumerator EndTileLife( )
    {
        moveTileDown = false;
        StopCoroutine( dragPuzzlePiece );
        OccupyTileGrid( );
        RemoveTouchEvents( );
        SetTileRotationToFaceCamera( );
        yield return StartCoroutine( SetTilePositionToClosestGridTile( ) );
        currentGridTileLocation.SearchAdjacentTilesForMatches( );
        StartCoroutine( CreatePieceAfterCurrentPairFinished( ) );
    }

    private IEnumerator CreatePieceAfterCurrentPairFinished( )
    {
        bool waitForBothTilesToComplete = true;
        while ( waitForBothTilesToComplete )
        {
            if ( puzzlePieceInactive && secondPuzzlePiecePair.puzzlePieceInactive )
            {
                Subject.Notify( PuzzleSpawner.CREATE_NEW_PIECE );
                waitForBothTilesToComplete = false;
            }
            yield return new WaitForSeconds( 0.009f );
        }
    }
}