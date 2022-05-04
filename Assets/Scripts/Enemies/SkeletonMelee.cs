using UnityEngine;

public class SkeletonMelee : Skeleton {

    [Header("Melee")]
    [SerializeField] protected GameObject attackCollider;
    protected float attackColliderRestTimer;

    [SerializeField] protected float blockDuration = 2.5f; //The duration the skeleton will hold the shield. If can't block, set to 0
    protected float blockTimer;
    protected bool isBlocking;

    protected override void Update() {
        base.Update();

        if(attackColliderRestTimer + .1f < Time.time)
            attackCollider.SetActive(false);

        if(blockTimer < Time.time) {
            isBlocking = false;
        }

        Quaternion _rot = Quaternion.LookRotation(target.position - transform.position);

        transform.rotation = Quaternion.Euler(0, _rot.eulerAngles.y, 0);
    }

    protected override void Animations() {
        base.Animations();
        anim.SetBool("IsBlocking", isBlocking);
    }

    protected override void UpdateDestination() {
        navMesh.ResetPath();
        if(isBlocking || currentHealth <= 0) { return; }
        base.UpdateDestination();
    }
    public override void TookDamage() {
        if(isBlocking) { //If is blocking, the skeleton doens't take damage
            anim.SetTrigger("Hit");
            isBlocking = false;
            lastDestinationUpdate = Time.time + 1.25f;
            return;
        }

        base.TookDamage();

        int _blockChance = Random.Range(0, 4); // if the player take damage, has a 25% chance to start blocking
        if(_blockChance == 1) {
            isBlocking = true;
            blockTimer = Time.time + blockDuration;
        }
    }

    protected override void Death() {
        base.Death();
        GameManager.Instance.MeleeDied();
    }

    protected override void Attack() {
        base.Attack();
        Hit();
    }

    public void Hit() {
        attackColliderRestTimer = Time.time;
        attackCollider.SetActive(true);
    }
   
}
