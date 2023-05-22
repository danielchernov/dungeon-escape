using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int DiamondValue = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Player>().Diamonds += DiamondValue;
            Destroy(gameObject);
        }
    }
}
