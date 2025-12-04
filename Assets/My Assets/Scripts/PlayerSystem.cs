using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PlayerSystem : MonoBehaviour
{
    protected Player player;
    public Player PlayerRef => player; // read-only public access

    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
    }

}
