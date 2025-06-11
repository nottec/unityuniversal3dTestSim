using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public VisualElement ui;
    public Button traininggroundsButton;
    public Button multiplayerButton;
    public Button quitButton;
    public GameObject HostServerUI;
    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        if (ui == null)
        {
            Debug.LogError("UIDocument not properly loaded.");
        }
        else
        {
            Debug.Log("Main menu UI Document loaded.");
        }
    }

    private void OnEnable()
    {
        if (ui == null) return;

        traininggroundsButton = ui.Q<Button>("TrainingGroundsButton");
        multiplayerButton = ui.Q<Button>("MultiplayerButton");
        quitButton = ui.Q<Button>("QuitButton");

        if (traininggroundsButton != null)
            traininggroundsButton.clicked += OnTrainingGroundsButtonClicked;
        else
            Debug.LogError("TrainingGroundsButton not found!");
/*
        if (multiplayerButton != null)
            multiplayerButton.clicked += OnMultiplayerButtonClicked;
        else
            Debug.LogError("MultiplayerButton not found!");
*/
        if (quitButton != null)
            quitButton.clicked += OnQuitButtonClicked;
        else
            Debug.LogError("QuitButton not found!");
     ui.style.display = DisplayStyle.Flex;
     HostServerUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();  // Quit the application
    }

    private void OnMultiplayerButtonClicked()
    {
        Debug.Log("Multiplayer button pressed");
        ui.style.display = DisplayStyle.None;  // Hide the main menu UI
        HostServerUI.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;
    }
    

    private void OnTrainingGroundsButtonClicked()
     {
          Debug.Log("Starting host for training grounds...");
          if (NetworkManager.Singleton != null)
          {
               string sceneToLoad = "TrainingGrounds";
               NetworkManager.Singleton.StartHost();
               Debug.Log("Host started.");
               NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single);
               ui.style.display = DisplayStyle.None;
          }
          else
          {
               Debug.LogError("NetworkManager is not available.");
          }
     }

}
