using UnityEngine;
using System.Collections;

public class GUILeave : MonoBehaviour {

    public GameObject[] guiObjects;
    public GameObject[] highScoreObjects;

    public AudioSource musicAudio;
    public GUITouch playButton;
    public GUITouch highScoreButton;
    public GUITouch exitHighScoreButton;
    public GUITouch muteButton;
    public TextMesh pausedText;
    private bool mainMenuSelected = true;
    private bool disableAll = false;
    private bool paused = false;
    private bool muted = false;
	// Use this for initialization
	void Start () {
        musicAudio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
	}
	
    

	// Update is called once per frame
	void Update () {

        if (disableAll)
        {
            if (Input.GetKeyDown(KeyCode.Escape)&&!MainCharacter.instance.IsDead())
            {
                paused = !paused;
                if (paused)
                {
                    pausedText.text = "Paused";
                    Time.timeScale = 0;
                }
                else
                {
                    pausedText.text = "";
                    Time.timeScale = 1;
                }
            }

            return;
        }

        if (muteButton.GotSelected())
        {
            muted = !muted;
            if (muted)
            {
                musicAudio.GetComponent<AudioSource>().Stop();
            }
            else
                musicAudio.GetComponent<AudioSource>().Play();
            muteButton.SetSelected(false);
        }

        if (mainMenuSelected)
            if (Input.GetKeyDown(KeyCode.Escape)) 
            { 
                Application.Quit(); 
            }

	    if (playButton.GotSelected()){
            if (!MainCharacter.instance.GamePlayHasStarted())
            {
                MainCharacter.instance.GamePlayStart();
                muteButton.transform.position = new Vector3(100, 100, 0);
                mainMenuSelected = false;
            }
            
            for (int i = 0; i < guiObjects.Length; i++)
            {
                if (guiObjects[i].transform.position.x < 6)
                    guiObjects[i].transform.position += new Vector3(21 * Time.deltaTime, 0, 0);
                else disableAll=true;   //disable all gui elements once gameplay starts
            }
        }

        if (highScoreButton.GotSelected())//HighScoreGUI.selected
        {
            mainMenuSelected = false;
            for (int i = 0; i < guiObjects.Length; i++)
            {
                if (guiObjects[i].transform.position.x < 6)
                    guiObjects[i].transform.position += new Vector3(21 * Time.deltaTime, 0, 0); //main menu out
                
            }

            for (int i = 0; i < highScoreObjects.Length; i++)
            {
                if (highScoreObjects[i].transform.position.x < 0)
                    highScoreObjects[i].transform.position += new Vector3(20 * Time.deltaTime, 0, 0); //score menu in
                else
                {
                    highScoreButton.SetSelected(false);
                    for (int j = 0; j < highScoreObjects.Length; j++)
                        highScoreObjects[j].transform.position = new Vector3(0, highScoreObjects[j].transform.position.y, highScoreObjects[j].transform.position.z);
                }
            }
        }


        if (exitHighScoreButton.GotSelected() || Input.GetKeyDown(KeyCode.Escape))//HighScoreGUI.unselected
        {
            mainMenuSelected = true;
            exitHighScoreButton.SetSelected(true);
            for (int i = 0; i < guiObjects.Length; i++)
            {
                if (guiObjects[i].transform.position.x > 0)
                    guiObjects[i].transform.position -= new Vector3(21 * Time.deltaTime, 0, 0); //main menu in
                else guiObjects[i].transform.position = new Vector3(0, guiObjects[i].transform.position.y, guiObjects[i].transform.position.z);
            }

            for (int i = 0; i < highScoreObjects.Length; i++)
            {
                if (highScoreObjects[i].transform.position.x > -5)
                    highScoreObjects[i].transform.position -= new Vector3(20 * Time.deltaTime, 0, 0); //score menu out
                else
                {
                    exitHighScoreButton.SetSelected(false);
                    for (int j = 0; j < guiObjects.Length; j++)
                        guiObjects[j].transform.position = new Vector3(0, guiObjects[j].transform.position.y, guiObjects[j].transform.position.z);
                }
            }
        }
	}
}
