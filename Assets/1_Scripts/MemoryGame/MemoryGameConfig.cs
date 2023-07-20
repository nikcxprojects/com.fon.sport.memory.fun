using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config", order = 50)]
public class MemoryGameConfig : ScriptableObject
{
    public Color[] colors;
    public int level;
}
