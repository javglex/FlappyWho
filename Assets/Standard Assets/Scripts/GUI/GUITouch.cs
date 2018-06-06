using UnityEngine;
using System.Collections;

public class GUITouch : MonoBehaviour {

    // Update is called once per frame
    private bool selected=false;
    Camera cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("GUICam").GetComponent<Camera>();
    }

    void Update()
    {
        TapSelect();
    }

    void TapSelect()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject==this.gameObject)
                        SetSelected(true);
                }
            }
        }
    }


    public void SetSelected(bool s)
    {
        Debug.Log("Selected", this);
        //CameraFade.StartAlphaFade(Color.black, false, 2f, .2f, () => { Application.LoadLevel(1); });
        selected= s;
        //MainCharacter.instance.GamePlayStart();
    }

    public bool GotSelected()
    {
        return selected;
    }
}
