using UnityEngine;
using System.Collections;

public class OceanWave : MonoBehaviour
{
    public float frequency = 0.1f;
    public float amplitude = 0.01f; //amplitude is the vertical distance
    public float waveLength = 0.05f;
    private Mesh mesh;
    private Vector3 waveSource = new Vector3( 2.0f, 0.0f, 2.0f );
    private Vector3[ ] meshVertices;
    private MeshCollider oceanCollider;

    private void Start( )
    {
        MeshFilter currentMesh = GetComponent<MeshFilter>( );
        oceanCollider = GetComponent<MeshCollider>( );
        if ( currentMesh == null )
        {
            Debug.Log( "No mesh filter" );
            return;
        }
        mesh = currentMesh.mesh;
        meshVertices = mesh.vertices;
    }

    private void Update( )
    {
        CalcWave( );
    }

    private void CalcWave( )
    {
        for ( int i = 0; i < meshVertices.Length; i++ )
        {
            Vector3 vertice = meshVertices[ i ];
            vertice.y = 0.0f;
            float distance = Vector3.Distance( vertice, waveSource );
            distance = ( distance % waveLength ) / waveLength;
            vertice.y = amplitude * Mathf.Sin( Time.time * Mathf.PI * 2.0f * frequency
            + ( Mathf.PI * 2.0f * distance ) );
            meshVertices[ i ] = vertice;
        }
        mesh.vertices = meshVertices;
        oceanCollider.sharedMesh = null;
        oceanCollider.sharedMesh = mesh;
    }
}
