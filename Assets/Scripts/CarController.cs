using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class CarController : MonoBehaviour
{

    [Header("Speeds")]
    public float MaxSpeed;
    public float acc;
    public float steering;
    public float boostTime;
    public bool boostActive;
    public float boostMaxSpeed;
    public float boostMaxACC;
    public TextMeshProUGUI BoostCountDown;
    public int BoostCountINT;



    float X;
    float Y;

    [Header("Checkpoints")]
    public bool StartCheckpoint;
    public bool CheckPoint1;
    public bool CheckPoint2;
    public bool CheckPoint3;
    public bool CheckPoint4;
    public bool CheckPoint5;
    public bool CheckPoint6;
    
    public int lapsNumber;
    public TextMeshProUGUI textMeshPro;
    private Scene CurrentScene;
    

    [Header("Nivel4")]
    public GameObject Border1;
    public GameObject Border2;

    public GameObject Bridge1;
    public GameObject Bridge2;

    public GameObject Barrier1;
    public GameObject Barrier2;

    public bool isAbove;


    [Header("Pause Thing")]
    public bool gameIsPaused;
    public GameObject pausedPanel;
    public Slider VolumeSlider;
    public AudioSource MusicSource;
    

    [Header("Skin")]
    public Sprite[] ImageSkin;
    private SpriteRenderer SR;
    public int skinID;

    [Header("Stopwatch")]
    public float stopwatchTime;
    public bool stopWatchPause;
    public bool stopWatchStop;
    public bool stopWatchReset;
    public TextMeshProUGUI stopWatchTimer;

    [Header("Second Watchtime")]
    public float secondstopWatchTime;
    public bool secondstopWatchPause;
    public TextMeshProUGUI secondStopWatchTimer;





    // // // // // // // // // // // // // // // // // // // // // // // // // // 


    private float acelerationForpause;
    private float MaxSpeedDefault;
    private Rigidbody2D rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>(); // Componentes
        acelerationForpause = acc;
        MaxSpeedDefault = MaxSpeed;
        CurrentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        textMeshPro.text = lapsNumber.ToString();

        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");

        Vector2 Speed = transform.up*(Y * acc);
        rb.AddForce(Speed * Time.deltaTime);

        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

        if(acc > 0)
        {
            if(direction > 0)
            {
                rb.rotation -= X * steering * Time.deltaTime *(rb.velocity.magnitude / MaxSpeed);
            }
            else
            {
                rb.rotation += X * steering * Time.deltaTime * (rb.velocity.magnitude / MaxSpeed);
            }

            float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
            Vector2 relativeForce = Vector2.right * driftForce;
            rb.AddForce(rb.GetRelativeVector(relativeForce));

            if (rb.velocity.magnitude > MaxSpeed)
            {
                rb.velocity = rb.velocity.normalized * MaxSpeed * Time.deltaTime;

            }
            Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        }

        if(CurrentScene.name == "Nivel2" || CurrentScene.name == "SampleScene")
        {
            if (CheckPoint1 == true && CheckPoint2 == true && CheckPoint3 == true && StartCheckpoint == true)
            {
                lapsNumber = lapsNumber + 1;
                CheckPoint1 = false;
                CheckPoint2 = false;
                CheckPoint3 = false;
                StartCheckpoint = false;
                
            }
        }
        if(CurrentScene.name == "Nivel3")
        {
            if(StartCheckpoint == true && CheckPoint1 == true && CheckPoint2 == true && CheckPoint3 == true && CheckPoint4 == true && CheckPoint5 == true && CheckPoint6 == true)
            {
                lapsNumber++;
                CheckPoint1 = false;
                CheckPoint2 = false;
                CheckPoint3 = false;
                CheckPoint4 = false;
                CheckPoint5 = false;
                CheckPoint6 = false;
                StartCheckpoint = false;
            }
        }
        if(CurrentScene.name == "Nivel4")
        {
            if(StartCheckpoint == true && CheckPoint1 == true && CheckPoint2 == true && CheckPoint3 == true && CheckPoint4 == true && CheckPoint5 == true && CheckPoint6 == true )
            {
                lapsNumber++;
                StartCheckpoint = false;
                CheckPoint1 = false;
                CheckPoint2 = false;
                CheckPoint3 = false;
                CheckPoint4 = false;
                CheckPoint5 = false;
                CheckPoint6 = false;
            }
        }

        if(lapsNumber == 69)
        {
            textMeshPro.text = "WTFFFFFFFFFFF WTFFFFFFFFF 69 Voltas btw";
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        MusicSource.volume = VolumeSlider.value;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausefunction();
        }

        StopWatch();
    }

    #region Triggers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(CurrentScene.name == "Nivel2")
        {
            if (collision.tag == "StartCheckpoint")
            {
                StartCheckpoint = true;
            }
            if (collision.tag == "CheckPoint 1" && StartCheckpoint == true)
            {
                CheckPoint1 = true;
                StartCoroutine("boostDeactiveter");                                        //Boost
            }
            if (collision.tag == "CheckPoint 2" && CheckPoint1 == true)
            {
                CheckPoint2 = true;
            }
            if (collision.tag == "CheckPoint 3" && CheckPoint2 == true)
            {
                CheckPoint3 = true;
            }
        }
        if (CurrentScene.name == "SampleScene")
        {
            if (collision.tag == "StartCheckpoint")
            {
                StartCheckpoint = true;
            }
            if (collision.tag == "CheckPoint 1" && StartCheckpoint == true)
            {
                CheckPoint1 = true;
            }
            if (collision.tag == "CheckPoint 2" && CheckPoint1 == true)
            {
                CheckPoint2 = true;
            }
            if (collision.tag == "CheckPoint 3" && CheckPoint2 == true)
            {
                CheckPoint3 = true;
            }
        }
        if(CurrentScene.name == "Nivel3")
        {
            if (collision.tag == "StartCheckpoint")
            {
                StartCheckpoint = true;
            }
            if (collision.tag == "CheckPoint 1" && StartCheckpoint == true)
            {
                CheckPoint1 = true;
            }
            if (collision.tag == "CheckPoint 2" && CheckPoint1 == true)
            {
                CheckPoint2 = true;
            }
            if (collision.tag == "CheckPoint 3" && CheckPoint2 == true)
            {
                CheckPoint3 = true;
            }
            if (collision.tag == "CheckPoint4" && CheckPoint3 == true)
            {
                CheckPoint4 = true;
            }
            if (collision.tag == "CheckPoint5" && CheckPoint4 == true)
            {
                CheckPoint5 = true;
            }
            if (collision.tag == "CheckPoint6" && CheckPoint5 == true)
            {
                CheckPoint6 = true;
            }
        }
        if(CurrentScene.name == "Nivel4")
        {
            bridgeColiderActive();
            if (collision.tag == "StartCheckpoint")
            {
                StartCheckpoint = true;
                stopWatchReset = true;
            }
            if (collision.tag == "CheckPoint 1" && StartCheckpoint == true)
            {
                CheckPoint1 = true;
                isAbove = false;
            }
            if (collision.tag == "CheckPoint 2" && CheckPoint1 == true)
            {
                CheckPoint2 = true;
                isAbove = true;
            }
            if (collision.tag == "CheckPoint 3" && CheckPoint2 == true)
            {
                Barrier2.SetActive(false);
                Barrier1.SetActive(true);
                CheckPoint3 = true;
                SR.sortingOrder = 11;
            }
            if (collision.tag == "CheckPoint4" && CheckPoint3 == true)
            {
                Barrier2.SetActive(true);
                Barrier1.SetActive(false);
                isAbove = false;
                CheckPoint4 = true;
                SR.sortingOrder = 5;
                bridgeColiderActive();
            }
            if (collision.tag == "CheckPoint5" && CheckPoint4 == true)
            {
                CheckPoint5 = true;
            }
            if (collision.tag == "CheckPoint6" && CheckPoint5 == true)
            {
                CheckPoint6 = true;
            }
        }
    }

    #endregion

    #region Functions

    void pausefunction()
    {
        if(gameIsPaused == false)
        {
            pausedPanel.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused =true;
            Debug.Log(gameIsPaused);
            acc = 0f;
            stopWatchPause = true;
        }
        else if(gameIsPaused == true)
        {
            stopWatchPause = false;
            pausedPanel.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            Debug.Log(gameIsPaused);
            acc = acelerationForpause;
        }
    }

    public void SkinChanger()
    {
        skinID = skinID + 1;
        if (skinID > 1)
        {
            skinID = 0;
        }
        SR.sprite = ImageSkin[skinID];
        
    }

    IEnumerator boostDeactiveter()
    {
        boostActive = true;
        MaxSpeed = boostMaxSpeed;
        acc = boostMaxACC;
        StartCoroutine("CountdownForBoost");
        yield return new WaitForSeconds(boostTime);
        boostActive = false;
        MaxSpeed = MaxSpeedDefault;
        acc = acelerationForpause;
    }

    IEnumerator CountdownForBoost()
    {
        while (true)
        {
            BoostCountDown.text = BoostCountINT.ToString();
            yield return new WaitForSeconds(1f);
            BoostCountINT--;
            if (BoostCountINT == -1)
            {
                BoostCountINT = 3;
                break;
            }
        }
    }

    void bridgeColiderActive()
    {
        if(isAbove == true)
        {
            Border1.SetActive(true);
            Border2.SetActive(true);

            Bridge1.SetActive(false);
            Bridge2.SetActive(false);

        }
        if(isAbove == false)
        {
            Bridge1.SetActive(true);
            Bridge2.SetActive(true);

            Border1.SetActive(false);
            Border2.SetActive(false);
        }
    }

    void StopWatch()
    {
        
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime - minutes * 60);

        int seconminutes = Mathf.FloorToInt(secondstopWatchTime / 60);
        int secseconds = Mathf.FloorToInt(secondstopWatchTime - seconminutes * 60);



         //1st StopWatch
        if (stopWatchPause == false)
        { 
            stopwatchTime = stopwatchTime + Time.deltaTime;
            stopWatchTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (stopWatchPause == true)
        {
            stopWatchTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if(stopWatchReset == true)
        {
            stopwatchTime = 0;
            stopWatchReset = false;
        }

        //2st StopWatch
        if (secondstopWatchPause == false)
        {
            secondstopWatchTime = secondstopWatchTime + Time.deltaTime;
            secondStopWatchTimer.text = string.Format("{0:00}:{1:00}", seconminutes, secseconds);
        }
    }


    #endregion

}
