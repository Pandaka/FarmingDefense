using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerCreation : MonoBehaviour {

	[SerializeField]
	List<Transform> tour;
	[SerializeField]
	GameObject player;
	[SerializeField]
	Camera player_cam;
	[SerializeField]
	Transform map;

	Ray ray;
	RaycastHit hit;

	private  bool impossibleThere;
	private PlayerScript player_script;

	void Start () 
	{
		player_script=player.GetComponent<PlayerScript>();
	}

	void Update () {
		//si on fait clic gauche
		if (Input.GetMouseButtonDown(0)) {
			
			//on capture la position du clic sur la map
			ray = player_cam.ScreenPointToRay (Input.mousePosition);

			
			//vérification qu'on touche la map
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == map.gameObject) {		

				//on arrange x et z pour une postion plus propre des tours
				float x = Mathf.Round(hit.point.x);
				float z = Mathf.Round(hit.point.z);

				//on met les coordées dans un vecteur 3
				Vector3 tmp = new Vector3 (x, 1.4f, z);
				//envoie du rpc pour créer la tour
				networkView.RPC("PlaceTower", RPCMode.Server, tmp);
			}
		}			
	}

	[RPC]
	void PlaceTower(Vector3 pos)
	{
		//si le joueur n'a pas atteind le nombre de tour max
		if(player_script.cmp_tower()>0)
		{
			// on détecte les obstacles autour de la position ou on veut placer la tour
			var collidedObs = Physics.OverlapSphere(pos, 1f, 1 << LayerMask.NameToLayer("obstacle"));

			if (collidedObs == null || collidedObs.Length == 0) {
				//si la tour est dans la bonne partie du terrain et qu'on a assez de billet pour la créer
				if(pos.x > 0 && player_script.combien_billet()>=100){
					networkView.RPC("CreateTower",RPCMode.All,pos);
				}
			}
		}
	}

	[RPC]
	void CreateTower (Vector3 pos) {

		foreach(Transform go in tour)
		{
			if(!go.gameObject.activeSelf)
			{
				go.position = pos;
				StatTower tower = go.gameObject.GetComponent<StatTower>();
				tower.lifeBarInitiate();
				go.gameObject.SetActive(true);

				//ajout d'une tour au compteur et on enlève des billets
				player_script.modif_nbTour(1);
				player_script.modif_Billet(-100);
				break;
			}
		}
	}	

}