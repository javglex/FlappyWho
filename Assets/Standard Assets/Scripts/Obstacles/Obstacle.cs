using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public Transform entrance;
    public Transform bottom;
    public Transform top;
    private float offset=4.5f;   //distance apart( or opening size for variable difficulty); 4.5 being most difficult

	// Use this for initialization
	void Start () {

        entrance = transform.Find("entrance");
        bottom = transform.Find("lower");
        top = transform.Find("upper"); 



        Invoke("Destroy", 6);
        if (MainCharacter.instance.GetScore() < 3)
            offset = 5.0f;
        if (MainCharacter.instance.GetScore() >= 3)
            offset = 4.9f;
        if (MainCharacter.instance.GetScore() >= 6)
            offset=4.8f;
        if (MainCharacter.instance.GetScore() >= 12)
            offset = 4.7f;
        if (MainCharacter.instance.GetScore() >= 16)
            offset = 4.5f;
        bottom.transform.localPosition = new Vector3(0, -offset, 0);
        
        top.transform.localPosition = new Vector3(0, offset, 0);
	}

    void Destroy()
    {
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(-MainCharacter.instance.GetWorldSpeed() * Time.deltaTime, 0, 0);
	}
}
