using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Transform _player;
    private void Start()
    {
        _player = FindObjectOfType<PlayerInput>().transform;
    }

    private void Update()
    {
        if (_spriteRenderer != null) {
            if (_player.position.z > transform.position.z)
            {
                //_spriteRenderer.sortingOrder = 3;
            }
            else
            {
                //_spriteRenderer.sortingOrder = 1;
            }
        }
    }
}
