using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public int health;
    public float movementSpeed;
    public Sprite characterSprite;
}

