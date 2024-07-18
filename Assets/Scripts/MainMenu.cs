using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    //To assign the firstLevel, just click on main menu in unity and scroll down in the inspector and put the 
    //of the first scene

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame(){
        SceneManager.LoadScene(firstLevel);
    }
    public void quit(){
        Application.Quit();
        Debug.Log("Quitting, will work when game is built.");
    }
}
