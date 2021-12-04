using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text bombCoutUI;
    [SerializeField] private Enemy _dog;
    [SerializeField] private Enemy _farmer;
    [SerializeField] private GameObject _controller;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _dead;
    [SerializeField] private GameObject _win;

    public int bombCount;

    private void Update()
    {
        bombCoutUI.text = bombCount.ToString();

        if (bombCount < 0)
        {
            GameOver();
        }
        if (_dog.isDirt && _farmer.isDirt)
        {
            YouAreWin();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        _controller.SetActive(false);
        _menu.SetActive(true);
        _dead.SetActive(true);
    }

    public void YouAreWin()
    {
        _controller.SetActive(false);
        _menu.SetActive(true);
        _win.SetActive(true);
        Debug.Log("You Are Win");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
