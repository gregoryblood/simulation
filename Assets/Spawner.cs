using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int foodnum;
    public int creaturenum;
    public int size;
    public float foodrate;
    

    public GameObject food;
    public GameObject creature;
    public GameObject predator;
    public GameObject floor;

    private Vector3 position;
    private float nextfood = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        
        int edge;
        
        GameObject newfloor = Instantiate(floor, transform.position, Quaternion.identity);
        newfloor.transform.localScale = new Vector3(size, 0.5f, size);

        for (int i = 0; i < foodnum; i++)
        {
            position = new Vector3(Random.Range((-size / 2f) + 1, (size / 2f) - 1), 0.3f, Random.Range((-size / 2f) + 1, (size / 2f) - 1));
            Instantiate(food, position + transform.position, Quaternion.identity);
        }


        //Spawn Creatures
        
        for (int i = 0; i < creaturenum; i++)
        {
            bool ispredator = (Random.value > 0.2f);
            edge = Random.Range(0, 4);
            if (edge == 0)
            {
                position = new Vector3(Random.Range(-size / 2, size / 2), 0.5f, -size / 2);
                if (ispredator)
                    Instantiate(creature, position + transform.position/4, Quaternion.identity);
                else
                    Instantiate(predator, position + transform.position, Quaternion.identity);
            }
            if (edge == 1)
            {
                position = new Vector3(Random.Range(-size / 2, size / 2), 0.5f, size / 2);
                if (ispredator)
                    Instantiate(creature, position + transform.position / 4, Quaternion.identity);
                else
                    Instantiate(predator, position + transform.position, Quaternion.identity);
            }
            if (edge == 2)
            {
                position = new Vector3(-size / 2, 0.5f, Random.Range(-size / 2, size / 2));
                if (ispredator)
                    Instantiate(creature, position + transform.position / 4, Quaternion.identity);
                else
                    Instantiate(predator, position + transform.position, Quaternion.identity);
            }
            if (edge == 3)
            {
                position = new Vector3(size / 2, 0.5f, Random.Range(-size / 2, size / 2));
                if (ispredator)
                    Instantiate(creature, position + transform.position / 4, Quaternion.identity);
                else
                    Instantiate(predator, position + transform.position, Quaternion.identity);
            }
        }


    }
    private void Update()
    {
        //Spawn Food
        if (Time.timeSinceLevelLoad > nextfood)
        {
            nextfood += 1/foodrate;
            position = new Vector3(Random.Range ((-size / 2f) + 1, (size / 2f) - 1), 0.3f, Random.Range((-size / 2f) + 1, (size / 2f) - 1));
            Instantiate(food, position + transform.position, Quaternion.identity);
        }
    }

}
