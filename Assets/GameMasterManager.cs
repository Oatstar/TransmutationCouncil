using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : MonoBehaviour
{
    public float timer = 0;
    float hintInterval = 10f;

    private void Start()
    {
        TriggerRandomHint();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= hintInterval)
        {
            timer = 0;
            TriggerRandomHint();
        }
    }

    void TriggerRandomHint()
    {
        int randomVal = UnityEngine.Random.Range(0, 100);

        if (randomVal < 5)
            LogTextManager.instance.ShowTierHint(3);
        else
            LogTextManager.instance.ShowTierHint(2);
    }
}
