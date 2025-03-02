using System;
using Gameplay.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grid = Gameplay.Utils.Grid;

public class GameManager : MonoBehaviour
{
    [SerializeField]TMP_Text _text;

    public Runner _runner;

    void Awake()
    {
        //Time.timeScale = 0.1f;
    }

    void Update()
    {
        //_text.text = "fps: "+(int)(1f / Time.deltaTime);
        _text.text = $"speed: {_runner._moveSpeed:F2}";
        if (Input.GetKeyDown(KeyCode.Space))
            ResetCurrentScene();
    }

    public static void ResetCurrentScene()
    {
        Debug.ClearDeveloperConsole();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}