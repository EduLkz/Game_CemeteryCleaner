using UnityEngine;

public class DamageCollision : MonoBehaviour {

    protected virtual void OnTriggerEnter(Collider other) {
        

        if(other.tag.Equals("Player")) {
            other.GetComponent<FPSMovement>().TookDamage();
            
        } else if(other.tag.Equals("Enemy")) {
            other.GetComponent<Skeleton>().TookDamage();
        }
    }
}
