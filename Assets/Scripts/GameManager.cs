using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private bool isPause = false;
    private PlayerControlls playerControlls;
    private GameObject player;
    private XpBarScript xpBar;

    public int maxLevel = 30;
    public GameObject pauseMenu;
    public GameObject levelUpMenu;
    public TextMeshProUGUI levelText;
    public static GameManager Instance;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            Pause();
        }else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Unpause();
        }

        UpdateLevelText();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        playerControlls = player.GetComponent<PlayerControlls>();
        xpBar = GameObject.Find("XpBar").GetComponent<XpBarScript>();
        pauseMenu.SetActive(false);
        levelUpMenu.SetActive(false);
    }

    void UpdateLevelText()
    {
        levelText.SetText("Level: " + PlayerControlls.level.ToString());
    }

    public void LevelUp()
    {
        playerControlls.LevelUp();
        xpBar.SetXp(0);
        xpBar.SetMaxXp(100);
        ActivateLevelUpMenu();
    }

    public void ActivateLevelUpMenu()
    {
        levelUpMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public List<object> GetPosibleUpdates()
    {
        List<object> updates = new List<object>();

        

        return updates;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseMenu.SetActive(false);
    }
}
