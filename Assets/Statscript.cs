using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statscript : MonoBehaviour
{

    public Text txtstats;
    public int boards;

    private float avgspeed = 0f;
    private float avgeffc = 0f;
    private float avgstrtenergy = 0;
    private float avgbirthtotal = 0;
    private float total = 0;
    private float ttleffc = 0;
    private float ttlspeed = 0;
    private float ttlstrtenergy = 0;
    private float ttlbirthtotal = 0;

    private void Start()
    {
        enabled = false;
    }
    public void Enablestats()
    {
        enabled = true;
    }
    public void UpdateBoards(int inboards)
    {
        boards = inboards;
    }

    public void NewLife (float speed, float effc, float strtenergy, float birthtotal)
    {
        total++;

        ttlspeed += speed;
        ttleffc += effc;
        ttlstrtenergy += strtenergy;
        ttlbirthtotal += birthtotal;
        avgspeed = ttlspeed / total;
        avgeffc = ttleffc / total;
    }
    public void NewDeath(float speed, float effc, float strtenergy, float birthtotal)
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
        txtstats.text = "Time Scale: " + Time.timeScale + "\nAvg Total per Environment: " + (total / boards).ToString("F2") + " || Avg Speed: " + avgspeed.ToString("F2") + " || Avg Energy per Second: " + (0.1f + avgeffc).ToString("F2") +
                        "\nTotal Energy to Start: " + avgstrtenergy.ToString("F2") + " || Total Energy needed for Birth: " + avgbirthtotal.ToString("F2");
        
    }
}
