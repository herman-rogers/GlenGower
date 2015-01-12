using UnityEngine;
using System.Collections;

public class ObjectFloat : MonoBehaviour
{
    public float bounceDamp;
    public Vector3 buoyancyCenterOffset;
    private float waterLevel;
    private float floatHeight;
    private Rigidbody currentRigidbody;
    

    private void Start( )
    {
        currentRigidbody = GetComponent<Rigidbody>( );
        //ocean = FindObjectOfType<OceanWave>( ).gameObject;
    }

    private void FixedUpdate( )
    {
        Vector3 actionPoint = transform.position + transform.TransformDirection( buoyancyCenterOffset );
        float forceFactor = 1.0f - ( ( actionPoint.y - waterLevel ) / floatHeight );
        if ( forceFactor > 0.0f )
        {
            Vector3 uplift = -Physics.gravity * ( forceFactor - GetComponent<Rigidbody>( ).velocity.y * bounceDamp );
            GetComponent<Rigidbody>( ).AddForceAtPosition( uplift, actionPoint );
        }
    }

    private void OnTriggerEnter( Collider col )
    {
        waterLevel = col.ClosestPointOnBounds( transform.position ).y + 1;
        floatHeight = col.ClosestPointOnBounds( transform.position ).y;
    }

    //private void ObjectUnderwater( )
    //{
    //    Debug.DrawLine( transform.localPosition, -Vector3.up * 100, Color.red );
    //    RaycastHit hit;
    //    if ( Physics.Raycast( transform.localPosition, -Vector3.up * 100, out hit ) )
    //    {
    //        Debug.Log( "HIT POSITION: " + hit.transform.position.y );
    //    }
    //}
}