using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkScript : MonoBehaviour{

	[SerializeField]
	private List<GameObject> _spawn;
	
	[SerializeField]
	private bool _isBuildingServer = true;

	[SerializeField]
	private List<GameObject> players;

	[SerializeField]
	Camera server_cam;
	
	[SerializeField]
	Camera minimap;
	
	private int joueur_connected=0;

	void Start (){
		//le serveur reste allumé si on est plus dessus
		Application.runInBackground = true;
		if (_isBuildingServer){
			server_cam.gameObject.SetActive(false);
			minimap.rect = new Rect (0f,0f,1f,0.85f) ;
			Network.InitializeSecurity();
			Network.InitializeServer(2, 9090, true);
		}
		else{
			Network.Connect("127.0.0.1", 9090);
		}
	}

	void OnPlayerConnected(NetworkPlayer player){
		//on active le "player" pour les joueurs qui se connecte et on lance le jeu
		foreach (var p in players)
		{
			if (!p.activeSelf){
				networkView.RPC("ActivePlayer",RPCMode.Others,0);
				joueur_connected++;
				break;
			}
		}
		//if(joueur_connected==2)
			networkView.RPC("StartGame", RPCMode.All);
	}

	/*void OnConnectedToServer(){
		foreach (var p in players)
		{
			if (!p.activeSelf){
				networkView.RPC("ActivePlayer",RPCMode.Others,0);
				joueur_connected++;
				break;
			}
		}
	}*/

	[RPC]
	void StartGame(){
		//on active tout les spawns
		foreach (GameObject generateur in _spawn)
			generateur.SetActive(true);
	}

	[RPC]
	void ActivePlayer(int nb){
		players[nb].SetActive(true);

	}

}