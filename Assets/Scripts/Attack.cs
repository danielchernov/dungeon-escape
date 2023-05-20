using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _gotHit = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable hit = collider.GetComponent<IDamageable>();

        if (hit != null && !_gotHit)
        {
            hit.Damage();
            _gotHit = true;
            StartCoroutine(ResetHit());
        }
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f);
        _gotHit = false;
    }
}
