using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _playerController.TryMoveUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _playerController.TryMoveDown();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _playerController.TryMoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _playerController.TryMoveRigh();
        }
    }
}
