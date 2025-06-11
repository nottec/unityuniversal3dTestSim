using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections;


public class DeathScreenController : MonoBehaviour
{
    public VisualElement ui;
    public Button mainmenuButton;
    public Button respawnButton;
    public Button quitButton;
    public GameObject MainMenu;
    private CustomSpawnManager customSpawnManager;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        // Find the CustomSpawnManager in the scene (if not set manually)
        customSpawnManager = Object.FindFirstObjectByType<CustomSpawnManager>();

        if (customSpawnManager == null)
        {
            Debug.LogError("CustomSpawnManager not found in the scene!");
        }
    }

    private void OnEnable()
    {
        if (ui == null) return;

        mainmenuButton = ui.Q<Button>("MainMenuButton");
        respawnButton = ui.Q<Button>("RespawnButton");
        quitButton = ui.Q<Button>("QuitButton");

        if (mainmenuButton != null)
            mainmenuButton.clicked += OnMainMenuButtonClicked;
        else
            Debug.LogError("MainMenuButton not found!");
/*
        if (respawnButton != null)
            respawnButton.clicked += OnRespawnButtonClicked;
        else
            Debug.LogError("RespawnButton not found!");
*/
        if (quitButton != null)
            quitButton.clicked += OnQuitButtonClicked;
        else
            Debug.LogError("QuitButton not found!");

        ui.style.display = DisplayStyle.None;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();  // Quit the application
    }
/*
    private void OnRespawnButtonClicked()
    {
        Debug.Log("Respawn button pressed");
        ui.style.display = DisplayStyle.None;  // Hide the death screen UI
        if (customSpawnManager != null)
        {
            customSpawnManager.SpawnPlayer();  // Respawn player using the custom spawn manager
        }
        else
        {
            Debug.LogError("CustomSpawnManager is not assigned!");
        }
    }
    
*/
    /*private void OnMainMenuButtonClicked()
    {
        NetworkManager.Singleton.Shutdown();
        ui.style.display = DisplayStyle.None;  // Hide the death screen UI
        string sceneToLoad = "Menu";
        NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single);
        if (MainMenu != null)
        {
            MainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.LogError("MainMenu GameObject is not assigned!");
        }
    }*/

    public void OnMainMenuButtonClicked()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.Shutdown();
        }

        StartCoroutine(LoadMainMenuAfterDelay());
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(0.3f); // Let shutdown finish
        SceneManager.LoadScene("Menu");
    }

}
