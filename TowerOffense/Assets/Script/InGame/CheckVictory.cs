using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckVictory : MonoBehaviour {

    PlayTime playTime;
    public Text vicOrDef;
    public int goalUnitCount = 0;
    public int goalUnit;
    public Slider HP;

	// Use this for initialization
	void Start () {
        HP=GameObject.Find("HP").GetComponent<Slider>();
        playTime = GetComponent<PlayTime>();
	}
	
	// Update is called once per frame
	void Update () {
        HP.value=10-goalUnitCount;
        if (playTime.playTime <= 0f)
        {
            Defeat();
        }
        else if (goalUnitCount >= goalUnit)
        {
            Victory();
        }
	}

    void Defeat()
    {
        vicOrDef.text = "DEFEAT";
        vicOrDef.gameObject.SetActive(true);
        Time.timeScale = 0;
       
        Camera.main.GetComponent<MainGame>().IsGameEnd = true;
    }

    void Victory()
    {
        vicOrDef.text = "VICTORY";
        vicOrDef.gameObject.SetActive(true);
        Time.timeScale = 0;
        SaveData sv = FileLoader.Load();
        sv.UpgradeCount += 3;
        sv.Save();
        Camera.main.GetComponent<MainGame>().IsGameEnd = true;
    }
}
