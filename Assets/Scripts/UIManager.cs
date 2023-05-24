using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is Null");
            }

            return _instance;
        }
    }

    public Text gemCountText;
    public Image selectionFill;
    public Image[] healthUnits;
    public Text gemCountHUD;

    public Text[] itemNames;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        gemCountText.text = gemCount + "G";
    }

    public void UpdateSelection(float yPos)
    {
        selectionFill.rectTransform.anchoredPosition = new Vector3(10, yPos, 0);
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountHUD.text = gemCount + "G";
    }

    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i < livesRemaining; i++)
        {
            healthUnits[i].gameObject.SetActive(true);
        }

        if (livesRemaining < 4)
        {
            healthUnits[livesRemaining].gameObject.SetActive(false);
        }
    }

    public void UpdateNames(int itemNumber)
    {
        itemNames[itemNumber].text = "Sold out!";
    }
}
