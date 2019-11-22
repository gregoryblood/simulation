using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statscript : MonoBehaviour
{

    public Text txtstats;

    private float avgspeed = 0f;
    private float avgeffc = 0f;
    private float total = 0;
    private float starting = 0;
    private float ttleffc = 0;
    private float ttlspeed = 0;
    private float survivalrate = 0;
    

    public void UpdateStart ()
    {
        starting++;
    }
    public void UpdateStats(float speed, float effc)
    {
        total++;
        ttlspeed += speed;
        ttleffc += effc;

        avgspeed = ttlspeed / total;
        

        avgeffc = ttleffc / total;


        survivalrate = total / starting;
        
        survivalrate *= 100;

        txtstats.text = "Avg Speed: " +  Mathf.Round(avgspeed * 100f) / 100f + " Avg Efficiency: " + Mathf.Round(avgeffc * 100f) / 100f + " Avg Survival Rate: " + Mathf.Round(survivalrate * 100f) / 100f + "%";
        
    }
}
