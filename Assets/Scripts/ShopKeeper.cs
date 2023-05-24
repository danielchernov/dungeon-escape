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
    private AudioClip[] audioSFX;

    private int _itemPrice;

    private Player _player;

    private bool _boughtBoots = false;
    private bool _boughtKeys = false;

    void Start()
    {
        _shopUI.SetActive(false);
    }

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
                _itemPrice = 100;
                break;
            case 1:
                UIManager.Instance.UpdateSelection(0);
                _itemPrice = 200;
                break;
            case 2:
                UIManager.Instance.UpdateSelection(-100f);
                _itemPrice = 300;
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
                    if (_player.Health < 4)
                    {
                        _player.Health = 4;
                        UIManager.Instance.UpdateLives(_player.Health);

                        _player.Diamonds -= _itemPrice;
                        UIManager.Instance.UpdateGemCount(_player.Diamonds);
                        AudioManager.Instance.PlaySFX(audioSFX[1], 0.5f);

                        Debug.Log("Health Potion Purchased!");
                    }
                    break;
                case 1:
                    if (!_boughtBoots)
                    {
                        _player.jumpForce = 10f;
                        _player.playerSpeed = 1200f;
                        UIManager.Instance.UpdateNames(0);
                        _boughtBoots = true;

                        _player.Diamonds -= _itemPrice;
                        UIManager.Instance.UpdateGemCount(_player.Diamonds);
                        AudioManager.Instance.PlaySFX(audioSFX[0], 0.5f);

                        Debug.Log("Boots Purchased!");
                    }
                    break;
                case 2:
                    if (!_boughtKeys)
                    {
                        GameManager.Instance.HasKeyToCastle = true;

                        UIManager.Instance.UpdateNames(1);
                        _boughtKeys = true;

                        _player.Diamonds -= _itemPrice;
                        UIManager.Instance.UpdateGemCount(_player.Diamonds);
                        AudioManager.Instance.PlaySFX(audioSFX[0], 0.5f);
                        Debug.Log("Key Purchased!");
                    }
                    break;
                default:
                    break;
            }

            _shopUI.SetActive(false);
        }
        else
        {
            _shopUI.SetActive(false);
        }
    }
}
