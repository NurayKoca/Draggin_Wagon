using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Axel{

    Front,
    Rear
}
[Serializable]
public struct Whell{

   public GameObject model;
   public   WheelCollider collider;
   public Axel axel; 
   
   
}

public class carcontrol : MonoBehaviour
{
    [SerializeField]
    private float MaxHizlanma = 200f;
    [SerializeField]
    private float donusHassasiyeti = 1f;
    [SerializeField]
    private float MaximumDonusAcisi = 45f;
    [SerializeField]
    private List<Whell> wheels;
    [SerializeField]
    private int FrenGucu;
    [SerializeField]
    private int HizlanmaDerecesi;
     public GameObject ArkaFar;
    public Material[] ArkaFarMateryaller;
    public GameObject[] OnFarIsiklari;
    bool OnFarAcikmi;
    private float inputX, inputY;

    public Vector3 centerOfMass;

    
    // Start is called before the first frame update
    void Start()
    {

        OnFarAcikmi = false;
        
      GetComponent<Rigidbody>().centerOfMass = centerOfMass;

    }

    private void LateUpdate()
    {
       Move();
       Turn();
       FrenYap();
    }

    // Update is called once per frame
    void Update()
    {

          Tekerlerindonusu();
          HareketYonu();

          if(Input.GetKeyDown(KeyCode.Q)){

            OnFarAcikmi = !OnFarAcikmi;

            OnFarIsiklari[0].SetActive(OnFarAcikmi);
            OnFarIsiklari[1].SetActive(OnFarAcikmi);


          }
    }

    void HareketYonu()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }
    private void Move()
    {
       
       foreach(var wheel in wheels){

           wheel.collider.motorTorque = inputY * MaxHizlanma * HizlanmaDerecesi *Time.deltaTime;
       }

    }
   void Turn()
   {

    foreach(var wheel in wheels)
    {

        if(wheel.axel == Axel.Front)
        {
 
            var _steerAngle = inputX * donusHassasiyeti * MaximumDonusAcisi;

            wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, .1f);
        }
    }

   }

   void Tekerlerindonusu()
   {

       foreach (var wheel in wheels)
       {

           Quaternion _rot;
           Vector3 _pos;
           wheel.collider.GetWorldPose(out _pos, out _rot);           
         //wheel.model.transform.position = _pos;
         //wheel.model.transform.rotation = _rot;

       }
   }
   void FrenYap(){

       if(Input.GetKeyDown(KeyCode.Space)){

           ArkaFar.GetComponent<MeshRenderer>().material = ArkaFarMateryaller[1];
           
           foreach(var wheel in wheels){

               wheel.collider.brakeTorque = FrenGucu;
           }
       }

       if(Input.GetKeyUp(KeyCode.Space)){

            ArkaFar.GetComponent<MeshRenderer>().material = ArkaFarMateryaller[0];
            
            foreach(var wheel in wheels){

                wheel.collider.brakeTorque = 0;
            }
       }
   }
}
