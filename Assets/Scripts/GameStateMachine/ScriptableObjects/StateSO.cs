using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game State", menuName = "Game State", order = 0)]
public class StateSO : ScriptableObject
{
    public bool canMove;
    public bool canMenu;
    public string stateName;
}
