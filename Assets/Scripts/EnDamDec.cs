using UnityEngine;

public class EnDamDec : MonoBehaviour
{
    public int attackDamage;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
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
