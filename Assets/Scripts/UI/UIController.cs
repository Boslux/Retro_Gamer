using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    PlayerBag _playerBag;
    
    [Header("Text")]
    public Text money;
    public Text win;
    public Text lost;
    public Text highMoney;
    public Text lastMoney;
    
    [Header("Gas")]
    public Image gasImage;
    public Sprite[] sprites;

    private void Awake() 
    {
        _playerBag=Resources.Load<PlayerBag>("PlayerBag");    
    }
    void Update()
    {
        TextControl();
        GasControl();
        UpdateHighScore(_playerBag.money);
    }
    void TextControl()
    {
        money.text=":"+_playerBag.money.ToString();
        win.text="Win: "+_playerBag.win.ToString();   
        lost.text="Lost: "+_playerBag.lost.ToString();
    }
    void GasControl()
    {
        int gasLevel = _playerBag.gas;
        if (gasLevel >= 0 && gasLevel <= 4)
        {
            gasImage.sprite = sprites[gasLevel];
        }
    }

    void UpdateHighScore(int currentMoney)
    {
        if (currentMoney > _playerBag.highMoney)
        {
            _playerBag.highMoney = currentMoney;
        }
    }
}
