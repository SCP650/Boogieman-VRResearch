﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum Face {front, back, left, right, top, bot}

public enum Column {left, center, right}

public enum Row {bot, mid, top}

public enum AttackType { ball, line, lasso }

[System.Serializable]
public struct AttackOption
{
    public AttackType attackType;
    [HideInInspector]
    public Face direction;

    public int col;

    public int row;

    public Vector3 pos()
    {
        float r = (float) row * 0.25f + 0.5f;
        float c = (float) col;
        // //TODO: un-hardcode
        // if (r == 0) r = 1f;
        return Vector3.right * c + Vector3.up * r;
    }

    public Vector3 dir()
    {
        switch (direction)
        {
            case Face.front:
                return Vector3.forward;
                break;
            case Face.back:
                return Vector3.back;
                break;
            case Face.left:
                return Vector3.left;
                break;
            case Face.right:
                return Vector3.right;
                break;
            case Face.top:
                return Vector3.up;
                break;
            case Face.bot:
                return Vector3.down;
                break;
        }

        return Vector3.zero;
    }

}

[CreateAssetMenu(menuName = "AttackController")]
public class AttackController : ScriptableObject
{
    [SerializeField] public Session BallSession;
    [SerializeField] public Session LineSession;
    [SerializeField] public Session LassoSession;
    
    [SerializeField] public Vector3Ref Place;
    [SerializeField] public Vector3Ref Orientation;
}