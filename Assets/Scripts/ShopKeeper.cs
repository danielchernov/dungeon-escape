using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopUI;

    [SerializeField]
    private int _selectedItem;

    [SerializeField]
    private AudioClip audioSFX;

    private int _itemPrice;

    private Player _player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _player = collider.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.Diamonds);
            }
            _shopUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _shopUI.SetActive(false);
        }
    }

    public void SelectItem(int selection)
    {
        switch (selection)
        {
            case 0:
                UIManager.Instance.UpdateSelection(100f);
                _itemPrice = 400;
                break;
            case 1:
                UIManager.Instance.UpdateSelection(0);
                _itemPrice = 200;
                break;
            case 2:
                UIManager.Instance.UpdateSelection(-100f);
                _itemPrice = 100;
                break;
            default:
                break;
        }

        _selectedItem = selection;
    }

    public void BuyItem()
    {
        if (_player.Diamonds >= _itemPrice)
        {
            switch (_selectedItem)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    GameManager.Instance.HasKeyToCastle = true;
                    Debug.Log("Key Purchased!");
                    break;
                default:
                    break;
            }

            _player.Diamonds -= _itemPrice;
            UIManager.Instance.UpdateGemCount(_player.Diamonds);
            AudioManager.Instance.PlaySFX(audioSFX, 0.5f);

            _shopUI.SetActive(false);
        }
        else
        {
            _shopUI.SetActive(false);
        }
    }
}
