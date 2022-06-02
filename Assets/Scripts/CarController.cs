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
    public bool CheckPoint1;
    public bool CheckPoint2;
    public bool CheckPoint3;
    public bool StartCheckpoint;
    public int lapsNumber;
    public TextMeshProUGUI textMeshPro;
    private Scene CurrentScene;
    public bool CheckPoint4;
    public bool CheckPoint5;
    public bool CheckPoint6;

    [Header("Pause Thing")]
    public bool gameIsPaused;
    public GameObject pausedPanel;
    public Slider VolumeSlider;
    public AudioSource MusicSource;
    

    [Header("Skin")]
    public Sprite[] ImageSkin;
    private SpriteRenderer SR;
    public int skinID;

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

        


    }


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



    }

    void pausefunction()
    {
        if(gameIsPaused == false)
        {
            pausedPanel.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused =true;
            Debug.Log(gameIsPaused);
            acc = 0f;
        }
        else if(gameIsPaused == true)
        {
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
        if(skinID == 2)
        {
            skinID = 0;
        }
        if(skinID == 0)
        {
            SR.sprite = ImageSkin[skinID];
        }
        if (skinID == 1)
        {
            SR.sprite = ImageSkin[skinID];
        }
        if (skinID < 0)
        {
            skinID = 0;
        }
        if (skinID > 0)
        {
            skinID = 0;
        }
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
}
