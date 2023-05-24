using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject _winLevelScreen;

    [SerializeField]
    private GameObject _needKey;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && GameManager.Instance.HasKeyToCastle)
        {
            collider.gameObject.SetActive(false);
            _winLevelScreen.SetActive(true);

            Debug.Log("You Win!");
        }
        if (collider.tag == "Player" && !GameManager.Instance.HasKeyToCastle)
        {
            _needKey.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !GameManager.Instance.HasKeyToCastle)
        {
            _needKey.SetActive(false);
        }
    }
}
