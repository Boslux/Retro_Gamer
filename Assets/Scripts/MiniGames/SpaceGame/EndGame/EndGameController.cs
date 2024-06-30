using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [Header("Scripts")]
    PlayerController _pl;
    BossMovement _boss;
    PlayerBag _bag;

    [Header("GameObject")]
    public GameObject endGameCanvas;
    public GameObject winGameCanvas;
    public GameObject lostGameCanvas;

    [Header("others")]
    public int temporaryMoney;

    private bool gameEnded = false; // Oyun sonlandığında true olur

    private void Awake()
    {
        _pl = GameObject.Find("Player").GetComponent<PlayerController>();   
        _boss = GameObject.Find("enemy").GetComponent<BossMovement>();
        _bag = Resources.Load<PlayerBag>("PlayerBag"); 
    }

    private void Update()
    {
        GameControl();
    }

    void GameControl()
    {
        if (!gameEnded && (_pl.health <= 0 || _boss.health <= 0))
        {
            gameEnded = true; // Oyun sonlandığında bayrağı true yap
            endGameCanvas.SetActive(true);

            if (_pl.health <= 0)
            {
                lostGameCanvas.SetActive(true);
                NewMoney();
            }
            else if (_boss.health <= 0)
            {
                temporaryMoney = _bag.money;
                winGameCanvas.SetActive(true);
                NewMoney();
            }
        }
    }

    void NewMoney()
    {
        if (_pl.health <= 0)
        {
            temporaryMoney = _bag.money;
            _bag.lost++;
            _bag.money -= 100;
        }
        else if (_boss.health <= 0)
        {
            temporaryMoney = _bag.money;
            _bag.win++;
            _bag.money += 100;
        }
    }
}
