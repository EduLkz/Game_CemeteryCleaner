using UnityEngine;

public class Arrow : DamageCollision {

    [SerializeField] private Rigidbody body;
    public FPSMovement player;

    private float startTime;
    private float lifeDuration = 5;

    private void OnEnable() {
        startTime = Time.time; //Set start time to the moment this object became active
    }

    public void ShotArrow(float force) {
        body.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void Update() {
        //Update rotation accordingly the arrow speed
        transform.forward = Vector3.Slerp(transform.forward, body.velocity.normalized, Time.deltaTime * 1.5f); 

        if(startTime + lifeDuration < Time.time) {
            DisableArrow(); //If the arrow is active for more time than its 'lifeDuration' disables it
        }

    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other); //if arrows hits target
        DisableArrow(); //disables arrow if hit something
    }

    void DisableArrow() {
        //Reset arrows velocities
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        if(player) //If player is not null, return to his arrow pool
            player.ReturnToPool(this);
        else // if player is null, return arrow to gamemanager pool
            GameManager.Instance.ReturnToPool(this);
    }
}