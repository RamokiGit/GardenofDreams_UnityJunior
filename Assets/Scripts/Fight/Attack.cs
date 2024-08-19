using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] LayerMask _collisionMask;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private int _damage = 2;
    [SerializeField] private bool _isPlayer, _isEnemy;
    [SerializeField] private GameObject _hitFX;

    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _collisionMask);
        if (hits.Length > 0)
        {
            if (_isPlayer)
            {
                Vector3 hitFXPos = hits[0].transform.position;
                hitFXPos.y += 1.3f;
                Instantiate(_hitFX, hitFXPos, Quaternion.identity);
            }
            hits[0].GetComponent<Health>().Damage(_damage);
            gameObject.SetActive(false);
        }
    }
}
