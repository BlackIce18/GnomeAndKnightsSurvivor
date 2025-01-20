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
    public Slider xpSlider;
    public Camera mainCamera;
    public EnemySpawnPatterns enemySpawnPatterns;

    public LvlAndXpData lvlAndXpData;
    public AttackTypeData clearTypeAttackData;
    public AttackTypeData normalTypeAttackData;
    public AttackTypeData piercingTypeAttackData;
    public AttackTypeData magicTypeAttackData;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyIncomeText;
    public TextMeshProUGUI killBountyText;
    public WalletData walletData;
    public Shop shop;
    public BorderColorsData shopBorderColorsData;

    public PlayerStats playerStats;
    public GunsPrefabList gunsPrefabList;

    public GameObject damageTextPrefab;
    public GameObject damageTextParent;

    public HealthAndManaShieldBar healthBar;
    public HealthAndManaShieldBar manaShieldBar;
}
