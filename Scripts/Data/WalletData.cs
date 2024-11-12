using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop/Wallet", order = 1)]
public class WalletData : ScriptableObject
{
    public int startMoney;
    public int startMoneyIncome;
    public float incomeInterval;
    public int startKillBountyPercent;
}
