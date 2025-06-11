using UnityEngine;
public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    public GameObject hitEffect;
    public int attackDamage;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hittable" && wc.IsAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            GameObject GO = Instantiate(hitEffect, new Vector3(other.transform.position.x,transform.position.y,transform.position.z),other.transform.rotation);
            Destroy(GO, 20);


            Actor enemyActor = other.GetComponent<Actor>();
            if (enemyActor != null)
            {
                Debug.Log("Actor component found on enemy! Applying damage.");
                enemyActor.TakeDamage(attackDamage); // Subtract health from the enemy
            }
            else
            {
                Debug.LogWarning("No Actor component found on " + other.name + " - damage not applied.");
            }
        } 
    }
}
