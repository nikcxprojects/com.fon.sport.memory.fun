using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MemoryGameManager : MonoBehaviour
{

    [Header("Game Settings")]

    [SerializeField] private MemoryGameConfig _gameConfig;

    [Header("UI Fields")]
    [SerializeField] private Transform content;
    [SerializeField] private Timer timer;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _clickCardClip;
    [SerializeField] private AudioClip _openCardClip;
    [SerializeField] private AudioClip _nextLevelClip;
    
    private List<GameObject> _cardObjs = new List<GameObject>();
    
    private int prevChildId = 0;
    private int currentIdCard = 0;
    

    void OnEnable()
    {
        UpdateUI();
        Play();
    }
    
    private void SetupButtons()
    {
        foreach (var obj in content.GetComponentsInChildren<CardButton>())
        {
            obj.SetColor(_gameConfig.colors[Random.Range(0, _gameConfig.colors.Length)]);
        }
    }
    
    public void ClickCard(int id)
    {
        AudioManager.getInstance().PlayAudio(_clickCardClip);
        if (id == _gameConfig.level - 1 && currentIdCard == id)
        {
            Vibration.Vibrate(400);
            AudioManager.getInstance().PlayAudio(_nextLevelClip);
            Invoke("NextLevel", 1.5f);
        }
        else if (currentIdCard != id)
        {
            Vibration.Vibrate(200);
            Vibration.Vibrate(200);
            AudioManager.getInstance().PlayAudio(_gameOverClip);
            Invoke("GameOver", 1.5f);
        }

        currentIdCard++;
    }
    
    private void IPlay(int steps)
    {
        EnabledButton(false);
        
        StartCoroutine(OpenCards(steps));
    }

    // Бот показывает последовательность
    private IEnumerator OpenCards(int steps)
    {
        yield return new WaitForSeconds(1.5f);
        SetupButtons();
        for (int i = 0; i < steps; i++)
        {
            // Чтобы не было повторений подряд
            var childId = 0;
            while(childId == prevChildId) childId = Random.Range(0, content.childCount);
            prevChildId = childId;
            
            var obj = content.transform.GetChild(childId).gameObject;
            obj.GetComponent<CardButton>().OpenCard();
            obj.GetComponent<CardButton>().SetId(i);
            AudioManager.getInstance().PlayAudio(_openCardClip);
            yield return new WaitForSeconds(1.4f);
        }

        EnabledButton(true);
    }

    private void UpdateUI()
    {
        levelText.text = $"LEVEL {_gameConfig.level}";
    }

    private void NextLevel()
    {
        _gameConfig.level++;
        timer.Pause(true);
        SceneLoader.getInstance().LoadScene("MemoryGameScene");
    }

    private void EnabledButton(bool val)
    {
        UpdateCadrsList();
        for (var i = 0; i < _cardObjs.Count; i++)
        {
            _cardObjs[i].GetComponent<Button>().enabled = val;
        }
    }
    
    private void UpdateCadrsList()
    {
        _cardObjs.Clear();
        foreach (Transform card in content.transform)
        {
            _cardObjs.Add(card.gameObject);
        }
    }

    #region PublicVoids
    
    public void Play()
    {
        UpdateUI();
        IPlay(_gameConfig.level);
    }
    
    public void GameOver()
    {
        var rec1 = PlayerPrefs.GetInt("R1");
        var rec2 = PlayerPrefs.GetInt("R2");
        var rec3 = PlayerPrefs.GetInt("R3");
        var rec4 = PlayerPrefs.GetInt("R4");
        var rec5 = PlayerPrefs.GetInt("R5");
        List<int> recs = new List<int>()
        {
            rec1,
            rec2,
            rec3,
            rec4,
            rec5
        };
        recs.Reverse();
        recs.Add(_gameConfig.level);
        recs.Reverse();
        PlayerPrefs.SetInt("R1", recs[0]);
        PlayerPrefs.SetInt("R2", recs[1]);
        PlayerPrefs.SetInt("R3", recs[2]);
        PlayerPrefs.SetInt("R4", recs[3]);
        PlayerPrefs.SetInt("R5", recs[4]);
        
        _gameConfig.level = 1;
        gameOverUI.SetActive(true);
        timer.Pause(true);
        //SceneLoader.getInstance().LoadScene("MainMenu");

    }

    public void Restart()
    {
        _gameConfig.level = 1;
        SceneLoader.getInstance().LoadScene("MemoryGameScene");
    }
    
    public void SetGameLvl(int lvl)
    {
        _gameConfig.level = lvl;
    }
    
    #endregion
    
}
