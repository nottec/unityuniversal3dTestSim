using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using Unity.VisualScripting;

public class HostServerController : MonoBehaviour
{
    public VisualElement ui;
    public Button hostButton;
    public Button clientButton;
    public Button backButton;

    public GameObject MainMenu;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;

        if (ui == null)
        {
            Debug.LogError("UIDocument is not properly initialized. Please check your UIDocument setup.");
        }
        else
        {
            Debug.Log("Host UIDocument loaded successfully.");
        }
    }

    private void OnEnable()
    {
        if (ui == null) return;

        hostButton = ui.Q<Button>("HostButton");
        clientButton = ui.Q<Button>("ClientButton");
        backButton = ui.Q<Button>("BackButton");

        if (hostButton != null)
            hostButton.clicked += OnHostButtonClicked;
        else
            Debug.LogError("HostButton not found!");

        if (clientButton != null)
            clientButton.clicked += OnClientButtonClicked;
        else
            Debug.LogError("ClientButton not found!");

        if (backButton != null)
        {
            backButton.clicked += OnBackButtonClicked;
            Debug.Log("backbuttonaintnull");
        }   
        else
            Debug.LogError("BackButton not found!");
            
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Back button pressed");  // Show the main menu
        ui.style.display = DisplayStyle.None;
        MainMenu.GetComponent<UIDocument>().rootVisualElement.style.display = DisplayStyle.Flex;   // Hide the current UI elements
    }

    private void OnClientButtonClicked()
    {
        Debug.Log("Client button clicked.");
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().ConnectionData.Address = "127.0.0.1"; // or use another IP if necessary

        ui.style.display = DisplayStyle.None;  // Hide host server UI

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartClient();  // Start the client
            Debug.Log("Trying to connect to host...");
        }
        else
        {
            Debug.LogError("No NetworkManager found!");
        }
    }

    private void OnHostButtonClicked()
    {
        ui.style.display = DisplayStyle.None;  // Hide the host UI

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.StartHost();  // Start the host
            Debug.Log("Hosting started!");
        }
        else
        {
            Debug.LogError("No NetworkManager found!");
        }
    }
}
