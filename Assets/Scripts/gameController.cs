using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class gameController : MonoBehaviour
{

    public float money;
    public static gameController instance;
    public TMP_Text interactiontxt;
    public TMP_Text doorText;
    public GameObject lunch;
    public GameObject classroom;
    public GameObject recess;
    public TMP_Text timer;
    public GameObject screen;
    public float classTime = 2;
    public float timeRemaining;
    public Slider parentsatis;
    public TMP_Text parenttxt;
   
    public GameObject player;
    public GameObject interactionScreen;
    public GameObject viewpoint;
  
    public float hungermaxvalue;
    public float currenthungervalue;
    
    public GameObject NPChandler;
    public GameObject EndScreen;
    bool inInteraction;
    public Camera cam;
    [HideInInspector]  public bool pause;
    public GameObject bully;
    public GameObject pauseScreen;
    public void Awake()
    {
        instance = this;
       
        

    }
  
    
    void Start()
    {
        
       
        timeRemaining = classTime;
        screen.SetActive(true);
        
        cam = Camera.main;
        
        pause = true;
    
        pauseScreen.SetActive(false);
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            pauseGame();

        }
        if (screen.activeInHierarchy)
        {
            cam.gameObject.GetComponent<camController>().enabled = false;
            player.gameObject.GetComponent<PlayerController>().enabled = false;
            

        }
        
       
       
    }
   
    
    public void close()
    {

        screen.SetActive(false);
        interactionScreen.SetActive(false);
        Camera.main.transform.position = viewpoint.gameObject.transform.position;
        Camera.main.transform.rotation = viewpoint.gameObject.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        cam.gameObject.GetComponent<camController>().enabled = true;
        player.gameObject.GetComponent<PlayerController>().enabled = true;
        pause = false;
        
        
    }
 


    public void attack()
    {
       
       
        cam.transform.LookAt(bully.transform);
        screen.SetActive(false);
        interactionScreen.SetActive(false);
        
    }
   

    public void reloadScene()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void gameEnd(string reasonfordying)
    {

        EndScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        //gameend
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        pause = true;
        
     
    }
    
  

    public void pauseGame()
    {

        pauseScreen.SetActive(true);
        pause = true;
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
    
}
