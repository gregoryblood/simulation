﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvSpawner : MonoBehaviour
{
    public int envnumsquared = 1;
    public int size = 50;
    public int foodnum = 50;
    public int creaturenum = 50;
    public GameObject env;

    public Canvas ui;
    public InputField getCreature;
    public InputField getFood;
    public InputField getSize;
    public InputField getEnvnum;


    private void Start()
    {
        ui.enabled = true;
        Time.timeScale = 0f;
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
            envnumsquared = 3;
        else
            envnumsquared = int.Parse(getEnvnum.text);


        ui.enabled = false;

        Spawn();
        Time.timeScale = 1f;


        
    }
    
    void Spawn()
    {
        env.GetComponent<Spawner>().size = size;
        env.GetComponent<Spawner>().creaturenum = creaturenum;
        env.GetComponent<Spawner>().foodnum = foodnum;




        for (int i = 0; i < envnumsquared; i++)
        {
            for (int j = 0; j < envnumsquared; j++)
            {
                Instantiate(env, new Vector3(i * (size * 2), 0, j * (size * 2)), Quaternion.identity);
            }
        }
    }
}