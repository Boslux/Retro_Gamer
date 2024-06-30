using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviour
{

    PlayerBag _bag;
    public Text highMoney;
    public Text lastMoney;
    void Awake()
    {
        _bag=Resources.Load<PlayerBag>("PlayerBag");
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
    }
    void GameOver()
    {
        highMoney.text="High Money: "+_bag.highMoney.ToString();   
        lastMoney.text="Last Money: "+_bag.money.ToString();
    }
}
