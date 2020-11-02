﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardDef;


// Use this component only for generating keyboard.
// Attach it to an instance of KeyboardGenerator prefab ,
// Then Hit Run to generate keyboard.
[ExecuteInEditMode]
public class KeyboardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject keyGameObject;
    [SerializeField] private KeyboardLayout keyboard;
    [SerializeField] private List<GameObject> rows;

    private void Start() 
    {
        GenerateKeyboard();
    }
    private void GenerateKeyboard()
    {
        keyboard.AdjustRows();
        GenerateRow(rows[0] , 0 , keyboard.GetEndIndexInRows[0]);
        GenerateRow(rows[1] , keyboard.GetEndIndexInRows[0] + 1 , keyboard.GetEndIndexInRows[1]);
        GenerateRow(rows[2] , keyboard.GetEndIndexInRows[1] + 1 , keyboard.GetEndIndexInRows[2]);
        GenerateRow(rows[3] , keyboard.GetEndIndexInRows[2] + 1 , keyboard.GetEndIndexInRows[3]);
        GenerateRow(rows[4] , keyboard.GetEndIndexInRows[3] + 1 , keyboard.GetEndIndexInRows[4]);
    }
    private void GenerateRow(GameObject targetRow , int startIndex , int endIndex)
    {
        for(int index = startIndex ; index <= endIndex ;  index++)
            GenerateKey(targetRow , index);
    }
    private void GenerateKey(GameObject targetRow , int keyIndex)
    {
        GameObject key = GameObject.Instantiate(keyGameObject) as GameObject;
        key.name = keyboard.GetDefaultKeyList[keyIndex];
        key.transform.parent = targetRow.transform;
        key.GetComponent<Key>().KeyPrimaryValue = keyboard.GetDefaultKeyList[keyIndex];
        StyleKey(key , keyIndex);
    }
    private void StyleKey(GameObject key , int keyIndex)
    {
        float keyWidth = 25 * keyboard.GetKeyWidthList[keyIndex];
        key.GetComponentInChildren<RectTransform>().localScale = Vector3.one;
        key.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(keyWidth,25);
        key.GetComponentInChildren<Text>().text = keyboard.GetDefaultKeyList[keyIndex];
    }
}
