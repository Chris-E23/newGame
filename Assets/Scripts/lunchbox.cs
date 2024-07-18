using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunchbox : MonoBehaviour
{
    public float money = 5;

    public void Update()
    {
        if (gameController.instance.state == gameController.gamestate.newday)
        {

            money = 5;

        }
    }
    public void ResetMoney()
    {
        money = 5;
    }

}
