﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformManager : singleton<platformManager>
{
    public List<GameObject> platformList;
    protected override void Awake() 
    {
        base.Awake();
    }
}
