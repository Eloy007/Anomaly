using UnityEngine;
using System.Collections;

public class InputControl  {

    
    

    Vector3 vStart;
    Vector3 vEnd;

    bool doAction=false;

    bool isMouse = false;
    int touchId = -1;


    private float iHeight;
    private float iWidth;
    private float sx;
    private float sy;


    bool fireNow = false;

    public bool getFire()
    {
        if (fireNow)
        {
            fireNow = false;
            return true;
        }
        return false;
    }

    public InputControl(float x,float y,float w,float h)
    {
        iHeight = h;
        iWidth = w;
        sx = x;
        sy = y;
    }

    public Vector3 getStart()
    {
        return new Vector3(vStart.x, vStart.y, vStart.z);
    }

    public Vector3 getEnd()
    {
        return new Vector3(vEnd.x, vEnd.y, vEnd.z);
    }

    public Vector3 getVector()
    {
        
        return vEnd - vStart;
    }

    private void saveStart(float x,float y)
    {


        if ((x > sx) && (y > sy) && (x < (iWidth)) && ((y < (iHeight))))
        {
            vStart = new Vector3(x, y);
            vEnd = new Vector3(x, y);
            doAction = true;
            
        }
    }

    private void setCurrent(float x,float y)
    {
        vEnd.x = x;
        vEnd.y = y;
    }

    
	
	// Update is called once per frame
	public bool Update () {

        if (isMouse)
        {
           // if (isMouse)
               setCurrent(Input.mousePosition.x, Input.mousePosition.y);

            //= new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        }

        else
        {

            

            if (Input.GetMouseButtonDown(0))
            {
                saveStart(Input.mousePosition.x, Input.mousePosition.y);
                isMouse = true;
            }

      



        }


        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch tmpTouch = Input.GetTouch(i);
                switch (tmpTouch.phase)
                {
                    case TouchPhase.Began:
                        float x = tmpTouch.position.x;
                        float y = tmpTouch.position.y;

                        if ((x > sx) && (y > sy) && (x < (iWidth)) && ((y < (iHeight))))
                        {
                            Debug.Log(string.Format("BeganTouchId: {0}, x: {1} y: {2}", touchId, tmpTouch.position.x, tmpTouch.position.y));
                            if (touchId != -1) {
                                fireNow = true;
                                Debug.Log(string.Format("Fire TouchId: {0}, x: {1} y: {2}", touchId, tmpTouch.position.x, tmpTouch.position.y));
                                break;
                                
                            }


                            saveStart(tmpTouch.position.x, tmpTouch.position.y);
                            touchId = tmpTouch.fingerId;
                            
                        }
                        break;
                        
                    case TouchPhase.Moved:
                        if (touchId == -1)break;
                        if (touchId == tmpTouch.fingerId)
                        {
                            setCurrent(tmpTouch.position.x, tmpTouch.position.y);
                            Debug.Log(string.Format("MovedTouchId: {0}, x: {1} y: {2}", touchId, tmpTouch.position.x, tmpTouch.position.y));
                        }
                        break;
                    case TouchPhase.Ended:
                        if (touchId == -1) break;
                        if (touchId== tmpTouch.fingerId)
                        {
                            Debug.Log(string.Format("EndedTouchId: {0}, x: {1} y: {2}", touchId, tmpTouch.position.x, tmpTouch.position.y));
                            touchId = -1;
                            doAction = false;
                            
                        }
                        break;

                        
                }
            }
        }



        if (Input.GetMouseButtonUp(0))
        {
            //vEnd = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            //return true;
            doAction = false;
            isMouse = false;
        }
        return doAction;
    }
}
