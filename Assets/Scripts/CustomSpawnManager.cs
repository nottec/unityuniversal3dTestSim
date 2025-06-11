using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;


public class CustomSpawnManager : NetworkBehaviour
{
    
    public Transform[] spawnPoints;            // Assign in the Inspector
    public GameObject playerPrefab;            // Assign in the Inspector
    public DeathScreenController deathScreenController;  // Assign in the Inspector

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            SpawnPlayer();
        }
    }
private Dictionary<ulong, NetworkObject> spawnedPlayers = new();

    /*public void SpawnPlayer()
    {
        
        if (spawnPoints.Length == 0 || playerPrefab == null) return;

        // pick a spawn point
        var spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // instantiate the player prefab
        GameObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);

        // **grab the Actor on the new player** and set its death-screen reference
        var actor = player.GetComponent<Actor>();
        if (actor != null)
        {
            actor.SetDeathScreenController(deathScreenController);
        }
        else
        {
            Debug.LogError("Spawned prefab has no Actor component!");
        }

        // now spawn it for the network (this also hands ownership to the connecting client)
        var netObj = player.GetComponent<NetworkObject>();
        netObj.SpawnWithOwnership(OwnerClientId);
        
    }*/

    public void SpawnPlayer()
{
    if (!NetworkManager.Singleton.IsServer)
    {
        Debug.LogWarning("Cannot spawn player: NetworkManager is not running as server/host.");
        return;
    }

    if (spawnPoints.Length == 0 || playerPrefab == null)
    {
        Debug.LogError("Spawn points or player prefab not set.");
        return;
    }

    var spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

    GameObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);

    var actor = player.GetComponent<Actor>();
    if (actor != null)
    {
        actor.SetDeathScreenController(deathScreenController);
    }
    else
    {
        Debug.LogError("Spawned prefab has no Actor component!");
    }

    var netObj = player.GetComponent<NetworkObject>();
    if (netObj != null)
    {
        netObj.SpawnWithOwnership(OwnerClientId);
    }
    else
    {
        Debug.LogError("No NetworkObject component found on player prefab.");
    }
}


}

