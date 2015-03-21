using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour{

	[SerializeField]
	private GameObject _ballPrefab;

	[SerializeField]
	private GameObject _spawn;
	
	[SerializeField]
	private bool _isBuildingServer = true;

	void Start (){
		Application.runInBackground = true;
		if (_isBuildingServer){
			Network.InitializeSecurity();
			Network.InitializeServer(1, 9090, true);
		}
		else{
			Network.Connect("127.0.0.1", 9090);
		}
	}

	void OnPlayerConnected(NetworkPlayer player){
		networkView.RPC("StartGame", RPCMode.All);
	}

	[RPC]
	void StartGame(){
		_ballPrefab.gameObject.SetActive(true);
		_spawn.gameObject.SetActive(true);
	}

	void Update (){}

}