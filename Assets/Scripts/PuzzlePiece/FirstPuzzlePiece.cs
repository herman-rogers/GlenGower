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

    public override IEnumerator DestoryPuzzlePiece( )
    {
        while ( !secondPuzzlePiecePair.puzzlePieceInactive || !puzzlePieceInactive )
        {
            yield return new WaitForSeconds( 1.0f );
        }
        currentGridTileLocation.puzzlePieceOnTile = null;
        currentGridTileLocation.currentState = GridState.GRID_IS_EMPTY;
        GameObject.Destroy( gameObject );
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
        StopCoroutine( dragPuzzlePiece );
        OccupyTileGrid( );
        RemoveTouchEvents( );
        SetTileRotationToFaceCamera( );
        secondPuzzlePiecePair.tileSpeed = 0.1f;
        yield return StartCoroutine( SetTilePositionToClosestGridTile( ) );
    }
}