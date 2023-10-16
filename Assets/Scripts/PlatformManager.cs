using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    float currenttime = 0;
    float targetTime = 0.52f;

    public List<GameObject> spawnpoints = new List<GameObject>();
    public List<GameObject> platformobjs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        if (currenttime > targetTime)
        {
            currenttime = 0;
            int spawncount = 0;
            int tempcount = Random.Range(1, 9);
            if (tempcount >= 1 && tempcount <= 4) { spawncount = 1; }
            else if (tempcount >= 5 && tempcount <= 7) { spawncount = 2; }
            else if (tempcount == 8) { spawncount = 3; }
            Startspawn(spawncount);
        }
    }
    void Startspawn(int numspawn)
    {
        if (numspawn == 3)
        {
            for (int a = 0; a < numspawn; a++)
            {
                GameObject tospawn = spawnpoints[a];
                GameObject chosenplat = platformobjs[Random.Range(0, 13)];
                SpawnIn(tospawn, chosenplat);
            }
        }
        else
        {
            int temp1 = 0;
            int temp2 = 0;
            int choice = 0;

            for (int i = 0; i < numspawn; i++)
            {
                while (choice == temp1 || choice == temp2)
                {
                    int tempchoice = Random.Range(1, 8);
                    if (tempchoice >= 1 && tempchoice <= 3) { choice = 1; }
                    if (tempchoice == 4 || tempchoice == 6) { choice = 2; }
                    if (tempchoice == 7) { choice = 3; }
                }
                temp1 = choice;
                GameObject tospawn = spawnpoints[choice - 1];
                GameObject chosenplat = platformobjs[Random.Range(0, 6)];
                SpawnIn(tospawn, chosenplat);
                choice = 0;
            }

        }
    }
    void SpawnIn(GameObject spawner, GameObject spawnedplat)
    {
        Instantiate(spawnedplat, spawner.transform);
    }
}
