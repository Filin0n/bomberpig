using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField] private float _bombLifeTime;
    [SerializeField] private GameObject _boom;

    [SerializeField] private bool _up;
    [SerializeField] private bool _down;
    [SerializeField] private bool _right;
    [SerializeField] private bool _left;

    [SerializeField] private int _boomRangeMax;
    [SerializeField] private int _boomRangeMin;



    private void Start()
    {
        StartCoroutine(Explosion());
        _up = SideCheck(Vector3.forward, 1f);
        _down = SideCheck(Vector3.back, 1f);
        _right = SideCheck(Vector3.right, 1f);
        _left = SideCheck(Vector3.left, 1f);
    }
    private void Update()
    {
        DebugCust();
    }
        

    private void DebugCust()
    { //стороны атаки
        if (_up && _down)
        {
            Debug.DrawRay(transform.position, Vector3.right * _boomRangeMax, Color.red);
            Debug.DrawRay(transform.position, Vector3.left * _boomRangeMax, Color.red);
        }
        else if (_right && _left)
        {
            Debug.DrawRay(transform.position, Vector3.forward * _boomRangeMax, Color.red);
            Debug.DrawRay(transform.position, Vector3.back * _boomRangeMax, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * _boomRangeMin, Color.red);
            Debug.DrawRay(transform.position, Vector3.left * _boomRangeMin, Color.red);
            Debug.DrawRay(transform.position, Vector3.forward * _boomRangeMin, Color.red);
            Debug.DrawRay(transform.position, Vector3.back * _boomRangeMin, Color.red);
        }
    }


    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(_bombLifeTime);

        Instantiate(_boom, transform.position, transform.rotation);

        if (_up && _down)
        {
            BombRaycast(Vector3.right, _boomRangeMax);
            BombRaycast(Vector3.left, _boomRangeMax);
        }
        else if (_right && _left)
        {
            BombRaycast(Vector3.forward, _boomRangeMax);
            BombRaycast(Vector3.back, _boomRangeMax);
        }

        else
        {
            BombRaycast(Vector3.right, _boomRangeMin);
            BombRaycast(Vector3.left, _boomRangeMin);
            BombRaycast(Vector3.forward, _boomRangeMin);
            BombRaycast(Vector3.back, _boomRangeMin);
        }
        Destroy(gameObject);
    }

    void BooomEffect(Vector3 direction,int count)
    {
        for (int i = 1; i < count+1; i++)
        {
          Transform boom = Instantiate(_boom).transform;
          boom.position = transform.position + direction *i;
        }
    }

    private bool SideCheck(Vector3 direction, float distance)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        Physics.Raycast(ray, out hit, distance);
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

    private void BombRaycast(Vector3 direction, int distance)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);

        Physics.Raycast(ray, out hit, distance);

        Enemy enemy = hit.transform?.GetComponent<Enemy>();

        if (enemy != null)
        { 
            enemy.isDirt = true;
        }
        BooomEffect(direction, distance);
    }
}
