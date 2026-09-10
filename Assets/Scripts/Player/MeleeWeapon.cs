using UnityEngine;

public class MeleeWeapon : Weapon
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    public override void Attack()
    {
        base.Attack();
        animator.Play("Attack");
    }
}
