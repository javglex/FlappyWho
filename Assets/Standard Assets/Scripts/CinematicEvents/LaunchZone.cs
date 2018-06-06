using UnityEngine;
using System.Collections;

public class LaunchZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(MainCharacter.instance.GamePlayHasStarted())
            transform.position -= new Vector3(MainCharacter.instance.GetWorldSpeed()*2*Time.deltaTime, 10*Time.deltaTime, 0);
	}
}
