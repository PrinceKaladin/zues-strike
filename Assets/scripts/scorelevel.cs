using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorelevel : MonoBehaviour
{
    public Text score;
    public Text bestscore;
    private void Start()
    {
        score.text = PlayerPrefs.GetInt("last").ToString();
        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("bestscore")) {
            PlayerPrefs.SetInt("bestscore", PlayerPrefs.GetInt("score"));
        }
        bestscore.text = PlayerPrefs.GetInt("bestscore").ToString();
     
        
    }
}
