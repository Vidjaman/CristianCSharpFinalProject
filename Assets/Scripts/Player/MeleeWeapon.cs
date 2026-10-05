using UnityEngine;

public class MeleeWeapon : Weapon
{
    Animator animator;
    SpriteRenderer sr;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if(sr.color==Color.white) sr.color = Random.ColorHSV(0,1,1,1,1,1);

    }
    public override void Attack()
    {
        base.Attack();
        animator.Play("Attack");
    }
}
