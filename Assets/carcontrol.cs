using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Axel{

    front,
    rear
}
[Serializable]
public struct whell{

   public GameObject wheel;
   public WheelCollider collider;
   public Axel axel; 
}

public class carcontrol : MonoBehaviour
{
    [SerializeField]
    private float MaxHizlanma = 200f;
    [SerializeField]
    private float dönüşHassasiyeti = 1f;
    [SerializeField]
    private float MaximumDonusAcisi = 45f;
    [SerializeField]
    private List<whell> wheels;
    private float inputX, inputY;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
       Move();
    }

    // Update is called once per frame
    void Update()
    {
          HareketYonu();
    }

    void HareketYonu()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
    private void Move()
    {
       
       foreach(var wheel in wheels){

           wheel.collider.motorTorque = inputY * MaxHizlanma * 500 *Time.deltaTime;
       }

    }
   
}
