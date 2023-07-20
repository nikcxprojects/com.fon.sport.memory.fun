using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigController : MonoBehaviour
{
    [SerializeField] private MemoryGameConfig _gameConfig;

    private void OnEnable()
    {
        _gameConfig.level = 1;
    }
    
    public void SwitchLevel(int id)
    {
        _gameConfig.level = id;
    }
}
