﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Fields
    private int numberOfActiveness;
    public bool IsSafePlatform;
    public GameObject textChild;
    public GameObject spawnObject;
    public List<Warrior> warriorsWithin;//all the warriors inside this platform
    [SerializeField] private PlatformCore core;

    // Properties
    public int NumberOfActiveness
    {
        get {return numberOfActiveness;}
        set
        {
            if (value == 0)
                IsSafePlatform = true;
            else
                IsSafePlatform = false;
            numberOfActiveness = value;
        }
    }
    public PlatformCore Core { get => core; set => core = value; }

    // Methods
    private void Awake()
    {
        InitializePlatform();
    }
    private void InitializePlatform()
    {
        numberOfActiveness  = 0;
    }
    private void OnTriggerEnter(Collider _collider)
    {
        Warrior enteredWarrior = _collider.gameObject.GetComponent<Warrior>();
        if (enteredWarrior == null)
            return;

        warriorsWithin.Add(enteredWarrior);
        numberOfActiveness ++;
        bool needsNewPointer = true;
        Pointer pointerOfWarrior = _collider.gameObject.GetComponentInChildren<Pointer>();
        foreach(Progress thisProgress in enteredWarrior.ProgressList)
            if (thisProgress.TargetPlatform == this.gameObject)
                {
                    pointerOfWarrior.UpdatePointer(thisProgress);
                    needsNewPointer = false;
                    break;
                }
        if(needsNewPointer)
            pointerOfWarrior.InitializePointer(this.gameObject);
    }
    private void OnTriggerExit(Collider _collider)
    {
        Warrior exitedWarrior = _collider.gameObject.GetComponent<Warrior>();
        if(exitedWarrior == null)
            return;
        Pointer pointerOfWarrior = _collider.gameObject.GetComponentInChildren<Pointer>();
        pointerOfWarrior.DontAllowCheck();
        numberOfActiveness --;
        warriorsWithin.Remove(exitedWarrior);
    }
}
