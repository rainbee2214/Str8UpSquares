using UnityEngine;
using System.Collections;

public class NextLevelLoader : MonoBehaviour {

	public int delayTime = 0;
	public string nextScene;
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeSinceLevelLoad > delayTime) 
			Application.LoadLevel(nextScene);
	}
}
