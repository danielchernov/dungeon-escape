using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.transform.position = new Vector3(-16, 0.6f, 0);

            IDamageable hit = collider.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage();
            }
        }
    }
}
