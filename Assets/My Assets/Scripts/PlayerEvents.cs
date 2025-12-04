using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

/* 
 * This is a struct that contains Actions that are basically event dispachers to allow connected
 * functionality between scripts. This all connects through player.ID.events
 */


public class PlayerEvents
{
    public Action OnRoll;
    public Action<bool> OnRollingStateChange;
    public Action<bool> OnDeathStateChange;
    public Action OnHealthChange;
    public Action OnPointsChange;
}


