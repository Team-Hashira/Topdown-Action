using System.Collections;
using System.Collections.Generic;
using Crogen.CrogenPooling;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EffectPoolType _attackEffect;
    [SerializeField] private Transform _attackEffectOffsetTrm;
    public void Attack(Vector2 dir)
    {
        StartCoroutine(CoroutineAttack(dir));
    }

    private IEnumerator CoroutineAttack(Vector2 dir)
    {
        print("공격햇엉");
        gameObject.Pop(_attackEffect, _attackEffectOffsetTrm.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
    }
}
