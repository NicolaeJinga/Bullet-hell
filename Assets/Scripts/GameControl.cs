using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public PlayerController player;
    public SidekickController sidekick;
    public Image gameOverImage;

    [HideInInspector]
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;

    public int scoreAward = 1;
    private int initialScoreAward;
    public int bulletDamage = 1;
    public int fuelAward = 1;

    public int startingHealth = 5;
    public int startingFuel = 0;
    public int enoughFuel = 10;
    private int currentHealth;
    private int currentFuel;
    private int score = 0;

    public float timeToPlay = 60;

    public float powerUpDuration = 10;
    private float currentPowerUpDuration;

    public Slider healthSlider;
    public Slider fuelSlider;
    public Text scoreText;
    public Text timeText;
    public Text wildfireText;
    private int wildfirePickups;

    public Text fightFireWithFire;
    public GameObject popupScore;
    public GameObject popupDamage;
    private Vector3 offset;

    private Color originalMaterialColor;

    void OnEnable()
    {
        Debug.Log("enabled");
        OnContact.OnPickup += AddScore;
        OnContact.OnPickup += AddFuel;
        OnContact.OnHitByEnemyBullet += TakeDamage;
        PlayerController.OnPowerUp += ActivatePowerUp;
    }

    void OnDisable()
    {
        OnContact.OnPickup -= AddScore;
        OnContact.OnPickup -= AddFuel;
        OnContact.OnHitByEnemyBullet -= TakeDamage;
        PlayerController.OnPowerUp -= ActivatePowerUp;
    }

    void AddScore(string pickuptype, GameObject character)
    {
        if (pickuptype == "wildfire")
        {
            scoreAward = initialScoreAward * 2;
            wildfirePickups++;
            wildfireText.text = wildfirePickups.ToString();
        }
        if (pickuptype == "steam")
        {
            scoreAward = initialScoreAward;
        }
        score += scoreAward;
        scoreText.text = "Score: " + score;

        GameObject tempTextBox = Instantiate(popupScore, character.transform.position + offset, character.transform.rotation);
        TextMesh theText = tempTextBox.transform.GetComponent<TextMesh>();
        theText.text = "+" + scoreAward;
    }

    void AddFuel(string none, GameObject none2)
    {
        currentFuel += fuelAward;
        fuelSlider.value = currentFuel;
    }

    void TakeDamage()
    {
        currentHealth -= bulletDamage;
        healthSlider.value = currentHealth;

        GameObject tempTextBox = Instantiate(popupDamage, player.transform.position + offset, player.transform.rotation);
        TextMesh theText = tempTextBox.transform.GetComponent<TextMesh>();
        theText.text = "-" + bulletDamage;
    }

    public void Init()
    {
        currentHealth = startingHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;

        currentFuel = startingFuel;
        fuelSlider.maxValue = enoughFuel;
        fuelSlider.value = currentFuel;

        currentPowerUpDuration = powerUpDuration;
        originalMaterialColor = player.GetComponent<Renderer>().material.GetColor("_Color");
        player.GetComponent<Transform>().position = player.startingPos;
        sidekick.GetComponent<Transform>().position = sidekick.startingPos;

        gameOver = false;
        score = 0;
        scoreText.text = "Score: " + score;
        timeToPlay = 60;
        timeText.text = "Time: " + (int)timeToPlay;
        gameOverImage.enabled = false;
        scoreAward = initialScoreAward;
        wildfirePickups = 0;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        gameOverImage.enabled = false;
        fightFireWithFire.enabled = false;
        initialScoreAward = scoreAward;
        offset = new Vector3(0, 1, 0);
        Init();
    }
	
	void Update ()
    {
        if (!gameOver)
        {
            timeToPlay -= Time.deltaTime;
            timeText.text = "Time: " + (int)timeToPlay;
        }

        if(timeToPlay <= 0)
        {
            GameOver();
        }

        if(player.state == PlayerController.State.POWEREDUP && player.powerUpActivated)
        {
            currentPowerUpDuration -= Time.deltaTime;
            fuelSlider.value = currentPowerUpDuration;
            if(currentPowerUpDuration <= 0)
            {
                PowerUpReset();
            }
        }

        if (currentFuel >= enoughFuel)
        {
            fightFireWithFire.enabled = true;
            player.state = PlayerController.State.POWEREDUP;
        }

        if (currentHealth <= 0 && !gameOver)
        {
            GameOver();
        }

        if (gameOver)
        {
            if (Input.GetButton("Fire1") && BossMovement.bossReset)
            {
                Init();
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverImage.enabled = true;
        PowerUpReset();
    }

    public void PowerUpReset()
    {
        player.state = PlayerController.State.NORMAL;
        currentPowerUpDuration = powerUpDuration;
        player.GetComponent<Renderer>().material.SetColor("_Color", originalMaterialColor);
        currentFuel = 0;
        fuelSlider.maxValue = enoughFuel;
        fuelSlider.value = currentFuel;
        player.powerUpActivated = false;
        fightFireWithFire.enabled = false;
    }

    public void ActivatePowerUp()
    {
        currentFuel = 0;
        player.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        currentPowerUpDuration = powerUpDuration;
        fuelSlider.maxValue = currentPowerUpDuration;
        fightFireWithFire.enabled = true;
    }
}
