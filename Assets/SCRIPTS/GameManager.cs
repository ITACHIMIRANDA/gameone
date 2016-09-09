using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static int PickUpCount;

	void Awake () 
	{
		PickUpCount = 0;
	}
	

	void OnGUI ()
	{
		GUI.Label (new Rect ((Screen.width / 2.0f)+700, 25, 200, 100), string.Format ("{0}", PickUpCount));
        GUI.skin.GetStyle("label").fontSize = 100;
	}
}
