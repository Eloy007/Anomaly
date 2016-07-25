using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentTest : MonoBehaviour {

    // Use this for initialization
    List<GameObject> cloneObjList;
	void Start () {
        if (name != "CubeFragment") return;
        Vector3 sPos = transform.position;
        float sSize = 1;// + 0.001f;
        cloneObjList = new List<GameObject>();

        for(int c=0;c<3;c++)
        for(int y=0;y<3;y++)
        for (int x = 0; x < 3; x++)
        {
                    if ((x == 0) && (y == 0) && (c == 0))
                        continue;
                    
            GameObject cloneCube= (GameObject)GameObject.Instantiate(gameObject, new Vector3(sPos.x + (sSize) * x, sPos.y+ sSize * y, sPos.z + sSize * c),gameObject.transform.rotation);
                    cloneCube.name = "CubeFragmentClone" + x + "" + y + "" + c;
                    cloneCube.transform.SetParent(this.transform.parent);
                    
            cloneObjList.Add(cloneCube);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
