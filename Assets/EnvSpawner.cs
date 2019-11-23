using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvSpawner : MonoBehaviour
{
    public int envnum = 1;
    public int size = 50;
    public int foodnum = 50;
    public int creaturenum = 50;
    public float foodrate = 5;
    public GameObject env;

    public Canvas ui;
    public InputField getCreature;
    public InputField getFood;
    public InputField getSize;
    public InputField getEnvnum;
    public InputField getFoodrate;

    public Button button;

    private Statscript stats;
    private void Start()
    {
        ui.enabled = true;
    }
    public void SetData()
    {
        if (string.IsNullOrEmpty(getCreature.text))
            creaturenum = 25;
        else
            creaturenum = int.Parse(getCreature.text);
        if (string.IsNullOrEmpty(getFood.text))
            foodnum = 50;
        else
            foodnum = int.Parse(getFood.text);
        if (string.IsNullOrEmpty(getSize.text))
            size = 50;
        else
            size = int.Parse(getSize.text);
        if (string.IsNullOrEmpty(getEnvnum.text))
            envnum = 1;
        else
            envnum = int.Parse(getEnvnum.text);
        if (string.IsNullOrEmpty(getFoodrate.text))
            foodrate = 5;
        else
            foodrate = (float.Parse(getFoodrate.text));


        ui.enabled = false;

        Spawn();
        button.enabled = false;
        stats = GameObject.Find("STATS").GetComponent<Statscript>();
        stats.UpdateBoards(envnum * envnum);
        stats.Enablestats();
    }
    
    void Spawn()
    {
        env.GetComponent<Spawner>().size = size;
        env.GetComponent<Spawner>().creaturenum = creaturenum;
        env.GetComponent<Spawner>().foodnum = foodnum;
        env.GetComponent<Spawner>().foodrate = foodrate;




        for (int i = 0; i < envnum; i++)
        {
            for (int j = 0; j < envnum; j++)
            {
                if (envnum >= 0)
                {
                    Instantiate(env, new Vector3(i * (size * 2), 0, j * (size * 2)), Quaternion.identity);
                    envnum--;
                }
                
            }
        }
    }
}
