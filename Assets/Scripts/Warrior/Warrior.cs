﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    // Fields
    private float health;
    private float armor;
    private float damage;
    private Profile ownerProfile;
    private int platformLocation;
    private List<Progress> progressList;
    [SerializeField] private Animator warriorAmimator;

    // Properties
    public float Health { get => health; set => health = value; }
    public float Armor { get => armor; set => armor = value; }
    public float Damage { get => damage; set => damage = value; }
    public Profile OwnerProfile { get => ownerProfile; set => ownerProfile = value; }
    public int PlatformLocation 
    {
        get {return platformLocation;}
        set 
        {
            int numberOfPlatforms = platformManager.instance.platformList.Count;
            if (value < numberOfPlatforms)
                platformLocation = value;
            else
                platformLocation = value % numberOfPlatforms;
        }
    }
    public List<Progress> ProgressList { get => progressList; set => progressList = value; }


    // Methods
    void Awake()
    {
        Armor = 1;
        damage = 10;
        platformLocation = 0;
        progressList = new List<Progress>();
        warriorAmimator = gameObject.GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            ChangePlatform();
    }
    private void ChangePlatform()
    {
        warriorAmimator.SetTrigger("OnSwitch");
        GameObject nextPlatform;
        PlatformLocation ++;
        nextPlatform = platformManager.instance.platformList[PlatformLocation];
        ChangeWarriorPosition(nextPlatform);
    }
    void ChangeWarriorPosition(GameObject targetPlatform)
    {
        GameObject spawnObject = targetPlatform.GetComponent<Platform>().spawnObject;
        Transform spawnTransform = spawnObject.GetComponent<Transform>();
        Vector3 spawnLocation = spawnTransform.localPosition + spawnTransform.parent.position;
        gameObject.GetComponent<Transform>().position = spawnLocation;
    }
    public void Attack()
    {
        warriorAmimator.SetTrigger("OnSimpleAttack");
        gameObject.GetComponent<SoundManager>().PlaySound();
    }
    public void BonusAttack(int continuousCorrects)
    {
        switch(continuousCorrects)
        {
            case 3 :
            {
                warriorAmimator.SetTrigger("OnBonusAttack1");
                gameObject.GetComponent<SoundManager>().PlaySound();
                break;
            }
            case 5 :
            {
                warriorAmimator.SetTrigger("OnBonusAttack2");
                gameObject.GetComponent<SoundManager>().PlaySound();
                break;
            }
            case 7 :
            {
                warriorAmimator.SetTrigger("OnBonusAttack3");
                gameObject.GetComponent<SoundManager>().PlaySound();
                break;
            }
            default :
            break;
        }
    }
}
