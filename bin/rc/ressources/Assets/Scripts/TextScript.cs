using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

	[SerializeField]
	string newText;
	[SerializeField]
	bool billet;
	[SerializeField]
	bool tour;
	[SerializeField]
	bool monstre;
	[SerializeField]
	Text ownText;
	[SerializeField]
	PlayerScript _playerScript;

	// Update is called once per frame
	void Update () {
		if(billet)
			ownText.text=newText+" : "+_playerScript.combien_billet();
		else if(tour)
			ownText.text=newText+" : "+_playerScript.combien_tour();
		else if(tour)
			ownText.text=newText+" : "+_playerScript.combien_monstre();


	}
}
