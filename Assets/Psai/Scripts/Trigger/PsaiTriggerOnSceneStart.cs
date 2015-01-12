//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using psai.net;

public class PsaiTriggerOnSceneStart : MonoBehaviour
{
    public int _themeId = 1;

#if !(UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
    [Range(0.01f, 1)]
#endif
    public float _intensity = 1.0f;


    void Start()
    {
        TriggerOnce();
    }

	void OnLevelWasLoaded()
    {
        TriggerOnce();
	}

    void TriggerOnce()
    {
        PsaiResult rc = PsaiCore.Instance.TriggerMusicTheme(_themeId, _intensity);

        if (rc != PsaiResult.OK)
        {
            Debug.LogError("PsaiTriggerOnSceneStart failed! " + rc);
        }        
    }
	
}
