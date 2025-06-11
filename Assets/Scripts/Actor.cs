using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Actor : NetworkBehaviour
{
    [SerializeField] private FloatingHealthBar _healthbar;
    public int currentHealth;
    public int maxHealth;

    private DeathScreenController deathScreenController;

    void Start()
    {
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        Debug.Log("damage taken");
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void UnlockCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
            UnityEngine.Cursor.visible = true;  // Make the cursor visible
        }

    void Death()
    {
        // Start the coroutine to delay destruction
        StartCoroutine(DelayedDestruction());
    }

    IEnumerator DelayedDestruction()
    {
        // Wait for 1 second before destroying the object
        yield return new WaitForSeconds(1f);

        // Destroy the GameObject after the delay
        
        

        // Load the TrainingGrounds scene only for players who die
        if (IsOwner)  // Check if the player is the owner of the object
        {
            UnlockCursor();
            deathScreenController.ui.style.display = DisplayStyle.Flex;
            NetworkObject.Despawn(true);
            NetworkManager.Singleton.Shutdown();

        }
        Destroy(gameObject);
    }

    // This method can be used to set the deathScreenController from another script
    public void SetDeathScreenController(DeathScreenController controller)
    {
        deathScreenController = controller;
    }
    
}
