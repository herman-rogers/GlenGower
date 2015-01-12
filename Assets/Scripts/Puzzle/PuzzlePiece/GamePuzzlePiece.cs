using UnityEngine;
using System;
using System.Collections;
using TouchScript;
using TouchScript.Gestures;

public enum PuzzlePieceType
{
    FIRST_PUZZLE_PIECE,
    SECOND_PUZZLE_PIECE,
    SPECIAL_PUZZLE_PIECE
}

public class GamePuzzlePiece : MonoBehaviour
{
    public PuzzlePieceType pieceType;
    public GridTile currentGridTileLocation{ get; private set; }
    public bool puzzlePieceInactive { get; private set; }
    public float tileSpeed = 0.03f;
    protected Camera mainCamera;
    protected PuzzleGrid currentPuzzleGrid;
    protected IEnumerator dragPuzzlePiece;
    private GameObject puzzleGameObject;

    private void Start( )
    {
        puzzleGameObject = transform.GetChild( 0 ).gameObject;
        currentPuzzleGrid = transform.GetComponentInParent<PuzzleGrid>( );
        mainCamera = FindObjectOfType<Camera>( );
        IntitializePuzzlePiece( );
    }

    public virtual void IntitializePuzzlePiece( )
    { 
    }

    public virtual void TappedHandler( object sender, EventArgs e )
    {
    }

    public void SetPuzzlePieceCoordinates( float x, float y )
    {
        transform.position = new Vector3( x, y, transform.position.z );
    }

    public virtual void StartDragCoroutine( object sender, EventArgs e )
    {
    }

    public virtual void StopDraggingPuzzlePiece( object sender, EventArgs e )
    {
    }

    public virtual IEnumerator DestoryPuzzlePiece( )
    {
        yield return new WaitForEndOfFrame( );
    }

    private void Update( )
    {
        UpdateGridLocation( );
        HasPuzzlePieceHitBoundaryOrOccupiedTile( );
        SetPuzzlePieceCoordinates( transform.position.x, ( transform.position.y - tileSpeed ) );
    }


    protected bool DoesPuzzlePieceOccupySameColumn( GridTile firstGridTile, GridTile SecondGridTile )
    {
        UpdateGridLocation( );
        if ( firstGridTile.tileColumn == SecondGridTile.tileColumn )
        {
            return true;
        }
        return false;
    }

    protected virtual void UpdateGridLocation( )
    {
        GridTile cachedTile = currentGridTileLocation;
        currentGridTileLocation = currentPuzzleGrid.SearchForClosestGridTileByCoordinates( transform.position.x, 
                                                                                           transform.position.y );
        if ( cachedTile != null &&
             cachedTile != currentGridTileLocation 
             && cachedTile.currentState == GridState.GRID_IS_OCCUPIED )
        {
            currentGridTileLocation.puzzlePieceOnTile = null;
            cachedTile.currentState = GridState.GRID_IS_EMPTY;
        }
    }

    protected virtual IEnumerator EndTileLife( )
    {
        yield return new WaitForEndOfFrame( );
    }

    protected void OccupyTileGrid( )
    {
        currentGridTileLocation.puzzlePieceOnTile = this;
        currentGridTileLocation.currentState = GridState.GRID_IS_OCCUPIED;
    }

    protected void RemoveTouchEvents( )
    {
        GetComponent<TapGesture>( ).Tapped -= TappedHandler;
        GetComponent<PanGesture>( ).PanStarted -= StopDraggingPuzzlePiece;
        GetComponent<PanGesture>( ).PanCompleted -= StopDraggingPuzzlePiece;
    }

    protected void SetTileRotationToFaceCamera( )
    {
        GetComponentInChildren<Animator>( ).enabled = false;
        puzzleGameObject.transform.rotation = Quaternion.identity;
    }

    protected IEnumerator SetTilePositionToClosestGridTile( )
    {
        while ( gameObject.transform.position.y > currentGridTileLocation.coordinates.y )
        {
            SetPuzzlePieceCoordinates( currentGridTileLocation.coordinates.x, ( transform.position.y - 0.05f ) );
            yield return new WaitForSeconds( 0.009f );
        }
        SetPuzzlePieceCoordinates( currentGridTileLocation.coordinates.x, currentGridTileLocation.coordinates.y );
    }

    public void HasPuzzlePieceHitBoundaryOrOccupiedTile( )
    {
        if ( currentPuzzleGrid.IsNextCellOccupied( currentGridTileLocation )
             || transform.position.y <= GridBoundaries.lowerYBoundary )
        {
            puzzlePieceInactive = true;
            tileSpeed = 0.1f;
            StartCoroutine( EndTileLife( ) );
        }
        else
        {
            puzzlePieceInactive = false;
        }
    }
}