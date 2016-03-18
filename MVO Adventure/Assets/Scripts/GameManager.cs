using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int playerHP;
    private int playerCurrentHP;
    private int playerMinDmg;
    private int playerMaxDmg;
    private int playerArmor;
    private int playerXp = 0;

    public int enemyHP;
    public int enemyCurrentHP;
    public int enemyMinDmg;
    public int enemyMaxDmg;
    public int enemyArmor;
    public int enemyXp;
    public Sprite enemySprite;

    private int enemyDmg;
    private int playerDmg;
    private float enemyAttackDelay = 1f;
    private float nextAttack;

    private float gameOverDelay = 2f;
    private float delay = 1f;

    public bool inCombat = false;
    bool playerTurn = true;

    private GameObject gameOverImage;
    private GameObject combatUI;
    private GameObject mainCam;
    private GameObject mapCam;
    private GameObject battleCam;
    private GameObject combatEnemy;
    private LevelManager levelManager;

    private Text playerHPText;
    private Text enemyHPText;
    private Text combatText;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        UpdateStats();
        SetupGameObjects();
    }

    void Update()
    {
        if (inCombat)
            if (!playerTurn)
                if(Time.time > nextAttack)
                    EnemyAttack();
    }

    public void OnEnterCombat()
    {
        combatEnemy = GameObject.Find("CombatEnemy");
        combatEnemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        combatUI.SetActive(true);
        inCombat = true;
        mainCam.SetActive(false);
        mapCam.SetActive(false);
        battleCam.SetActive(true);
        UpdateCombatUI();
    }

    void EndCombat()
    {
        inCombat = false;
        playerTurn = true;
        combatUI.SetActive(false);
        mainCam.SetActive(true);
        mapCam.SetActive(true);
        battleCam.SetActive(false);
        UpdateStats();
    }

    public void PlayerAttack()
    {
        if(playerTurn)
        {
            playerDmg = Random.Range(playerMinDmg, playerMaxDmg) - enemyArmor;
            if (playerDmg >= 1)
                enemyCurrentHP -= playerDmg;
            else
            {
                playerDmg = 1;
                enemyCurrentHP -= playerDmg;
            }
            UpdateCombatUI();
            playerTurn = false;
            nextAttack = Time.time + enemyAttackDelay;
        }
    }

    void EnemyAttack()
    {
        enemyDmg = Random.Range(enemyMinDmg, enemyMaxDmg) - playerArmor;
        if (enemyDmg >= 1)
            playerCurrentHP -= enemyDmg;
        else
        {
            enemyDmg = 1;
            playerCurrentHP -= playerDmg;
        }
        UpdateCombatUI();
        playerTurn = true;
    }

    public void RunAway()
    {
        playerXp -= 1;
        EndCombat();      
    }

    void UpdateStats()
    {
        playerHP = 100 + playerXp * 5;
        playerCurrentHP = playerHP;
        playerMinDmg = 5 + playerXp / 2;
        playerMaxDmg = playerMinDmg * 2;
        playerArmor = 1 + playerXp / 4;
    }

    void GameOver()
    {
        gameOverImage.SetActive(true);
        Invoke("RestartGame", gameOverDelay); 
    }

    void RestartGame()
    {
        levelManager.LoadLevel("01a Start");
    }

    void SetupGameObjects()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        gameOverImage = GameObject.Find("GameOverImage");
        combatUI = GameObject.Find("CombatUI");
        mainCam = GameObject.Find("Main Camera");
        mapCam = GameObject.Find("MiniMap");
        battleCam = GameObject.Find("Battle Camera");
        playerHPText = GameObject.Find("PlayerHP").GetComponent<Text>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<Text>();
        combatText = GameObject.Find("CombatText").GetComponent<Text>();
        gameOverImage.SetActive(false);
        combatUI.SetActive(false);
        battleCam.SetActive(false);
    }

    void UpdateCombatUI()
    {
        playerHPText.text = "HP: " + playerCurrentHP + " / " + playerHP;
        enemyHPText.text = "HP: " + enemyCurrentHP + " / " + enemyHP;
        if (playerCurrentHP <= 0)
        {
            combatText.text = "Oh no! You got killed by the enemy!";
            Invoke("GameOver", delay);
        }
        else if (enemyCurrentHP <= 0)
        {
            combatText.text = "You have slain the enemy!";
            playerXp += enemyXp;
            Invoke("EndCombat", delay);
        }
        else if (playerHP == playerCurrentHP && enemyHP == enemyCurrentHP)
            combatText.text = "";
        else if (playerTurn)
            combatText.text = "You hit the enemy for " + playerDmg + " damage!";
        else if (!playerTurn)
            combatText.text = "Enemy hits you for " + enemyDmg + " damage!";
    }
}
