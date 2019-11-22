using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statscript : MonoBehaviour
{

    public Text txtstats;

    private float avgspeed = 0f;
    private float avgeffc = 0f;
    private float avgstrtenergy = 0;
    private float avgbirthtotal = 0;
    private float total = 0;
    private float ttleffc = 0;
    private float ttlspeed = 0;
    private float ttlstrtenergy = 0;
    private float ttlbirthtotal = 0;



    public void NewLife (float speed, float effc, int strtenergy, int birthtotal)
    {
        total++;
        ttlspeed += speed;
        ttleffc += effc;
        ttlstrtenergy += strtenergy;
        ttlbirthtotal += birthtotal;
        avgspeed = ttlspeed / total;
        avgeffc = ttleffc / total;
    }
    public void NewDeath(float speed, float effc, int strtenergy, int birthtotal)
    {
        total--;
        ttlspeed -= speed;
        ttleffc -= effc;
        ttlstrtenergy -= strtenergy;
        ttlbirthtotal -= birthtotal;
        avgspeed = ttlspeed / total;
        avgeffc = ttleffc / total;
        avgstrtenergy = ttlstrtenergy / total;
        avgbirthtotal = ttlbirthtotal / total;
    }
    public void Update()
    {
        txtstats.text = "Total: " + total + " Avg Speed: " + Mathf.Round(avgspeed * 100f) / 100f + " Avg Efficiency: " + Mathf.Round(avgeffc * 100f) / 100f + 
                        " Total Energy to Start: " + avgstrtenergy + " Total Energy needed for Birth: " + avgbirthtotal;
        
    }
}
