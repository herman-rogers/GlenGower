//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using psai.net;

public class PsaiTriggerWhenInRange : PsaiContinuousTrigger
{    
    public float radius = 42;

#if !(UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
    [Range(0.01f, 1)]
#endif
    public float minimumIntensity = 0.1f;

#if !(UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
    [Range(0.01f, 1)]
#endif
    public float maximumIntensity = 1.0f;
    public bool scaleIntensityByDistance = true;

    public Collider playerCollider;

    private Collider PlayerCollider
    {
        get
        {
            if (playerCollider == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {
                    playerCollider = playerObject.GetComponent<Collider>();
                }
            }
            return playerCollider;
        }

        set
        {
            playerCollider = value;
        }

    }

    public override float CalculateTriggerIntensity()
    {
        float distance;

        if (this.GetComponent<Collider>() != null)
        {
            Vector3 closestPointOnThisCollider = GetComponent<Collider>().ClosestPointOnBounds(PlayerCollider.gameObject.transform.position);
            distance = (closestPointOnThisCollider - PlayerCollider.ClosestPointOnBounds(closestPointOnThisCollider)).magnitude;
        }
        else
        {
            distance = (gameObject.transform.position - PlayerCollider.ClosestPointOnBounds(gameObject.transform.position)).magnitude;
        }

        if (distance < radius)
        {
            if (scaleIntensityByDistance)
            {
                float distanceRatio = 1.0f - (distance / radius);
                float triggerIntensity = minimumIntensity + (1.0f - minimumIntensity) * distanceRatio;
                return triggerIntensity;
            }
            else
            {
                return maximumIntensity;
            }
        }
        else
        {
            return 0;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }
}
