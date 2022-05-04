using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour {

	[SerializeField] protected GameObject model;
	[SerializeField] protected int maxHealth = 5;
	[SerializeField] protected NavMeshAgent navMesh;
	[SerializeField] protected float attackDistance = 2;

	[Header("Prefab")]
	[SerializeField] protected GameObject ammoPrefab;
	[SerializeField] protected GameObject healthPrefab;

	protected Animator anim;

	protected int currentHealth;
	protected float destinationUpdateTimer = .15f;
	protected float lastDestinationUpdate;

	[SerializeField] protected float attackCD = 1.5f;
	protected float lastAttack;
	
	protected Transform target;

	protected virtual void GetModelInfo() { //Instantiate model prefab and updates values
		/*
		 * It's made like this so we can change
		 * the model for any other, making easy
		 * to customize the skeletons to new models
		*/
		if(model == null) { return; }

		GameObject _go = Instantiate(model);
		_go.transform.SetParent(transform);
		_go.transform.localPosition = Vector3.zero;
		_go.transform.localRotation = Quaternion.identity;

		anim = _go.GetComponentInChildren<Animator>();
    }

	protected virtual void OnEnable() {
		GetModelInfo();
		target = FindObjectOfType<FPSMovement>().transform;
		currentHealth = maxHealth;
	}


	protected virtual void Update () {
		Animations(); 
		if(Vector3.Distance(transform.position, target.position) <= attackDistance) {
			Attack(); //if player is in attack range, attack
		} else {
			if(lastDestinationUpdate < Time.time) {
				UpdateDestination(); //Update nav mesh destinations after the cooldown
			}
		}
	}

	protected virtual void UpdateDestination() {
		navMesh.SetDestination(target.position);

		lastDestinationUpdate = Time.time + destinationUpdateTimer;
	}

	protected virtual void Attack() {
		navMesh.ResetPath(); //Stop moving for 1.75 seconds so it can attacks
		lastDestinationUpdate = Time.time + 1.75f;

		anim.SetTrigger("Attack"); //Set animator attack value
	}

	public virtual void TookDamage() {
		/*
		 * Reduces health and stops the nav
		 * if has more than 0 health, execute hit animation
		 * if has less or equal to 0 health, execute Death method
		 */
		currentHealth--;
		navMesh.ResetPath();
		lastDestinationUpdate = Time.time + 1.75f;

		if(currentHealth <= 0) {
			Death();
		} else {
			anim.SetTrigger("Hit");
		};
	}

	protected virtual void Death() {
		/*
		 * Add a delay to destroy the skeleton so the
		 * death animation can play.
		 * 35% of chance of spawn a ammo item on death
		 * 20% of chance of spawn a health item on death
		 */

		Destroy(gameObject, 2f);
		anim.SetTrigger("Death");
		lastDestinationUpdate = Time.time + 5f;

		int _i = Random.Range(0, 100);

		if(_i < 35) {
			Instantiate(ammoPrefab, transform.position, transform.rotation);
        }else if(_i < 55) {
			Instantiate(healthPrefab, transform.position, transform.rotation);
		}
	}

	protected virtual void Animations() {
		anim.SetBool("IsMoving", navMesh.hasPath);
	}
}
