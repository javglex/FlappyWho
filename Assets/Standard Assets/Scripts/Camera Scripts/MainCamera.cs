//Javier Gonzalez
//aug 1 2014
//camera stuff

using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Transform player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!MainCharacter.instance.IsDead() && MainCharacter.instance.GamePlayHasStarted())
	        transform.position=Vector3.Lerp(transform.position, new Vector3(player.transform.position.x+2,transform.position.y,transform.position.z),10f*Time.deltaTime);

        if (MainCharacter.instance.GamePlayHasStarted())
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 7, 2 * Time.deltaTime);
        }
	}

}
