using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && GameManager.Instance.HasKeyToCastle)
        {
            GameManager.Instance.WinGame();
        }
    }
}
