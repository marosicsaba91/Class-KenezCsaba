using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
	[SerializeField] Button startHostButton;
	[SerializeField] Button startClientButton;
	[SerializeField] Button startServerButton;
	[SerializeField] TMP_Text messageText;

	[SerializeField] NetworkManager networkManager;

	readonly float _networkManager;

	private void Awake()
	{
		startHostButton.onClick.AddListener(StartHost);
		startClientButton.onClick.AddListener(StartClient);
		startServerButton.onClick.AddListener(StartServer);
	}

	void StartHost() => networkManager.StartHost();

	void StartClient() => networkManager.StartClient();

	void StartServer() => networkManager.StartServer();

	public void SendUIMessage(string message) 
	{
		messageText.text = message;
	}
}
