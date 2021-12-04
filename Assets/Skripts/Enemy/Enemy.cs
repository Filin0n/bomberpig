using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private bool _wayIsLoop;
    [SerializeField] private float _minDistanceToPoint;
    [SerializeField] private Transform _ollSprites;
    [SerializeField] private SpriteRenderer[] _sprites;
    [SerializeField] private SpriteRenderer[] _sprites2;
    [SerializeField] private SpriteRenderer[] _sprites3;
    [SerializeField] private Transform[] _targetPoints;

    [SerializeField] private bool playerIsTarget;
    public bool isDirt = false;

    private Transform player;
    private NavMeshAgent _navMeshAgent;
    private int _numberOfPoint = 0;
    private bool _iSgoingBack = false;
    private Vector3 _pastPosition;

    private float _directionX;
    private float _directionZ;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pastPosition = transform.position;
    }

    private void Update()
    {
        if (!playerIsTarget)
        {
            ChangeActivePoint();
            _navMeshAgent.SetDestination(_targetPoints[_numberOfPoint].position);
        }
        else
        {
            _navMeshAgent.SetDestination(player.position);
            float distToPlayer = Vector3.Distance(transform.position,player.position);
            if (distToPlayer< 1)
            {
                _gameController.GameOver();
            }
        }

     
        if (isDirt){
            SpriteRenderControl(_sprites2);
            playerIsTarget = false;
        }
        
        if(playerIsTarget)
        {
            SpriteRenderControl(_sprites3);
        }
        else
        {
            SpriteRenderControl(_sprites);
        }

        PlayerDetection();

        _ollSprites.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void ChangeActivePoint()
    {
        float currentPointDistsnce = Vector3.Distance(transform.position, _targetPoints[_numberOfPoint].position);

        if (currentPointDistsnce < _minDistanceToPoint)
        {
            if (_wayIsLoop)
            {
                _numberOfPoint += 1;

                if (_numberOfPoint >= _targetPoints.Length)
                {
                    _numberOfPoint = 0;
                }
            }
            else
            {
                if (_numberOfPoint + 1 >= _targetPoints.Length)
                {
                    _iSgoingBack = true;
                }
                else if (_numberOfPoint - 1 < 0)
                {
                    _iSgoingBack = false;
                }

                if (_iSgoingBack)
                {
                    _numberOfPoint -= 1;
                }
                else
                {
                    _numberOfPoint += 1;
                }
            }
        }
    }

    private void SpriteRenderControl(SpriteRenderer[] sprites)
    {
        float directionZ = _pastPosition.z - transform.position.z;
        float directionX = _pastPosition.x - transform.position.x;

         _directionZ = _pastPosition.z - transform.position.z;
         _directionX = _pastPosition.x - transform.position.x;


        if (Math.Abs(directionZ) > Math.Abs(directionX))
        {
            if (directionZ > 0)
            {
                HideSprite();
                sprites[2].enabled = true;
            }
            if (directionZ < 0)
            {
                HideSprite();
                sprites[3].enabled = true;
            }
        }
        else
        {
            if (directionX > 0)
            {
                HideSprite();
                sprites[1].enabled = true;
            }
            if (directionX < 0)
            {
                HideSprite();
                sprites[0].enabled = true;
            }
        }
        _pastPosition = transform.position;
    }

    private void HideSprite()
    {
        for (int i = 0; i < 4; i++)
        {
            _sprites[i].enabled = false;
            _sprites2[i].enabled = false;
            _sprites3[i].enabled = false;
        }
    }

    private void PlayerDetection()
    {
        Vector3 direction;

        if (Math.Abs(_directionZ) < Math.Abs(_directionX))
        {
            direction = new Vector3(-_directionX, 0, 0).normalized;
        }
        else
        {
            direction = new Vector3(0, 0,-_directionZ).normalized;

        }
        Debug.DrawRay(transform.position, direction * 20, Color.red);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        Physics.Raycast(ray, out hit, 20);

        PlayerController player = hit.transform?.GetComponent<PlayerController>();

        if (player != null)
        {
            playerIsTarget = true;
        }
    }
}
