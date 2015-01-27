using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour {
	public GUISkin skin = null;

	public static int billet = 1000;

	public static int nb_mob_max=30;

	public static int nb_mob=0;

	void OnGUI () {
		GUI.skin = skin;

		GUI.Label(new Rect(0,0,400,200), "Billet :" + billet);
	}

}
