using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject effects;
    public GameObject timeText;
    public string gameState;
    public GameObject roomsClearedText;
    float timer = 0f;
    public float maxPrepTime;
    public float maxExecutionTime;
    int roomsCleared = 0;
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
        timeText.GetComponent<TMPro.TextMeshProUGUI>().text = gameState + " phase: " + System.Math.Round(timer, 2) + "<br>seconds remaining";
        timer -= Time.deltaTime;
        if(timer<=0){
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
        if(timer<=maxPrepTime/2 && timer> maxPrepTime * 0.25f && string.Equals(gameState, "Prep") || timer <= maxExecutionTime / 2 && timer > maxPrepTime * 0.25f && string.Equals(gameState, "Execute"))
        {
            timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0);
        }
        else if(timer <= maxPrepTime *0.25f && string.Equals(gameState, "Prep") || (timer <= maxExecutionTime * 0.25f && string.Equals(gameState, "Execute")))
        {
            timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0, 0);
        }
        else
        {
            timeText.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1,1,1);
        }
    }
    void ChangeRoomText()
    {
        roomsClearedText.GetComponent<TMPro.TextMeshProUGUI>().text = "Rooms cleared: "+roomsCleared;
    }
}
