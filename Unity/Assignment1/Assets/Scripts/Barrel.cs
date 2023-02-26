using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject Bike;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (Bike.GetComponent<SpriteRenderer>().color == Color.gray)
        {
            Bike.GetComponent<Bike>().moveSpeed = Bike.GetComponent<Bike>().moveSpeed;
        }
        else
        {
            Bike.GetComponent<SpriteRenderer>().color = Color.gray;
            Bike.GetComponent<Bike>().moveSpeed = Bike.GetComponent<Bike>().moveSpeed - 3;
        }
    }
}
