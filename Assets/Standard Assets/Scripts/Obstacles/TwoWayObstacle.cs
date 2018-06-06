//Javier Gonzalez
//aug 2 2014
//two way obstacle is an obstacle emerging out of top and bottom view, leaving a small opening the player can go through

using UnityEngine;
using System.Collections;

public class TwoWayObstacle : MonoBehaviour {

    int entranceHeight; //where in the obstacle the entrance will be positioned, 0 is ground level, 2 around middle
    int entranceSize; //how big the entrance will be (1 block - difficult, 3 blocks -easy)
    public GameObject[] block; //block and block end must match aesthetically
    public GameObject[] blockEnd;
    public GameObject entranceTrigger; //to score points when player passes through obstacle

    class ObstacleSegment{
        public bool isEmpty;
        public ObstacleSegment()
        {
            isEmpty = true; //obstacle entrance, default true (loop will then set necessary obstacle segments)
        }
    }

    class EntranceSegments
    {
        public int entranceHeight, entranceSize;      //if only one block will be entrance
        public int[] entranceHeights;   //if multiple (to make it bigger)
        public EntranceSegments(int h,int s){ //entrance height, entrance size
            entranceHeight=h;
            entranceSize=s;
            entranceHeights = new int[entranceSize];
            for (int i=0;i<entranceSize;i++)
                entranceHeights[i]=entranceHeight+i-entranceSize/2;
        }

        public int[] GetEntranceHeights()
        {
            return entranceHeights;
        }
    }

    ObstacleSegment[] obstacleArray = new ObstacleSegment[11];  //the obstacle's blocks and positions will be determined in an array before spawning

    // Use this for initialization
	public void ForcedStart () {    //controlled by obstacle spawner
       // entranceHeight = 5;
        entranceSize=3;
        
        EntranceSegments entranceSegs = new EntranceSegments(entranceHeight, entranceSize);

	    for(int i=0;i<obstacleArray.Length;i++)
        {
            obstacleArray[i]=new ObstacleSegment();
            for (int j = 0; j < entranceSize; j++)
                if (i == entranceSegs.GetEntranceHeights()[j])
                    obstacleArray[i].isEmpty = false;       //lets add trigger colliders for score count
        }


        //draw
        for (int i = 0; i < obstacleArray.Length; i++)
        {
            if (obstacleArray[i].isEmpty == true)
            {
                GameObject obj = Instantiate(block[0], new Vector3(transform.position.x, i - 5, 4), Quaternion.identity) as GameObject;
                obj.transform.parent = this.transform;
            }
            else
            {
                GameObject entrance = Instantiate(entranceTrigger, new Vector3(transform.position.x+.9f, i - 5, 4), Quaternion.identity) as GameObject;
                entrance.transform.parent = this.transform;
            }
            Debug.Log(obstacleArray[i].isEmpty+" | ");
        }

	}

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-MainCharacter.instance.GetWorldSpeed()*Time.deltaTime, 0, 0);
	}

    public int GetEntranceHeight()
    {
        return entranceHeight;
    }

    public void SetEntranceHeight(int h)
    {
        entranceHeight = h;
    }


    public int GetEntranceSize()
    {
        return entranceSize;
    }

    public void SetEntranceSize(int s)
    {
        entranceSize = s;
    }
}
