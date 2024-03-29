﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float efficiency;
    public float energy;
    public float strtenergy;
    public float birthtotal;
    public int gen = 0;
    public bool mutate = true;
    public float bodysize = 1f;
    public bool dead = false;

    private Vector3 target;
    private GameObject[] foods;
    private GameObject food = null;

    private Vector3 position;
    private Vector3 startpos;
    private bool nofood = false;
    

    private Statscript stats;
    private float corpsetime = 3f;
    private bool reportdeath = false;
    private float eps = 0f;
 


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("STATS").GetComponent<Statscript>();

        
        if (mutate)
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>()
            .material.SetColor("_BaseColor", Random.ColorHSV(0.5f, 1f, 1f, 1f, 1f, 1f));
            mutate = false;


            speed = Random.Range(0.1f, 12f); //Speed they travel

            strtenergy = Random.Range(5f, 12f);
            energy = strtenergy; //Starting energy for the day
            birthtotal = strtenergy + Random.Range(1f, 12f);
            bodysize = Random.Range(1f, 4f);

        }
        if (gen == 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>()
            .material.SetColor("_BaseColor", Random.ColorHSV(0f, 0.5f, 1f, 1f, 1f, 1f));
            mutate = false;

            speed = Random.Range(1f, 12f);
            strtenergy = Random.Range(5f, 12f);
            energy = strtenergy; //Starting energy for the day
            birthtotal = strtenergy + Random.Range(1f, 12f);
            bodysize = Random.Range(1f, 4f);
        }
        else
        {
            
            speed *= Random.Range(0.9f, 1.1f); //Speed they travel

            strtenergy *= Random.Range(0.9f, 1.1f);

            energy = strtenergy; //Starting energy for the day
            birthtotal *= Random.Range(0.9f, 1.1f);
            bodysize *= Random.Range(0.9f, 1.1f);

            if (bodysize < 1f)
                bodysize = 1f;

            if (birthtotal <= strtenergy)
                birthtotal = strtenergy + 1;


        }
        transform.localScale = new Vector3(bodysize, bodysize, bodysize);
        efficiency = (bodysize * bodysize * bodysize) * (speed * speed) / 100f ;
        stats.NewLife(speed, efficiency, strtenergy, birthtotal);
    }
    
    // Update is called once per frame
    void Update()
    {

        //Starvation
        energy -= Time.deltaTime * (0.1f + efficiency);
        if (energy <= 0)
            dead = true;

        if (dead)
        {
            if (!reportdeath)
            {
                gameObject.tag = "corpse";
                stats.NewDeath(speed, efficiency, strtenergy, birthtotal);
                reportdeath = true;
            }
            
            target = transform.position;

            transform.Translate(Vector3.down * Time.deltaTime);
            corpsetime -= Time.deltaTime;
            
            if (corpsetime < 0)
            {
                
                Destroy(gameObject);
            }
            return;
        }

        float step = speed * Time.deltaTime; // calculate distance to move

        if (energy > birthtotal)
        {
            CreateGen();
            energy -= strtenergy;
        }

        Findclosestfood();

        if (!nofood && food)
        {
            target = food.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            target.y = transform.position.y;
            transform.LookAt(target);
        }

        

    }
    private void CreateGen()
    {
        GameObject kid = Instantiate(gameObject, transform.position, Quaternion.identity);
        kid.GetComponent<Movement>().gen = gen + 1;
        if (Random.Range(0f, 10f) > 9f)
        {
            kid.GetComponent<Movement>().mutate = true;
        }
        else
        {
            kid.GetComponent<Movement>().speed = speed;
            kid.GetComponent<Movement>().efficiency = efficiency;
            kid.GetComponent<Movement>().strtenergy = strtenergy;
            kid.GetComponent<Movement>().birthtotal = birthtotal;
        }

    }
    private void Findclosestfood()
    {

        foods = GameObject.FindGameObjectsWithTag("food");
        if (foods == null)
        {
            nofood = true;
            food = null;
            return;
        }
        nofood = false;
        food = null;
        float minDist = bodysize * 50;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in foods)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                food = t;
                minDist = dist;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "food" && !dead)
        {
            energy++;
            Destroy(other.gameObject);

        }

    }
}
