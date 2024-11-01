using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public Transform player;
    public PlayerData playerData;
    public EnemyPoolData enemyPoolData;
    public SpawnTimingsData spawnTimings;
    public TextMeshProUGUI timerText;
    public Camera mainCamera;
    public EnemySpawnPatterns enemySpawnPatterns;
    public Transform parentForBullets;

    public AttackTypeData normalTypeAttackData;
    public AttackTypeData piercingTypeAttackData;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyIncomeText;
    public TextMeshProUGUI killBountyText;
    public WalletData walletData;
    public ShopItemDatas shopItemDatas;

    public PlayerStats playerStats;
}
