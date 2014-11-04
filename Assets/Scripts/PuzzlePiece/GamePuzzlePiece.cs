using UnityEngine;
using System.Collections;

public class GamePuzzlePiece : MonoBehaviour
{
    public GameObject renderer;
    private float gridYBoundary;
    private const float tileSpeed = 0.05f;
    //private float gridXUpperBoundary;
    //private float gridXLowerBoundary;

    public void MoveTilePiece( float lowestPosition )
    {
        gridYBoundary = lowestPosition;
        StartCoroutine( MoveTileDown( ) );
    }

    private IEnumerator MoveTileDown( )
    {
        while ( true )
        {
            transform.position = new Vector3( transform.position.x,
                                            ( transform.position.y - tileSpeed ),
                                              transform.position.z );
            yield return new WaitForSeconds( 0.009f );
            if ( transform.position.y <= GridBoundaries.lowerYBoundary )
            {
                renderer.GetComponent<Animator>( ).enabled = false;
                renderer.transform.rotation = Quaternion.identity;
                StopAllCoroutines( );
            }
        }
    }
}
