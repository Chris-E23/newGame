using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    private GameObject player;
    public Transform touchplayerdirection;
    public LayerMask playerlayer;
    public LayerMask bullylayer;
    public AudioSource bullydying;
    public GameObject NPCHandler;
    Camera cam;

    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        
        player = gameController.instance.player;
        cam = Camera.main;

    }

    
    void Update()
    {

        if (gameController.instance.state == gameController.gamestate.recess && this.gameObject.tag == "bully" && !gameController.instance.pause)
        {

           
            target = player.transform;
            agent = this.gameObject.GetComponent<NavMeshAgent>();
            this.GetComponent<NavMeshAgent>().enabled = true;
            moveToTarget();
        }
        else if(gameController.instance.attackgoing)
        {
            gangAttack();


        }
       
        
    }

    private void moveToTarget()
    {

        agent.SetDestination(target.position);
        
        Ray ray = new Ray(touchplayerdirection.position, touchplayerdirection.forward);
        
        
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, playerlayer))
        {
           
              
            gameController.instance.money = 0;


            

        }
        
    }

    public void gangAttack()
    {
        target = gameController.instance.bully.transform;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        this.GetComponent<NavMeshAgent>().enabled = true;
        this.GetComponent<NavMeshAgent>().speed = 10f;
        
        agent.SetDestination(target.position);

        Ray ray = new Ray(touchplayerdirection.position, touchplayerdirection.forward);
        gameController.instance.bully.GetComponent<AIController>().enabled = false;
        gameController.instance.bully.GetComponent<NavMeshAgent>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(mycoroutine()); 
      

    }

    IEnumerator mycoroutine()
    {
        yield return new WaitForSeconds(8);
        gameController.instance.winScreen.SetActive(true);
        
    }
}
