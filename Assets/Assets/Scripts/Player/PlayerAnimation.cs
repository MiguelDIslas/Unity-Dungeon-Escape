using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator _animator;
    private Animator _swordAnimator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _swordAnimator = GameObject.Find("Sword_Arc").GetComponentInChildren<Animator>();
        //_swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float move)
    {
        _animator.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        _animator.SetBool("Jumping", jump);
    }
    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAnimation");
    }

}
