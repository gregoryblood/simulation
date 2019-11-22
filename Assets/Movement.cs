using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    public float speed = 0;

    public int needed = 0;
    public float efficiency = 0;
    public int energy = 0;
    public Material deadmat;

    private Vector3 target;
    private GameObject[] foods;
    private int numate = 0;
    private GameObject food = null;
    private float distance = Mathf.Infinity;
    private Vector3 position;
    private Vector3 startpos;
    private bool nofood = false;
    private bool done = false;
    private bool reported = false;
    private bool dead = false;

    private float nextenergysub = 0f;
    private Statscript stats;


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("STATS").GetComponent<Statscript>();
        position = transform.position;
        startpos = position;
        Findclosestfood();
        target = food.transform.position;

        
        speed = Random.Range(1, 6); //Speed they travel
        needed = Mathf.RoundToInt(speed); //How much they need to eat
        efficiency = (6 - speed); //Rate at which they lose energy
        energy = 3; //Starting energy for the day

        stats.UpdateStart();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        target.y = transform.position.y;
        transform.LookAt(target);

        //Starvation
        if (Time.time > nextenergysub)
        {
            nextenergysub += efficiency;
            energy--;
            if (energy == 0)
                dead = true;
        }
        if (!food && nofood && numate < needed)
        {
            dead = true;
        }
        if (food && numate < needed)
        {
            target = food.transform.position;
        }
        else if (!food && numate < needed && !nofood)
        {
            Findclosestfood();
        }
        else if (numate >= needed)
        {
            target = startpos;
            done = true;
        }
        if (dead && !done)
        {
            speed = 0;
            var child = transform.GetChild(0);

            child.GetComponent<Renderer>().material = deadmat;
        }
        
        if (transform.position == startpos && done && !reported)
        {
            
            stats.UpdateStats(speed, efficiency);
            reported = true;
        }

    }
    private void Findclosestfood()
    {
        distance = Mathf.Infinity;
        foods = GameObject.FindGameObjectsWithTag("food");
        if (foods.Length == 0)
        {
            nofood = true;
            food = null;
            return;
        }
        foreach (GameObject fooditem in foods)
        {
            Vector3 diff = fooditem.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                food = fooditem;
                distance = curDistance;
            }
        }   
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            energy++;
            numate++;
            Destroy(other.gameObject);
        }
            
    }
}
