using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

    public GameObject[] obstacleTypes;
    public GameObject mainCamera;
    int prevHeight=3;
    float maxHeight = 2.5f;
    int prevSize;
	// Use this for initialization
	void Start () {
	    InvokeRepeating("SpawnObstacle",1,1.5f);
	}
	
    void SpawnObstacle(){
        if (MainCharacter.instance.GamePlayHasStarted())
        {
            GameObject prevObs = Instantiate(obstacleTypes[0], new Vector3(mainCamera.transform.position.x + 10, Random.Range(-maxHeight, maxHeight), 4), Quaternion.identity) as GameObject;
           // prevObs.GetComponent<TwoWayObstacle>().SetEntranceHeight(5 + Random.Range(-3, 3));
           // prevObs.GetComponent<TwoWayObstacle>().ForcedStart();
        }
    }

	// Update is called once per frame
	void Update () {
        if (MainCharacter.instance.GetScore() == 5)
        {
            maxHeight = 3;
        }

        if (MainCharacter.instance.GetScore() == 10)
        {
            maxHeight = 3.5f;
        }

        if (MainCharacter.instance.GetScore() == 20)
        {
            maxHeight = 4;
        }
	}
}
