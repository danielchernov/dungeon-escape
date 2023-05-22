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

    public void OpenShop(int gemCount)
    {
        gemCountText.text = gemCount + "G";
    }

    public void UpdateSelection(float yPos)
    {
        selectionFill.rectTransform.anchoredPosition = new Vector3(10, yPos, 0);
    }

    private void Awake()
    {
        _instance = this;
    }
}
