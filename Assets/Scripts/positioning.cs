using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class positioning : MonoBehaviour
{
    public Transform lunchposition;
    public Transform recesspositon;
    public Transform classposition;
    private GameObject player;

    void Start()
    {
        player = gameController.instance.player;
        if (this.gameObject.tag == "bully")
        {
            this.gameObject.GetComponent<AIController>().enabled = true;


        }
    }

   
    void Update()
    {
        
       
    }
    public void changeposition()
    {

        if (gameController.instance.state == gameController.gamestate.classtime)
        {
            this.transform.position = classposition.position;
            this.transform.rotation = classposition.rotation;


        }
        else if (gameController.instance.state == gameController.gamestate.lunchtime)
        {
            this.transform.position = lunchposition.position;
            this.transform.rotation = lunchposition.rotation;
        }
        else if (gameController.instance.state == gameController.gamestate.recess)
        {
            this.transform.position = recesspositon.position;
            this.transform.rotation = recesspositon.rotation;
            
         
        }

    }

    public void resetRotation()
    {
        if (gameController.instance.state == gameController.gamestate.classtime)
        {
           
            this.transform.rotation = classposition.rotation;


        }
        else if (gameController.instance.state == gameController.gamestate.lunchtime)
        {
          
            this.transform.rotation = lunchposition.rotation;
        }
        else if (gameController.instance.state == gameController.gamestate.recess)
        {
         
            this.transform.rotation = recesspositon.rotation;


        }
    }
}
