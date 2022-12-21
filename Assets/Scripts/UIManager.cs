using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject effects;
    public GameObject timeText;
    public string gameState;
    public GameObject roomsClearedText;
    public GameObject gameOverCanvas;
    public TMP_Text gameOverText;
    public GameObject gameplayCanvas;
    [SerializeField] private AudioSource deathSound;
    float timer = 0f;
    public float maxPrepTime;
    public float maxExecutionTime;
    private int roomsCleared = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxPrepTime;
        gameState = "Prep";
        ChangeRoomText();
        effects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "GameOver")
        {
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = gameState + " phase: " + System.Math.Round(timer, 2) + "<br>seconds remaining";
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                switch (gameState)
                {
                    case "Prep":
                        gameState = "Execute";
                        timer = maxExecutionTime;
                        effects.SetActive(true);
                        break;
                    case "Execute":
                        gameState = "Prep";
                        timer = maxPrepTime;
                        effects.SetActive(false);
                        break;

                }
            }
            if (timer <= maxPrepTime / 2 && timer > maxPrepTime * 0.25f && string.Equals(gameState, "Prep") || timer <= maxExecutionTime / 2 && timer > maxPrepTime * 0.25f && string.Equals(gameState, "Execute"))
            {
                timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0);
            }
            else if (timer <= maxPrepTime * 0.25f && string.Equals(gameState, "Prep") || (timer <= maxExecutionTime * 0.25f && string.Equals(gameState, "Execute")))
            {
                timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0, 0);
            }
            else
            {
                timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            }
        }
        
    }
    void ChangeRoomText()
    {
        roomsClearedText.GetComponent<TMPro.TextMeshProUGUI>().text = "Rooms cleared: "+roomsCleared;
    }
    public void RoomCleared()
    {
        roomsCleared++;
        ChangeRoomText();
    }
    public void GameOver()
    {
        gameState = "GameOver";
        deathSound.Play();
        gameplayCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        PlayerCam.inMenu = true;
        PlayerMovement.inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverText.text = "\nGAME OVER\n\n";
        int highscore = 0;
        if (PlayerPrefs.HasKey("highscore"))
            highscore = PlayerPrefs.GetInt("highscore");
        if (roomsCleared > highscore)
        {
            gameOverText.text += $"NEW HIGHSCORE: {roomsCleared}";
            PlayerPrefs.SetInt("highscore", roomsCleared);
        }
        else
            gameOverText.text += $"SCORE: {roomsCleared}\n\n HIGHSCORE: {highscore}";
    }
    public void Reload()
    {
        StartCoroutine("ReloadGame");
    }
    private IEnumerator ReloadGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainGame");
    }
}
