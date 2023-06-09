using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }

            return _instance;
        }
    }

    public bool HasKeyToCastle { get; set; }
    public Player player { get; private set; }

    private void Awake()
    {
        _instance = this;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void AddGems(int amount)
    {
        if (player != null)
        {
            player.Diamonds += amount;
            UIManager.Instance.UpdateGemCount(player.Diamonds);
        }
    }
}
