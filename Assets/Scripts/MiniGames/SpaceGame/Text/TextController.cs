using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [Header("Texts")]
    public Text player;
    public Text enemy;

    public Text oldMoney;
    public Text newMoney;

    [Header("Scripts")]
    public PlayerController pl;
    public BossMovement boss;
    public EndGameController endGame;
    PlayerBag _bag;

    private void Awake() 
    {
        _bag=Resources.Load<PlayerBag>("PlayerBag");
    }
    
    void Update()
    {
        TextControl();
        EndGameText();
    }

    void TextControl()
    {
        player.text="HEALTH:"+ pl.health.ToString();
        enemy.text="HEALTH:"+ boss.health.ToString();
    }
    void EndGameText()
    {
        oldMoney.text="Old Money: "+endGame.temporaryMoney.ToString();
        newMoney.text="New Money: "+_bag.money.ToString();
    }
    public void TurnGame()
    {
        SceneManager.LoadScene(1);
    }
}
