using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;
    public int round;
    [SerializeField] private int pickups;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI roundText;

    void Awake()
    {
        if(PlayerPrefs.HasKey("score"))
        {
            Recover();
        }
        else
        {
            StartNew();
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
        roundText.text = "Round " + round;

        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.LogWarning("PURGING WITH FIRE!!!!!!!!!!!!");
            WipeOut();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            pickups += 1;
            AddScore();
        }

        if (pickups >= 4)
        {
            pickups = 0;
            round += 1;
            Store();
            SceneManager.LoadScene("Game");
        }
    }

    void AddScore()
    {
        score += 100;
    }

    void StartNew()
    {
        if(PlayerPrefs.HasKey("score"))
        {
            WipeOut();
        }
        score = 0;
        round = 1;
        Store();
    }

    void Store()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("round", round);
        PlayerPrefs.Save();
    }

    void WipeOut()
    {
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("round");
    }

    void Recover()
    {
        score = PlayerPrefs.GetInt("score");
        round = PlayerPrefs.GetInt("round");
    }
}
