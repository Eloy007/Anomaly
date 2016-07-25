using UnityEngine;
using System.Collections;

public class TestTranslate : MonoBehaviour {

    // Use this for initialization
    private Quaternion qStart;
    private InputControl iControl;
    private LineRenderer lRender;
    Animator anim;
    CharacterController characterController;
    Vector3 lookVector;

    public GameObject bullet;

    public GameObject bulletPosition;

    Collider collider;

    float aA = 0;

    float health = 100f;


    bool die=false;

 

    void Start () {


        
        qStart = this.transform.rotation;
        if (name == "knight1")
        {
            iControl = new InputControl(Screen.width-Screen.width / 2.5f, 0, Screen.width, Screen.height);
        }

        if (name == "knight2")
        {
            iControl = new InputControl(0,0, Screen.width/2.5f, Screen.height);
        }

        lRender = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        characterController= GetComponent<CharacterController>();
        collider= GetComponent<Collider>();
        lRender.SetVertexCount(2);

        lookVector= (transform.rotation * Vector3.forward).normalized;




    }

    


    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    // Update is called once per frame
   void FixedUpdate()
    {
        //Physics.BoxCast()
    }


    

    void OnTriggerEnter(Collider other)
    {
        //other.gameObject.des

        DestroyObject(other.gameObject);
        anim.SetTrigger("hit");

        health -= Random.Range(5, 20); 

        if(health<0)
        {
            die = true;
            anim.SetBool("die",true);
            

        }


        //foreach (ContactPoint contact in other.contacts)
        //{
        //  Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
    }
  

    void Update () {
        //if (!Input.GetMouseButtonUp(0)) return;
        if (die) return;
        if(Input.GetKeyUp(KeyCode.Space)||(iControl.getFire()))
        {
            //bullet.

            if (bullet == null)
                return;

            Vector3 newV = bulletPosition.transform.position;
            //newV= Vector3.forward * 60f;

            GameObject newBullet=(GameObject)GameObject.Instantiate(bullet, newV, this.transform.rotation);
            newBullet.SetActive(true);
            Rigidbody rbody = newBullet.GetComponent<Rigidbody>();
            //Collider rbodyCollider = newBullet.GetComponent<Collider>();
            rbody.AddForce(lookVector * 40,ForceMode.VelocityChange);
            //Collider
            

            
            //Physics.ig (rbodyCollider, collider);



        }


        if(iControl.Update())
        {
            lRender.enabled = true;
            Vector3 v3 = iControl.getVector();

            if ((v3.x == 0f) && (v3.y == 0f) && (v3.z == 0f))
                return;

            //Debug.DrawLine(iControl.getStart(),iControl.getEnd(),Color.red);
            
            Vector3 pStart = new Vector3(iControl.getStart().x,1, iControl.getStart().y);
            Vector3 pEnd = new Vector3(iControl.getEnd().x, 1, iControl.getEnd().y);

            

            float dist=Vector3.Distance(pStart, pEnd);
            

            Vector3 nV3 = v3.normalized;
            nV3.z = nV3.y;
            nV3.y = 0;

            //Time.deltaTime


            if (dist > 100)
                dist = 100;

            float fTspeed = dist * Time.deltaTime;

            float mX = (nV3.x * fTspeed);
            float mZ= (nV3.z * fTspeed);

            anim.SetBool("isMove", true);



            lRender.SetPositions(new Vector3[] { transform.position, (transform.position+ nV3*dist) });


            transform.rotation = Quaternion.LookRotation(nV3);

            lookVector = new Vector3(nV3.x, nV3.y, nV3.z);

            //transform.position= Vector3.MoveTowards(transform.position, transform.position + (nV3 * 100),fTspeed);

            Vector3 moveVector = nV3 * fTspeed;

            
           characterController.Move(moveVector);
            
            


            //transform.po

        }
        else
        {
            lRender.enabled = false;
            if (anim.GetBool("isMove"))
                anim.SetBool("isMove", false);
            
            
            
        }
        
       
    }
}
