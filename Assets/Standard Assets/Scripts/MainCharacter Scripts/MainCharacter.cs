//Javier Gonzalez
//Main character behavior, physics, atributes
//july 31 2014
using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {


    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static MainCharacter s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static MainCharacter instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(MainCharacter)) as MainCharacter;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = new GameObject("AManager");
                s_Instance = obj.AddComponent(typeof(MainCharacter)) as MainCharacter;
                Debug.Log("Could not locate an AManager object.  AManager was Generated Automaticly.");
            }
            return s_Instance;
        }
    }

    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit()
    {
        s_Instance = null;
    }

    // Add the rest of the code here...




    private int score=0;
    private int highestScore;

    public TextMesh scoreText;
    public TextMesh highestScoreText;
    private bool isDead = false;
    private bool GAMEPLAYHASSTARTED=false;

    public Sprite[] tardisSprites;
    public SpriteRenderer thisSprite;
    private int jumpSpeed=7;
    public int WORLDSPEED=4; //the speed the world travels at
    private float spinSpeed = 0;
    private float defaultSpinSpeed = 30;
    private bool jmped = false; //states player has jumped
    private bool canJump=false; //checks if player has jumped
    public Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("highestScore") == null) //if empty, highest score will be zero
        {
            PlayerPrefs.SetInt("highestScore", score);
        }
        highestScore = PlayerPrefs.GetInt("highestScore");
        highestScoreText.text = highestScore.ToString();
        
        //if (highestScore>=5)
        //    AdvertisementHandler.ShowAds();
        //else
            //AdvertisementHandler.HideAds();
        if (highestScore >= 20)
            thisSprite.sprite = tardisSprites[0];
        if (highestScore >= 30)
            thisSprite.sprite = tardisSprites[1];
        if (highestScore >= 40)
            thisSprite.sprite = tardisSprites[2];
        scoreText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //move along horizontal
        //transform.position += new Vector3(travelSpeed * Time.deltaTime, 0, 0);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);



	}

    public void GamePlayStart()
    {
        GAMEPLAYHASSTARTED = true;
        Invoke("SetCanJumpTrue", .5f);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
        scoreText.text = score.ToString();
    }
    public bool GamePlayHasStarted()
    {
        return GAMEPLAYHASSTARTED;
    }


    void Update()
    {
        PushObjectBackInFrustum(this.transform);

        if (!isDead && GAMEPLAYHASSTARTED)
        {
            UserControlsJump();
           // TardisSpin();
        }



    }

    void RestartLevel()
    {
        Application.LoadLevel(0);
    }
    void UserControlsJump(){
        //check if player has jumped
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            
            jmped = true;
            canJump = false;
            Invoke("SetCanJumpTrue", .1f);
        }
        if (jmped) //if player jumped, go up
        {
            // transform.position+= new Vector3(0, 25* Time.deltaTime, 0);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed,0);
            spinSpeed = 503; //spin faster when jump
            jmped = false;
        }

    }
    void TardisSpin()
    {

        spinSpeed = Mathf.Lerp(spinSpeed, 0, 2 * Time.deltaTime);

        transform.Rotate(0, (defaultSpinSpeed + spinSpeed) * Time.deltaTime, 0);
    }

    void SetCanJumpTrue()
    {
        canJump = true;
    }

    bool canCollideAgain = true; //to avoid multiple collisions on same entrance

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Entrance"&&canCollideAgain&&!isDead)
        {
            score++;
            if (score>highestScore) //set high score
                PlayerPrefs.SetInt("highestScore", score);
            //if (score >= 5)
                //AdvertisementHandler.HideAds();
            scoreText.text = score.ToString();
            canCollideAgain = false;
            Invoke("ResetCanCollideAgain", 1);
            Debug.Log("Score: " + score);
        }
    }

    void ResetCanCollideAgain(){
        canCollideAgain=true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle"&&!isDead)
        {

            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;
            SetDead();
            rigidbody.AddExplosionForce(180, pos, 5);

        }
    }

    public void SetDead(){
        WORLDSPEED = 0;
        isDead = true;
        Invoke("RestartLevel", 2);
    }
    public int GetScore()
    {
        return score;
    }

    public bool IsDead()
    {
        return isDead;

    }
    public int GetWorldSpeed()
    {
        return WORLDSPEED;
    }


    private void PushObjectBackInFrustum(Transform obj)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.position);
        if (pos.y > .98f)
            pos.y = .98f;
        if (pos.y <= 0)
            SetDead();
        obj.position = new Vector3(Camera.main.ViewportToWorldPoint(pos).x, Camera.main.ViewportToWorldPoint(pos).y,4);
    }
}
