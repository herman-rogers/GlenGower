//-----------------------------------------------------------------------
// <copyright company="Periscope Studio">
//     Copyright (c) Periscope Studio UG & Co. KG. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using psai.net;

/// <summary>
/// This is the base class for Trigger scripts that make use of dynamic intensity levels, and therefore need to trigger Themes continuously.
/// </summary>
/// <remarks>
/// The problem with continuously firing scripts is that they may interfere with each other as soon as they overlap, 
/// causing wild jumps of intensity. Therefore PsaiContinousTrigger provides a boolean parameter to have the 
/// PsaiCoreManager filter and synchronize overlapping trigger events, using only the trigger with the highest intensity for any given Theme.
/// Derive from this class if wish to implement a trigger that controls the musical intensity of a Theme based on an arbitraty parameter
/// in your game world.
/// </remarks>
public abstract class PsaiContinuousTrigger : MonoBehaviour
{
    public int themeId = 1;

    /// <summary>
    /// if set to false the trigger will only fire once in each first tick when the condition was met
    /// </summary> 
    public bool triggerContiuously = true;

    /// <summary>
    /// The interval by which the firing condition of this trigger will be evaluated.
    /// </summary>
    /// <remarks>
    /// If this script is synchronized by the PsaiCoreManager, this value will be ignored and the tick interval of the PsaiCoreManager will be used instead.
    /// </remarks>
    public float tickIntervalInSeconds = 0.25f;

    /// <summary>
    /// Enable this to have the PsaiCoreManager filter and synchronize this trigger script, to avoid problems caused by overlapping triggers.
    /// </summary>
    /// <remarks>
    /// Set this to true if you use other ContinuousTriggers in your Scene that may overlap and interfere with this one. 
    /// The PsaiCoreManager will evaluate those scripts externally so that Triggers 
    /// returning low level intensities will not override Triggers returning high level intensities.
    ///</remarks>
    public bool synchronizeByPsaiCoreManager = false;

    private bool triggerConditionWasTrueInLastTick = false;
    private float tickCounter;

    public int overrideMusicDurationInSeconds;

    
    /// <summary>
    /// Calculates an intensity value that will be used to trigger a psai Theme.
    /// </summary>
    /// <remarks>
    /// This methods evaluates the trigger condition and returns a level of musical intensity. 
    /// Override this method for any custom Contiuous Triggers you may wish to implement to map
    /// the intensity of a game situation to the intensity of the music.   
    /// If the trigger condition failed, the return value must be 0.
    /// </remarks>
    /// <returns>
    /// The trigger intensity between 0.01f and 1.0f, or 0 if the trigger condition failed.
    /// </returns>
    public abstract float CalculateTriggerIntensity();


    public void OnEnable()
    {
        if (synchronizeByPsaiCoreManager)
        {
            GameObject go = GameObject.Find("Psai");
            if (go != null)
            {
                PsaiCoreManager pcm = go.GetComponent<PsaiCoreManager>();
                if (pcm != null)
                {
                    pcm.RegisterContinuousTrigger(this);
                }
            }
        }        
    }

    public void OnDisable()
    {
        if (synchronizeByPsaiCoreManager)
        {
            GameObject go = GameObject.Find("Psai");
            if (go != null)
            {
                PsaiCoreManager pcm = go.GetComponent<PsaiCoreManager>();
                if (pcm != null)
                {
                    pcm.UnregisterContinuousTrigger(this);
                }
            }            
        }        
    }



    public void Update()
    {
        if (!synchronizeByPsaiCoreManager)
        {
            this.tickCounter += Time.deltaTime;
            if (this.tickCounter > this.tickIntervalInSeconds)
            {
                tickCounter -= tickIntervalInSeconds;

                float intensity = CalculateTriggerIntensity();
                if (intensity > 0)
                {
                    if (triggerConditionWasTrueInLastTick == false || triggerContiuously)
                    {
                        if (overrideMusicDurationInSeconds > 0)
                        {
                            PsaiCore.Instance.TriggerMusicTheme(themeId, intensity, overrideMusicDurationInSeconds);
                        }
                        else
                        {
                            PsaiCore.Instance.TriggerMusicTheme(themeId, intensity);
                        }
                    }                    
                }

                triggerConditionWasTrueInLastTick = (intensity > 0);
            }
        }
    }

}

