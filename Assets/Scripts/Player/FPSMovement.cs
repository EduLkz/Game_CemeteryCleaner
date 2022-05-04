using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inputs;

public class FPSMovement : MonoBehaviour {
	
	[Header("Stats")]
	[SerializeField] private int maxHealth = 10;

	[SerializeField] private Image healthVisualizer;

	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float jumpForce = 8f;

	[SerializeField] private LayerMask groundLayer;


	[Header("Components")]
	[SerializeField] private Rigidbody body;
	[SerializeField] private Animator anim;
	[SerializeField] private Camera cam;
	[SerializeField] private AudioSource audioSource;


	[Header("Mouse")]
	[SerializeField] private float mouseSensitivity = 2.5f;
	[SerializeField] private float mouseSmooth = 2f;

	Vector2 mouseInput;

	public float mouseClampMin = -60f;
	public float mouseClampMax = 80f;
	Vector2 mouseLook;
	Vector2 smoothLook;

	private float currentSpeed;
	private int currentHealth;

	private float i_hor;
	private float i_ver;

	private bool isGrounded;
	private bool jump;
	private bool attacking;
	private bool aiming;
	private float arrowForceMultiplier; //The value of how long the player has been holdin the shot button to charge the arrow

	int attackNum;
	int attackCount;
	float lastAttack;
	float attackCd = 1.2f;

	[Header("Arrow")]
	[SerializeField] private Text ammoText;
	
	[Space(5)]
	[SerializeField] private GameObject arrowPrefab;
	[SerializeField] private Transform arrowShotpoint;
	[SerializeField] private Transform arrowParent;

	Queue<Arrow> arrows = new Queue<Arrow>();

	[SerializeField]private float maxArrowCount = 30;
	private float currentArrowCount;

	[Header("Audio Clips")]
	[SerializeField] private AudioClip shotArrowClip;
	[SerializeField] private AudioClip attackClip;
	[SerializeField] private AudioClip restoreHealthClip;
	[SerializeField] private AudioClip restoreAmmoClip;

	Controls controls;


	private void Start() {
		AddArrow(10); //Instantiate 10 arrows at the start, it increases load scene time but decrease a the number of arrows instantiated in game

		//Initializing values
		currentArrowCount = maxArrowCount;
		currentSpeed = movementSpeed;
		currentHealth = maxHealth;

		//Create a Controls instance if doesn't exist
        if(controls == null) controls = new Controls();

		controls.Enable();

		/*Assingning methods to the input actions created
		 * Performed means that the actions was triggered
		 * Canceled means that the actions stopped
		 */
		controls.Gameplay.Move.performed += ctx => GetMovementInput(ctx.ReadValue<Vector2>());
		controls.Gameplay.Move.canceled += ctx => GetMovementInput(Vector2.zero);

		controls.Gameplay.LookAround.performed += ctx => GetMouseInput(ctx.ReadValue<Vector2>());
		controls.Gameplay.LookAround.canceled += ctx => GetMouseInput(Vector2.zero);

		controls.Gameplay.Shoot.performed += ctx => GetAimingInput(true);
		controls.Gameplay.Shoot.canceled += ctx => GetAimingInput(false);
		controls.Gameplay.Shoot.canceled += ctx => GetShootInput();

		controls.Gameplay.Jump.performed += ctx => GetJumpInput();

		controls.Gameplay.Attack.performed += ctx => GetAttackInput();
	}

    #region Inputs

	//Setting the variable values to the input action values

	private void GetMovementInput(Vector2 _value) {
		i_hor = _value.x;
		i_ver = _value.y;
	}

	private void GetMouseInput(Vector2 _value) => mouseInput = _value;

	private void GetJumpInput() => Jump();

	private void GetShootInput() {
		ShotArrow();
		aiming = false;
	}

	private void GetAttackInput() {
		Attack();
	}

	private void GetAimingInput(bool _value) {
		aiming = _value;
    }
	#endregion

	private void Update() {
		if(GameManager.Instance.IsPaused()) { return; } //If the GameManager instance set the pause value to true, we don't chance any values

		MouseLook(); //Moves the mouse

		if(aiming) {
			arrowForceMultiplier += 1f * Time.deltaTime;
			arrowForceMultiplier = Mathf.Clamp01(arrowForceMultiplier); //Make sure the 'arrowForceMultiplier' doesn't become bigger than 1
		}

		SetAnimations(); //Set Animator values

		if(lastAttack <= Time.time) { //Reset 'attackCount' accordingly with attack cooldown
			if(attackCount >= 2) {
				attackCount = 0;
			}
		}
	}

	private void FixedUpdate() {
		if(GameManager.Instance.IsPaused()) { return; } //If the GameManager instance set the pause value to true, we don't chance any values
		Movement(); //Moves the player
		Grounded(); //Check if player isn't on air
	}

