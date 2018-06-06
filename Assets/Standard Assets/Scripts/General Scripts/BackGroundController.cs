using UnityEngine;
using System.Collections;

public class BackGroundController : MonoBehaviour {

    public Transform[] backGround;    //move slow (farther)
    public Transform[] midGround;     //middle ground
    public Transform[] foreGround;    //move faster (closer)
    public Transform[] ground;
    private int speed=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (MainCharacter.instance.GamePlayHasStarted()){
            speed=MainCharacter.instance.GetWorldSpeed()/4;
        }

        for (int i = 0; i < backGround.Length; i++)
        {
            backGround[i].Translate(new Vector3(-1*speed*Time.deltaTime, 0, 0));
            if (backGround[i].transform.position.x < -22)
            {
                backGround[i].transform.position = new Vector3(44, backGround[i].transform.position.y, backGround[i].transform.position.z);
            }
        }
        for (int i = 0; i < midGround.Length; i++)
        {
            midGround[i].Translate(new Vector3(-4*speed * Time.deltaTime, 0, 0));
            if (midGround[i].transform.position.x < -15)
            {
                midGround[i].transform.position = new Vector3(20, midGround[i].transform.position.y, midGround[i].transform.position.z);
            }
        }

        for (int i = 0; i < ground.Length; i++)
        {
            ground[i].Translate(new Vector3(-4 * speed * Time.deltaTime, 0, 0));
            if (ground[i].transform.position.x < -22)
            {
                ground[i].transform.position = new Vector3(44, ground[i].transform.position.y, ground[i].transform.position.z);
            }
        }

        for (int i = 0; i < foreGround.Length; i++)
        {
            foreGround[i].Translate(new Vector3(-.2f*speed * Time.deltaTime, 0, 0));
        }

	}
}
