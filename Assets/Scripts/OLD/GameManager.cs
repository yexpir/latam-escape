using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _fps;
    [SerializeField] Character _character;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ResetCurrentScene();
        //_fps.text = (1 / Time.deltaTime).ToString();
        _fps.text = _character.travelSpeed.ToString();
    }


    public static void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
