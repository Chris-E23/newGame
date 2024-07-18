using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
public class NPCStorage : MonoBehaviour
{
    public string name;
    public TMP_Text nametxt;
    public float money;
    public bool ingang;
    public GameObject bully;
    private GameObject player;
    void Start()
    {

        this.gameObject.GetComponent<AIController>().enabled = false;
        player = gameController.instance.player;
        ingang = false;
    }

    void Update()
    {

        nametxt.text = name;
        if (ingang && gameController.instance.attackgoing == true)
        {
            this.gameObject.GetComponent<AIController>().enabled = true;
            this.gameObject.GetComponent<AIController>().target = bully.transform;
            this.gameObject.GetComponent<AIController>().agent = this.gameObject.GetComponent<NavMeshAgent>();
        }

       
        else
        {
            this.gameObject.GetComponent<AIController>().enabled = false;

        }

    }
    public void setName(string name_)
    {

        name = name_;

    }
}
