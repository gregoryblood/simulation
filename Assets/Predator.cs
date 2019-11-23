using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{

    public float speed;
    public float efficiency;
    public float energy;
    public float strtenergy;
    public float birthtotal;
    public int gen = 0;
    public bool mutate = true;
    public float bodysize;

    private Vector3 target;
    private GameObject[] foods;
    private GameObject food = null;

    private Vector3 position;
    private Vector3 startpos;
    private bool nofood = false;
    private bool dead = false;

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
            .material.SetColor("_BaseColor", Random.ColorHSV(0f, 0.5f, 1f, 1f, 1f, 1f));
            mutate = false;

            speed = Random.Range(1f, 12f);
            strtenergy = Random.Range(1f, 12f);
            energy = strtenergy; //Starting energy for the day
            birthtotal = strtenergy + Random.Range(1f, 12f);
            bodysize = Random.Range(1f, 4f);

        }
        if (gen == 0)
        {
            gameObject.transform.GetChild(0).GetComponent<Renderer>()
            .material.SetColor("_BaseColor", Random.ColorHSV(0f, 0.5f, 1f, 1f, 1f, 1f));
            mutate = false;

            speed = Random.Range(2f, 12f);
            strtenergy = Random.Range(24f, 50f);
            energy = strtenergy; //Starting energy for the day
            birthtotal = strtenergy + Random.Range(24f, 50f);
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
        efficiency = (bodysize * bodysize * bodysize) * (speed * speed) / 100f;
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
        kid.GetComponent<Predator>().gen = gen + 1;
        if (Random.Range(0f, 10f) > 9f)
        {
            kid.GetComponent<Predator>().mutate = true;
        }
        else
        {
            kid.GetComponent<Predator>().speed = speed;
            kid.GetComponent<Predator>().efficiency = efficiency;
            kid.GetComponent<Predator>().strtenergy = strtenergy;
            kid.GetComponent<Predator>().birthtotal = birthtotal;
        }

    }
    private void Findclosestfood()
    {

        foods = GameObject.FindGameObjectsWithTag("creature");
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
            if (bodysize / t.GetComponent<Movement>().bodysize > 1.2f)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    food = t;
                    minDist = dist;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "creature" && !dead)
        {
            if (bodysize / other.gameObject.GetComponent<Movement>().bodysize > 1.2f)
            {
                energy += other.gameObject.GetComponent<Movement>().energy * ( other.gameObject.GetComponent<Movement>().bodysize *
                            other.gameObject.GetComponent<Movement>().bodysize *
                            other.gameObject.GetComponent<Movement>().bodysize);
                other.gameObject.GetComponent<Movement>().dead = true;
            }
            

        }

    }
}
