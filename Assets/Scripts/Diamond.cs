using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int DiamondValue = 1;

    [SerializeField]
    private AudioClip sfxAudio;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Player player = collider.GetComponent<Player>();
            player.AddGems(DiamondValue);

            AudioManager.Instance.PlaySFX(sfxAudio, 0.3f);

            Destroy(gameObject);
        }
    }
}
