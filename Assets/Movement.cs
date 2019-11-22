using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public float efficiency;
    public int energy;
    public int strtenergy;
    public int birthtotal;
    public int gen = 0;



    public Material deadmat;

    private Vector3 target;
    private GameObject[] foods;
    private GameObject food = null;

    private Vector3 position;
    private Vector3 startpos;
    private bool nofood = false;
    private bool dead = false;

    public float nextenergysub = 0f;
    private Statscript stats;
    private float corpsetime = 3f;

 


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("STATS").GetComponent<Statscript>();

        nextenergysub = Time.time;
        if (gen == 0)
        {

            speed = Random.Range(1, 6); //Speed they travel
            efficiency = (6 - speed); //Rate at which they lose energy
            strtenergy = 4;
            energy = strtenergy; //Starting energy for the day
            birthtotal = 10;
            

        }
        else
        {
            
            speed += Random.Range(-1, 1); //Speed they travel
            if (speed > 6)
                speed = 6;
            efficiency = (6 - speed); //Rate at which they lose energy
            strtenergy += Random.Range(-1, 1);
            energy = strtenergy; //Starting energy for the day
            birthtotal += Random.Range(-1, 1);
            
            
        }

        stats.NewLife(speed, efficiency, strtenergy, birthtotal);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        float step = speed * Time.deltaTime; // calculate distance to move

        if (energy == birthtotal)
        {
            CreateGen();
            energy -= strtenergy;
        }

        Findclosestfood();

        if (!nofood && food && !dead)
        {
            target = food.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            target.y = transform.position.y;
            transform.LookAt(target);
        }

        //Starvation
        if (Time.timeSinceLevelLoad > nextenergysub)
        {
            nextenergysub += efficiency;
            energy--;
            if (energy == 0)
                dead = true;
        }

        if (dead)
        {
            speed = 0;
            var child = transform.GetChild(0);
            child.GetComponent<Renderer>().material = deadmat;
            corpsetime -= Time.deltaTime;
            if (corpsetime < 0)
            {
                stats.NewDeath(speed, efficiency, strtenergy, birthtotal);
                Destroy(gameObject);

            }

        }

    }
    private void CreateGen()
    {
        GameObject kid = Instantiate(gameObject, transform.position, Quaternion.identity);
        kid.GetComponent<Movement>().gen = gen + 1;
        kid.GetComponent<Movement>().speed = speed;
        kid.GetComponent<Movement>().efficiency = efficiency;
        kid.GetComponent<Movement>().strtenergy = strtenergy;
        kid.GetComponent<Movement>().birthtotal = birthtotal;
    }
    private void Findclosestfood()
    {

        foods = GameObject.FindGameObjectsWithTag("food");
        if (foods.Length == 0)
        {
            nofood = true;
            food = null;
            return;
        }
        nofood = false;
        food = null;
        float minDist = Mathf.Infinity;
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
