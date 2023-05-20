using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * 3 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable hit = collider.GetComponent<IDamageable>();

        if (hit != null)
        {
            hit.Damage();
            Destroy(gameObject);
        }
    }
}
