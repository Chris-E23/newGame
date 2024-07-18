using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class gameController : MonoBehaviour
{

    public TMP_Text moneytxt;
    public TMP_Text beginningText;
    public gamestate state = gamestate.classtime;
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
    public Slider hungerslider;
    public TMP_Text hungertxt;
    public GameObject player;
    public GameObject interactionScreen;
    public GameObject viewpoint;
    public TMP_Text moneytheyhave;
    public Transform classteleport;
    public Transform lunchteleport;
    public Transform recessteleport;
    public float parentsatisfactionmax = 100;
    public float currentparentsatisfaction;
    public float hungermaxvalue;
    public float currenthungervalue;
    public days daystatus;
    public GameObject gangmembertext;
    public GameObject NPChandler;
    public GameObject EndScreen;
    bool inInteraction;
    public Camera cam;
    public TMP_Text daytext;
    public GameObject bribebutton;
    public GameObject launchattackbutton;
    public bool attackgoing;
    [HideInInspector]  public bool pause;
    public GameObject bully;
    public AudioSource teacher;
    public TMP_Text reasonfordyingtext;
    public TMP_Text peopleingangtext;
    public GameObject pauseScreen;
    public List<lunchbox> lunchboxes = new List<lunchbox>();
    public GameObject winScreen;
    public AudioSource bullydying;
    public GameObject closebutton;

    public void Awake()
    {
        instance = this;
        daystatus = days.monday;
        

    }
    public enum gamestate
    {
        lunchtime,
        classtime,
        recess,
        newday

    }
    public enum days
    {
        monday,
        tuesday,
        wednesday,
        thursday,
        friday

    }
    
    void Start()
    {
        money = 20;
        state = gamestate.classtime;
        timeRemaining = classTime;
        screen.SetActive(true);
        parentsatis.maxValue = parentsatisfactionmax;
        hungerslider.maxValue = hungermaxvalue;
        cam = Camera.main;
        daytext.text = "Monday";
        pause = true;
        peopleingangtext.text = "People in gang: ";
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
            parentsatis.gameObject.SetActive(false);
            hungerslider.gameObject.SetActive(false);

        }
        
       if(NPChandler.gameObject.GetComponent<nameGenerator>().ganglist.Count >= 5)
        {
            gangmembertext.gameObject.SetActive(true);


        }
   
        
        
        
        
        parentsatis.value = currentparentsatisfaction;
        hungerslider.value = currenthungervalue;
        
        
        
        
        
        
        if(state == gamestate.newday)
        {
            for(int i = 0; i < lunchboxes.Count; i++)
            {
                lunchboxes[i].ResetMoney();

            }
            bully.gameObject.GetComponent<NavMeshAgent>().speed += 1;
            if (daystatus == days.monday)
            {
                daystatus = days.tuesday;
                currentparentsatisfaction -= 20;
                currenthungervalue -= 30;
                state = gamestate.classtime;
                changeposition();
                daytext.text = "Tuesday";
            }
            else if(daystatus == days.tuesday)
            {
                daystatus = days.wednesday;
                currentparentsatisfaction -= 20;
                currenthungervalue -= 20;
                state = gamestate.classtime;
                changeposition();
                daytext.text = "Wednesday";
            }
            else if (daystatus == days.wednesday)
            {

                daystatus = days.thursday;
                currentparentsatisfaction -= 20;
                currenthungervalue -= 50;
                state = gamestate.classtime;
                changeposition();
                daytext.text = "Thursday";
            }
            else if (daystatus == days.thursday)
            {
                daystatus = days.friday;
                currentparentsatisfaction -= 20;
                currenthungervalue -= 50;
                state = gamestate.classtime;
                changeposition();
                daytext.text = "Friday";
            }
            
           

        }
        if(money <= 0)
        {

            //game end
            gameEnd("You lost all your money!");
        }
        if(currentparentsatisfaction <= 0)
        {
            gameEnd("Your parents became too unhappy!");
        }
        if(currenthungervalue <= 0)
        {

            gameEnd("You died of hunger!");
        }
       
        timer.text = "Time: " + (int)timeRemaining;

       
        if((state == gamestate.classtime || state == gamestate.lunchtime || state == gamestate.recess) && !pause)
        {
            time();
        }
        
        if(timeRemaining <= 0)
        {
                switch (state)
                {

                    case gamestate.classtime:
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.transform.position = lunchteleport.position;
                        state = gamestate.lunchtime;
                        timeRemaining = classTime;
                        teacher.Stop();
                        teacher.loop = false;
                    changeposition();
                        break;
                    case gamestate.lunchtime:
                        state = gamestate.recess;
                        timeRemaining = classTime;
                        NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                        player.transform.position = recessteleport.position;
                        changeposition();
                        recesstime();
                        
                    break;
                    case gamestate.recess:
                        if (daystatus == days.friday)
                        {

                            if (NPChandler.gameObject.GetComponent<nameGenerator>().ganglist.Count >= 5)
                            {

                            launchattackbutton.gameObject.SetActive(true);
                            pause = true;
                            interactionScreen.SetActive(true);
                            interactiontxt.text = "You've outlasted the bully everyday, now launch your attack on him";
                            bribebutton.gameObject.SetActive(false);
                            closebutton.SetActive(false);
                            cam.gameObject.GetComponent<camController>().enabled = false;
                            player.gameObject.GetComponent<PlayerController>().enabled = false;
                            Cursor.lockState = CursorLockMode.None;

                            }
                        else
                        {

                            gameEnd("You didn't get the bully by the end of the week");
                        }
                           
                        }
                        else
                        {

                            state = gamestate.newday;
                            timeRemaining = classTime;
                            NPChandler.gameObject.GetComponent<nameGenerator>().respawn();
                            changeposition();
                            player.transform.position = classteleport.position;
                            player.transform.position = classteleport.position;
                            teacher.Play();
                            teacher.loop = true;

                        }

                        
                        break;



                }

            
            

        }
        moneytxt.text = "Money: " + money; 
        if(state == gamestate.classtime)
        {
            
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(true);
            recess.gameObject.SetActive(false);
            
        }
        else if(state == gamestate.lunchtime)
        {
            
        
            lunch.gameObject.SetActive(true);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(false);
            teacher.Stop();
            teacher.loop = false;
        }
        else if (state == gamestate.recess)
        {
           
            lunch.gameObject.SetActive(false);
            classroom.gameObject.SetActive(false);
            recess.gameObject.SetActive(true);
            teacher.Stop();
            teacher.loop = false;
        }

        if (screen.activeInHierarchy)
        {
           
            Cursor.lockState = CursorLockMode.None;
            parentsatis.gameObject.SetActive(false);
            parenttxt.gameObject.SetActive(false);
            hungerslider.gameObject.SetActive(false);
            hungertxt.gameObject.SetActive(false);
        }
        else
        {
            
            parentsatis.gameObject.SetActive(true);
            parenttxt.gameObject.SetActive(true);
            hungerslider.gameObject.SetActive(true);
            hungertxt.gameObject.SetActive(true);
            timer.gameObject.SetActive(true);

        }


    }
   
    public void time()
    {
        timeRemaining -= 1 * Time.deltaTime;


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
        parentsatis.gameObject.SetActive(true);
        hungerslider.gameObject.SetActive(true);
        if(player.gameObject.GetComponent<interacting>().person.gameObject != null)
            player.gameObject.GetComponent<interacting>().person.gameObject.GetComponent<positioning>().resetRotation();
    }
 

    public void recesstime()
    {
        pause = true;
        interactionScreen.SetActive(true);
        interactiontxt.text = "It's recess time, your bully is going to go after you to steal your money, last the period in order to keep your money.";
        bribebutton.gameObject.SetActive(false);
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;

        if (daystatus == days.monday)
        {
            bully.gameObject.GetComponent<NavMeshAgent>().speed = 3;


        }

        
          

    }

    public void attack()
    {
        attackgoing = true;
        bribebutton.SetActive(false);
        cam.transform.LookAt(bully.transform);
        screen.SetActive(false);
        interactionScreen.SetActive(false);
        closebutton.SetActive(false);
    }
    public void changeposition()
    {
        for (int i = 0; i < 15; i++) {

            NPChandler.GetComponent<nameGenerator>().classroom[i].GetComponent<positioning>().changeposition();

    }
        bully.gameObject.GetComponent<positioning>().changeposition();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene("SampleScene");




    }

    public void gameEnd(string reasonfordying)
    {

        EndScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        parentsatis.gameObject.SetActive(false);
        parenttxt.gameObject.SetActive(false);
        hungerslider.gameObject.SetActive(false);
        hungertxt.gameObject.SetActive(false);
        //gameend
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        reasonfordyingtext.text = reasonfordying;
        pause = true;
        teacher.Stop();
        teacher.loop = false;
        peopleingangtext.gameObject.SetActive(false);
    }
    
    public void continueGame()
    {
        pauseScreen.SetActive(false);
        pause = false;
        cam.gameObject.GetComponent<camController>().enabled = true;
        player.gameObject.GetComponent<PlayerController>().enabled = true;
        teacher.Play();
        teacher.loop = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void pauseGame()
    {

        pauseScreen.SetActive(true);
        pause = true;
        cam.gameObject.GetComponent<camController>().enabled = false;
        player.gameObject.GetComponent<PlayerController>().enabled = false;
        teacher.Stop();
        teacher.loop = false;
        Cursor.lockState = CursorLockMode.None;
    }
    
}
