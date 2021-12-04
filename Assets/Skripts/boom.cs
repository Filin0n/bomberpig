using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        StartCoroutine(DestroyBoom());
    }

    private IEnumerator DestroyBoom()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
