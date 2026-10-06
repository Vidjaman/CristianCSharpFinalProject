using UnityEngine;

public class MeleeWeapon : Weapon
{
    Animator animator;
    SpriteRenderer sr;
    [SerializeField] AudioClip slashClip;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if(sr.color==Color.white) sr.color = Random.ColorHSV(0,1,1,1,1,1);

    }
    public override void Attack()
    {
        //AudioManager.Instance.PlayClip(slashClip, transform.position,0.2f,Random.Range(0.9f,1.1f));
        base.Attack();
        animator.Play("Attack");
    }
}
