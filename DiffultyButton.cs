using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffultyButton : MonoBehaviour
{
    private Button difficultyButton;
    private GameManager gameManager;

    [SerializeField] private int difficulty;
    void Start()
    {
        difficultyButton = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        difficultyButton.onClick.AddListener(SetDifficulty);

    }
    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
