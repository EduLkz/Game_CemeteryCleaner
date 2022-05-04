using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRanged : Skeleton {

	[Header("Ranged")]
	[SerializeField] private float moveStartCd = 3f;
	public GameObject projectilePrefab;
	[SerializeField] private float projectileForce = 15f;
	private Transform point;

	protected override void OnEnable() {
		base.OnEnable();

		UpdateDestination();
	}

    protected override void Update() {
		Animations();
		if(Vector3.Distance(transform.position, target.position) <= attackDistance) {
			Attack();
		}

        if(navMesh.hasPath) { return; } //If the Skeleton is not in the fixed location, don't look at the player

		/*
		 * Make sure the ranged skeleton is always looking
		 * towards the player, even when doesn't have a nav mesh path
		 */

		Quaternion _rot = Quaternion.LookRotation(target.position - transform.position);

		transform.rotation = Quaternion.Euler(0, _rot.eulerAngles.y, 0);
    }

    protected override void UpdateDestination() {
		point = GameManager.Instance.GetRangedPoint(); //Ranged skeletons have fixed positions where they shot the player
		navMesh.SetDestination(point.position);
	}

    protected override void Attack() {
		if(lastAttack > Time.time) { return; }
		float _arrowForceMultiplier = Random.Range(10, 110) / 10f;

		Quaternion _rot = Quaternion.LookRotation(target.position - (transform.position + Vector3.up));

		Arrow _a = GameManager.Instance.GetProjectile(); //Get an arrow from the gamemanager pool
		_a.transform.position = transform.position + Vector3.up;
		_a.transform.rotation = _rot;

		_a.ShotArrow(projectileForce * (_arrowForceMultiplier * 3f));

		base.Attack();

		lastAttack = Time.time + attackCD;
    }

	protected override void Death() {
		base.Death();
		GameManager.Instance.RangedDied(point); //return the fixed point to the gamemanager so another skeleton can take it's place
	}
}
