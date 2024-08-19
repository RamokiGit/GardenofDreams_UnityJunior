using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointActivate : MonoBehaviour
{
    [SerializeField] private GameObject _handPoint;
    [SerializeField] private GameObject _footPoint;


    void HandAttackOn()
    {
        _handPoint.SetActive(true);
    }

    void FootAttackOn()
    {
        _footPoint.SetActive(true);
    }
    
    void HandAttackOff()
    {
        _handPoint.SetActive(false);
    }

    void FootAttackOff()
    {
        _footPoint.SetActive(false);
    }
}
