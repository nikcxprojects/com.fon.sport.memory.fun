using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    private MemoryGameManager _gameManager;
    private Animation _animation;
    private Button button;

    private List<int> ids = new List<int>();

    private void OnEnable()
    {
        _gameManager = FindObjectOfType<MemoryGameManager>();
        _animation = GetComponent<Animation>();
        button = GetComponent<Button>();
    }

    private void EnableButton()
    {
        button.enabled = true;
    }

    public void SetId(int id)
    {
        ids.Add(id);
    }
    
    public void SetColor(Color color)
    {
        Debug.Log(button.image == null);

        button.image.color = color;
    }
    
    public void OnClick()
    {
        OpenCard();
        _gameManager.ClickCard(PopList(ids));
    }

    private int PopList(List<int> list)
    {
        if (ids.Count != 0)
        {
            var i = list[0];
            list.Remove(list[0]);
            return i;
        }
        
//        _gameManager.GameOver();
        return 100;
    }

    public void ClearStack()
    {
        ids.Clear();
    }

    public void CloseCard()
    {
        _animation.Play("CloseCard");
    }

    public void OpenCard()
    {
        _animation.Play("OpenCard");
        Invoke("CloseCard", 0.5f);
    }

    public void DeletePair()
    {
        Invoke("Delete", 1.5f);
    }

    private void Delete()
    {
        _animation.Play("DeleteCard");
        Invoke("destroy", 0.5f);
    }
    
    private void destroy()
    {
        Destroy(gameObject);
    }
}