	private void MouseLook() {
		Vector2 _ml = mouseInput; //_ml is a local variable that refers to the mouse look value, but its updated everytime the method is called
		_ml = Vector2.Scale(_ml, Vector2.one * mouseSensitivity); //Multiplies mouse input by the sensitivity

		smoothLook.x = Mathf.Lerp(smoothLook.x, _ml.x, 1f / mouseSmooth); //Set smoothLook to the _ml value
		smoothLook.y = Mathf.Lerp(smoothLook.y, _ml.y, 1f / mouseSmooth);
		mouseLook += smoothLook; //update the mouse Look value to increase by the smoothlook

		mouseLook.y = Mathf.Clamp(mouseLook.y, mouseClampMin, mouseClampMax); //Clamp the verticar look to the min and max values setted

		cam.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); //Updates vertical look
		transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up); //Updates horizontal look by moving the player
	}

	private void Movement() {
		Vector3 _movement = (transform.forward * i_ver + transform.right * i_hor) * currentSpeed; //Multiplies player orientation by input values and then by the player speed
		_movement.y = body.velocity.y; //Set the y value to the rigidbody y values, to make sure we don't mess up the player gravity

		body.velocity = _movement; //updates rigidbody velocity
    }

	private void Jump() {
		if(!isGrounded) { return; } //If the players is not on the ground

		body.velocity = Vector3.zero; //Reset fall speed to make a better jump

		body.AddForce(Vector3.up * jumpForce * body.mass, ForceMode.Impulse); //Impulsionate player upwards
	}

	private void Grounded() {
		bool _isGrounded = false;

		//Making a raycast from player position to the ground to see if player is not in the air
		if(Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer)) {
			//if the raycast hits the ground, the value become true
			_isGrounded = true;
        }

		isGrounded = _isGrounded;
    }

    private void SetAnimations() {
		anim.SetFloat("ArrowForceMultiplier", arrowForceMultiplier);
		anim.SetBool("Move", i_hor != 0 || i_ver != 0);
		anim.SetBool("Aiming", aiming);
		anim.SetBool("Attacking", attacking);
		anim.SetInteger("AtkNum", attackNum);
    }

	#region Arrow shot & pooling

	/*
	 * Here we instantiate the arrows and add to the pool,
	 * if the player tries to shoot, we use an arrow from the pool
	 * if the pool doesn't have an arrow to be shoted, we instantiate a new one
	 * once the arrow hits a target we add back to the pool to be used later
	 */

	private void ShotArrow() {
		if(currentArrowCount <= 0) { return; }

		audioSource.PlayOneShot(shotArrowClip);
			
		Arrow _a = GetArrow();
		_a.transform.position = arrowShotpoint.position;
		_a.transform.rotation = cam.transform.rotation;
		_a.gameObject.SetActive(true);

		_a.ShotArrow(25f * (arrowForceMultiplier * 3f));

		arrowForceMultiplier = 0;

		currentArrowCount--;
		ammoText.text = currentArrowCount.ToString("00");
	}

	Arrow GetArrow() {
		if(arrows.Count == 0) {
			AddArrow(1);
		}
		return arrows.Dequeue();
	}
	void AddArrow(int _count) {
        for(int i = 0; i < _count; i++) {
			Arrow _a = Instantiate(arrowPrefab).GetComponent<Arrow>();
			_a.transform.SetParent(arrowParent);
			_a.gameObject.SetActive(false);
			_a.player = this;
			arrows.Enqueue(_a);
        }
    }

	public void ReturnToPool(Arrow _arrow) {
		_arrow.gameObject.SetActive(false);
		arrows.Enqueue(_arrow);
    }
    #endregion

    private void Attack() {
		//A melee attack with a 3 hit combo, if the player uses the 3 hits we add a cooldown to the attack

		if(attacking && lastAttack >= Time.time) { return; }
		if(attackNum == 0) {
			attackNum = 1;
        } else {
			attackNum = 0;
        }

		audioSource.PlayOneShot(attackClip);
		anim.SetTrigger("Attack");
		attacking = true;
		attackCount++;

		if(attackCount >= 2) {
			lastAttack = Time.time + attackCd;
		}
	}

	public void ResetAttack() {
		attacking = false; //reset attack variable to make sure player can attack later. This method is called in the animation
	}

	public void TookDamage() {
		currentHealth--; //Reduces current health

		Color _c = healthVisualizer.color;
		_c.a = 1f - (float)currentHealth / (float)maxHealth; //change the image overlay representation of health
		healthVisualizer.color = _c;

		if(currentHealth <= 0)
			this.enabled = false; //If player dies, the script is disabled
    }

	private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Ammo")) { //If player collect "Ammo", destroy ammo object and refil player ammo
			currentArrowCount = maxArrowCount;

			Destroy(other.gameObject);

			audioSource.PlayOneShot(restoreAmmoClip);
			ammoText.text = currentArrowCount.ToString("00");
		}
		if(other.tag.Equals("Health")) { // If player collect "Health", destroy health object and heals player
			currentHealth = maxHealth;

			Color _c = healthVisualizer.color;
			_c.a = 1f - (float)currentHealth / (float)maxHealth;
			healthVisualizer.color = _c;

			Destroy(other.gameObject);
			audioSource.PlayOneShot(restoreHealthClip);
		}
	}
}

