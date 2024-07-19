using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))];
public class floatingForce : MonoBehaviour
{
    [SerializeField] private float underWaterDrag = 3, underWaterAngularDrag = 1; 
    [SerializeField] private float airDrag = 0, airAngularDrag = 0.05f;
    
    Rigidbody m_Rigidbody; 
    [SerializeField] private float floatingPower = 15f, float waterHeight = 0f;
    private bool underwater; 

    
    void Start()
    {
        m_Rigidbody= GetComponent<Rigidbody>();

    }

   
    void FixedUpdate()
    {
        float difference = transform.position.y - waterHeight;
         
         if(difference < 0){
            m_Rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);

            if(!underwater){
                underwater = true;
                 
            }
         }
         else if(underwater){
            underwater = false;
         }
        void SwitchState(bool isUnderwater){
                if(!isUnderwater){
                    m_Rigidbody.drag = underWaterDrag;
                    m_Rigidbody.angularDrag = underWaterAngularDrag
                }

        }
    }

   
    
}
