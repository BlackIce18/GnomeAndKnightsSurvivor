using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public Transform player;
    public PlayerData playerData;
    public EnemyPoolData enemyPoolData;
    public SpawnTimingsData spawnTimings;
    public TextMeshProUGUI timerText;
    public Slider timerSlider;
    public Camera mainCamera;
    public EnemySpawnPatterns enemySpawnPatterns;
    public Transform parentForBullets;

    public AttackTypeData normalTypeAttackData;
    public AttackTypeData piercingTypeAttackData;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyIncomeText;
    public TextMeshProUGUI killBountyText;
    public WalletData walletData;
    public Shop shop;

    public PlayerStats playerStats;
    public GunsPrefabList gunsPrefabList;
}
