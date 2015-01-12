//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using psai.net;

public class PsaiTriggerOnPlayerCollision : PsaiContinuousTrigger
{

    public bool _keepTriggeringWhileCollisionStays = true;
    public float _retriggerIntervalInSecondsWhileCollisionStays;

#if !(UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
    [Range(0.01f, 1)]
#endif
    public float _intensity = 1f;
    private bool _collisionWithPlayerDetected;

    private Collider _playerCollider;


    void Start()
    {
        if (_playerCollider == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            _playerCollider = playerObject.GetComponent<Collider>();
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other == _playerCollider)
        {
            _collisionWithPlayerDetected = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == _playerCollider)
        {
            _collisionWithPlayerDetected = false;
        }
    }    

    public override float CalculateTriggerIntensity()
    {
        if (_collisionWithPlayerDetected)
        {
            if (!_keepTriggeringWhileCollisionStays)
                _collisionWithPlayerDetected = false;

            return _intensity;
        }
        
        return 0;
    }
}
