using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int foodnum;
    public int creaturenum;
    public int size;

    public GameObject food;
    public GameObject creature;
    public GameObject floor;

    
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 position;
        int edge;
        
        GameObject newfloor = Instantiate(floor, transform.position, Quaternion.identity);
        newfloor.transform.localScale = new Vector3(size, 0.5f, size);

        //Spawn Food
        for (int i = 0; i < foodnum; i++)
        {
            position = new Vector3(Random.Range(-size / 2.1f, size / 2.1f), 0.3f, Random.Range(-size / 2.1f, size / 2.1f));
            Instantiate(food, position + transform.position, Quaternion.identity);
        }
        
        //Spawn Creatures
        for (int i = 0; i < creaturenum; i++)
        {
            edge = Random.Range(0, 4);
            if (edge == 0)
            {
                position = new Vector3(Random.Range(-size / 2, size / 2), 0.8f, -size / 2);
                Instantiate(creature, position + transform.position, Quaternion.identity);
            }
            if (edge == 1)
            {
                position = new Vector3(Random.Range(-size / 2, size / 2), 0.8f, size / 2);
                Instantiate(creature, position + transform.position, Quaternion.identity);
            }
            if (edge == 2)
            {
                position = new Vector3(-size / 2, 0.8f, Random.Range(-size / 2, size / 2));
                Instantiate(creature, position + transform.position, Quaternion.identity);
            }
            if (edge == 3)
            {
                position = new Vector3(size / 2, 0.8f, Random.Range(-size / 2, size / 2));
                Instantiate(creature, position + transform.position, Quaternion.identity);
            }
        }


    }

}
