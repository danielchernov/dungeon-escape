using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;
    private Animator _swordArcAnimator;

    [SerializeField]
    private GameObject _gameOverScreen;

    void Start()
    {
        _playerAnimator = transform.GetChild(0).GetComponent<Animator>();
        _swordArcAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float horizontalMovement)
    {
        _playerAnimator.SetFloat("isMoving", Mathf.Abs(horizontalMovement));
    }

    public void Jump(bool jumpingValue)
    {
        _playerAnimator.SetBool("isJumping", jumpingValue);
    }

    public void Attack()
    {
        _playerAnimator.SetTrigger("Attack");
        _swordArcAnimator.SetTrigger("SwordArc");
    }

    public void Hit()
    {
        _playerAnimator.SetTrigger("Hit");
    }

    public IEnumerator Dead()
    {
        _playerAnimator.SetTrigger("Dead");
        Debug.Log("Player Died!");

        yield return new WaitForSeconds(2);

        _gameOverScreen.SetActive(true);
    }
}
