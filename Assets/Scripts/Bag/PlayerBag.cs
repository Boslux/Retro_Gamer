using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBag", menuName = "PlayerBag", order = 0)]
public class PlayerBag : ScriptableObject 
{
    public int money=10;

    public int gas=4;
    public int highMoney;
    
    
    public int win=0;
    public int lost=0;
    public int bossLevel=5;
}