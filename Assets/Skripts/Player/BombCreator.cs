using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCreator : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameController _gameController;

    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
       // if (Input.GetMouseButtonDown(0) && _playerController.playerIsNotMove)
       // {
         //   Instantiate(bomb,transform.position,transform.rotation);
       // }
    }
    public void BombCreate()
    {
        if (_playerController.playerIsNotMove)
        {
            Instantiate(bomb, transform.position, transform.rotation);
            _gameController.bombCount -= 1;
        }
    }
}
