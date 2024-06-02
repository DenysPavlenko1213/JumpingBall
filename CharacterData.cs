using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public int speed;
    public float JumpForce;
    public enum Buff {None,DoubleMoney,Protection,DoubleScore}
    public Buff buff;
}
