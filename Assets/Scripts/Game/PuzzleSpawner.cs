using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleSpawner : UnityObserver
{
    public GameObject gamePiecePrefab;
    public List<GamePuzzlePiece> puzzlePiecesOnBoard = new List<GamePuzzlePiece>( );
    public Material[ ] gamePieceMaterials;
    public const string CREATE_NEW_PIECE = "CREATE_NEW_PIECE";
    private GameManager gameManager;
    private Vector3 firstSpawnPosition;
    private Vector3 secondSpawnPosition;
    private PuzzleGrid puzzleGrid;

    public override void OnNotify( Object sender, EventArguments e )
    {
        if ( e.eventMessage == CREATE_NEW_PIECE )
        {
            PlaceTileOnGrid( );
        }
    }

    public void StartPuzzleSpawning( )
    {
        gameManager = FindObjectOfType< GameManager >( );
        puzzleGrid = GetComponent<PuzzleGrid>( );
        firstSpawnPosition = puzzleGrid.gridTiles[ 2, 9 ].transform.localPosition;
        secondSpawnPosition = puzzleGrid.gridTiles[ 3, 9 ].transform.localPosition;
        PlaceTileOnGrid( );
    }

    private void PlaceTileOnGrid( )
    {
        FirstPuzzlePiece firstPuzzlePiece = CreateFirstPuzzlePiece( );
        SecondPuzzlePiece secondPuzzlePiece = CreateSecondPuzzlePiece( );
        gameManager.piecesOnBoard.Add( firstPuzzlePiece );
        gameManager.piecesOnBoard.Add( secondPuzzlePiece );
        firstPuzzlePiece.transform.position = firstSpawnPosition;
        secondPuzzlePiece.transform.position = secondSpawnPosition;
        firstPuzzlePiece.secondPuzzlePiecePair = secondPuzzlePiece;
        secondPuzzlePiece.firstPuzzlePiecePair = firstPuzzlePiece;
    }

    private FirstPuzzlePiece CreateFirstPuzzlePiece( )
    {
        GameObject createFirstPuzzlePiece = Instantiate( gamePiecePrefab );
        FirstPuzzlePiece newComponent = createFirstPuzzlePiece.AddComponent<FirstPuzzlePiece>( );
        newComponent.transform.parent = puzzleGrid.transform;
        newComponent.pieceType = PuzzlePieceType.FIRST_PUZZLE_PIECE;
        CreateRandomPiece( newComponent );
        return newComponent;
    }

    private SecondPuzzlePiece CreateSecondPuzzlePiece( )
    {
        GameObject createSecondPuzzlePiece = Instantiate( gamePiecePrefab );
        SecondPuzzlePiece newComponent = createSecondPuzzlePiece.AddComponent<SecondPuzzlePiece>( );
        newComponent.transform.parent = puzzleGrid.transform;
        newComponent.pieceType = PuzzlePieceType.SECOND_PUZZLE_PIECE;
        CreateRandomPiece( newComponent );
        return newComponent;
    }

    private void CreateRandomPiece( GamePuzzlePiece puzzlePiece )
    {
        int randomNumber = Random.Range( 0, 4 );
        if ( randomNumber == 0 )
        {
            puzzlePiece.tag = "Cannon";
            puzzlePiece.GetComponentInChildren<MeshRenderer>( ).material = gamePieceMaterials[ 0 ];
        }
        else if ( randomNumber == 1 )
        {
            puzzlePiece.tag = "Repair";
            puzzlePiece.GetComponentInChildren<MeshRenderer>( ).material = gamePieceMaterials[ 1 ];
        }
        else if ( randomNumber == 2 )
        {
            puzzlePiece.tag = "Sailor";
            puzzlePiece.GetComponentInChildren<MeshRenderer>( ).material = gamePieceMaterials[ 2 ];
        }
        else if ( randomNumber == 3 )
        {
            puzzlePiece.tag = "Soldier";
            puzzlePiece.GetComponentInChildren<MeshRenderer>( ).material = gamePieceMaterials[ 3 ];
        }
    }
}