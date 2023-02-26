using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Bike : MonoBehaviour
{
    [SerializeField] float steerSpeed = 200f;
    public float moveSpeed = 5f;
    [SerializeField] float boostSpeed = 3f;
    [SerializeField] int Lapcounter = 0;
    [SerializeField] Text Lap1;
    [SerializeField] float time;
    [SerializeField] bool startime1 = false;
    [SerializeField] Text Lap2;
    [SerializeField] float time2;
    [SerializeField] bool startime2 = false;
    [SerializeField] Text Lap3;
    [SerializeField] float time3;
    [SerializeField] bool startime3 = false;
    [SerializeField] Text Total_Time;
    [SerializeField] float sum;
    [SerializeField] GameObject oil;
    [SerializeField] GameObject FallenBarrelRed;
    [SerializeField] GameObject FallenBarrelBlue;
    [SerializeField] bool passed;
    
    // Start is called before the first frame update
    void Start()
    {
        Total_Time.gameObject.SetActive(false);
        oil.SetActive(false);
        FallenBarrelRed.SetActive(false);
        FallenBarrelBlue.SetActive(false);
        passed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0,0,-steerSpeed*Input.GetAxis("Horizontal")*Time.deltaTime);
        transform.Translate(0, moveSpeed * Input.GetAxis("Vertical")*Time.deltaTime, 0);
        if(startime1==true)
        { time = time + Time.deltaTime; }
        Lap1.text = "Lap1:" + time;
        if (startime2 == true)
        { time2 = time2 + Time.deltaTime; }
        Lap2.text = "Lap2:" + time2;
        if (startime3 == true)
        { time3 = time3 + Time.deltaTime; }
        Lap3.text = "Lap3:" + time3;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Y))
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Start")
        { if (Lapcounter == 0)
            { Lapcounter++; startime1 = true; }
        else if(Lapcounter == 1 && passed==true)
            { Lapcounter++; startime1 = false; }
        else if (Lapcounter == 2 && passed == true)
            { Lapcounter++;}
        else if (Lapcounter == 3 && passed == true)
            { Lapcounter++; }
        moveSpeed = 5;GetComponent<SpriteRenderer>().color = Color.white; }
        if(Lapcounter==1 && collision.gameObject.tag=="passed")
        {
            passed = true;
        }
        if (Lapcounter == 2 && passed==true)
        {
            startime2 = true;
            startime1 = false;
            passed = false;
        }
        if(Lapcounter == 2 && collision.gameObject.tag=="passed")
        {
            passed = true;
        }
        if (Lapcounter == 3 && passed==true)
        {
            startime3 = true;
            startime2 = false;
            startime1 = false;
            passed=false;
            
        }
        if (Lapcounter>3 && passed==true)
        {
            startime3 = false;
            startime2 = false;
            startime1 = false;
            if (collision.gameObject.tag == "Start")
            {
                Total_Time.gameObject.SetActive(true);
                sum = time + time2 + time3;
                Total_Time.text = "Total Time: " + sum;
            }
        }
        if (Lapcounter == 1)
        {
            if (collision.gameObject.tag == "Boost")
            {
                moveSpeed = moveSpeed + boostSpeed;
                collision.gameObject.SetActive(false);
            }
        }
        if (Lapcounter == 2)
        {
            if (collision.gameObject.tag == "Boost2")
            {
                moveSpeed = moveSpeed + boostSpeed;
                collision.gameObject.SetActive(false);
            }
        }
        if (Lapcounter == 3)
        {
            if (collision.gameObject.tag == "Boost3")
            {
                moveSpeed = moveSpeed + boostSpeed;
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "RedBarrel")
        {
            collision.gameObject.SetActive(false);
            FallenBarrelRed.SetActive(true);
            FallenBarrelRed.GetComponent<SpriteRenderer>().sortingOrder = 4;
            FallenBarrelRed.transform.position = new Vector3(collision.gameObject.transform.position.x,
                collision.gameObject.transform.position.y,0);
            oil.SetActive(true);
            oil.transform.position = new Vector3(collision.gameObject.transform.position.x+2,
                collision.gameObject.transform.position.y, 0);
            Instantiate(FallenBarrelRed);
            Instantiate(oil);
            oil.SetActive(false);
        }
        if (collision.gameObject.tag == "BlueBarrel")
        {
            collision.gameObject.SetActive(false);
            FallenBarrelBlue.SetActive(true);
            FallenBarrelBlue.GetComponent<SpriteRenderer>().sortingOrder = 4;
            FallenBarrelBlue.transform.position = new Vector3(collision.gameObject.transform.position.x,
                collision.gameObject.transform.position.y, 0);
            oil.SetActive(true);
            oil.transform.position = new Vector3(collision.gameObject.transform.position.x,
                collision.gameObject.transform.position.y+1, 0);
            Instantiate(FallenBarrelBlue);
            Instantiate(oil);
        }
        if (collision.gameObject.tag == "Oil" && GetComponent<SpriteRenderer>().color == Color.gray)
        {
            moveSpeed = moveSpeed;
        }
        if (collision.gameObject.tag == "Oil" && GetComponent<SpriteRenderer>().color == Color.white)
        {
            moveSpeed = moveSpeed - 3f ;
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (GetComponent<SpriteRenderer>().color == Color.gray && collision.gameObject.tag == "Dirt Road")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            moveSpeed = 5f;
            moveSpeed = moveSpeed - 2f;
            passed = true;
        }
        else if (collision.gameObject.tag=="Dirt Road" && GetComponent<SpriteRenderer>().color == Color.white)
        {
            moveSpeed = moveSpeed - 2f;
            passed=true;
        }
        if(collision.gameObject.tag == "NormalRoad")
        {
            moveSpeed = 5f;
            passed = true;
        }
    }
    private void OnMouseDown()
    {
        if (Lapcounter>3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
