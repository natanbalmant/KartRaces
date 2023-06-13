using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RaceController : MonoBehaviour
{
    public TextMeshProUGUI[] positionsTexts;
    public static RaceController instance;

    public List<Kart> racers;
    public List<Kart> racersPositions;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SetPositionsText();
        InvokeRepeating("UpdatePositions", 0.5f, 0.5f);
    }

    void UpdatePositions(){
        racersPositions = racers.OrderByDescending(racer => racer.totalCheckpoints).ThenBy(racer => racer.lastTime).ToList();
        SetPositionsText();
    }
    
    void SetPositionsText(){
        for(int i = 0; i< positionsTexts.Length; i++){
            positionsTexts[i].text = racersPositions[i].racerName;
        }
    }
}
