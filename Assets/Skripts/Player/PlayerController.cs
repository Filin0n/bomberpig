using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _colisionRadius;
    [SerializeField] private float _stepSizeX;
    [SerializeField] private float _stepSizeZ;
    [SerializeField] private SpriteRenderer[] _sprites;

    private Vector3 _targetPosition;

    [HideInInspector] public bool playerIsNotMove;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        playerIsNotMove = transform.position == _targetPosition;

        if (!playerIsNotMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,_targetPosition,_speedMove*Time.deltaTime);
        }
    }

    public void TryMoveUp()
    {
        HideSprite();
        _sprites[3].enabled = true;

        if (!IsObstacleAhead(Vector3.forward, _stepSizeX))
        {
            SetNextPosition(0, _stepSizeZ);
        }
    }
    public void TryMoveDown()
    {
        HideSprite();
        _sprites[2].enabled = true;

        if (!IsObstacleAhead(Vector3.back, _stepSizeX))
        {
            SetNextPosition(0, -_stepSizeZ);
        }
    }
    public void TryMoveRigh()
    {
       HideSprite();
       _sprites[0].enabled = true;

        if (!IsObstacleAhead(Vector3.right, _stepSizeX))
       {
        SetNextPosition(_stepSizeX, 0);
       }
    }
    public void TryMoveLeft()
    {
        HideSprite();
        _sprites[1].enabled = true;

        if (!IsObstacleAhead(Vector3.left, _stepSizeX))
        {
            SetNextPosition(-_stepSizeX, 0);
        }
    }

    private void SetNextPosition(float stepSizeX, float stepSizeZ)
    {
        if (playerIsNotMove)
        {
            _targetPosition = new Vector3(_targetPosition.x + stepSizeX, _targetPosition.y, _targetPosition.z + stepSizeZ);
        }
    }

    private void HideSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            _sprites[i].enabled = false;
        }
    }

    private bool IsObstacleAhead(Vector3 direction, float minDistanceToObstacle)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction * minDistanceToObstacle);

        Physics.Raycast(ray, out hit,minDistanceToObstacle);

        Debug.DrawRay(transform.position, direction,Color.red);

        Wall wall = hit.transform?.GetComponent<Wall>();

        if (wall == null)
        {   
            return false;
        }
        else
        {
            return true;
        }
    }
}
