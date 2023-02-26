using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject Bike;
    public int count = 0;
    public bool zoomout = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (zoomout==false)
        {
            Vector3 v3 = new Vector3(Bike.transform.position.x, Bike.transform.position.y, -10);
            transform.position = v3;
        }
        if (Input.GetKeyUp(KeyCode.V) && zoomout == false)
        {
            zoomout = true;
            GetComponent<Camera>().orthographicSize = 10;
            Vector3 v1 = new Vector3(-1.553597f, -2.815471f, -10);
            transform.position = v1;
        }
        else if (Input.GetKeyUp(KeyCode.V) && zoomout == true)
        {
            GetComponent<Camera>().orthographicSize = 5;
            Vector3 v3 = new Vector3(Bike.transform.position.x, Bike.transform.position.y, -10);
            transform.position = v3;
            zoomout = false;
        }
    }

}
