using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnit : MonoBehaviour {

    private MainGame mainGame;
    GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        mainGame = Camera.main.GetComponent<MainGame>();
        spawnPoint = GameObject.Find("SpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn(int LV)
    {
        GameObject unit = mainGame.AddUnit(LV);

        if (unit != null)
        {
            unit.transform.position = spawnPoint.transform.position;
            Camera.main.GetComponent<SpawnTower>().SpawnUnitcount++;
            unit.transform.SetParent(GameObject.Find("Players").transform);
        }
    }

    public void Spawn(Image LV)
    {
        if (LV.color == Color.gray)
            return;

        GameObject unit = mainGame.AddUnit(int.Parse(LV.name.Split('r')[1]));

        if (unit != null)
        {
            unit.transform.position = spawnPoint.transform.position;
            Camera.main.GetComponent<SpawnTower>().SpawnUnitcount++;
            unit.transform.SetParent(GameObject.Find("Players").transform);
            object[] param = new object[2] {LV, 2.0f};
            StartCoroutine(MainGame.CoolDown(param));
        }
    }

    public void SpawnTower(int LV, Vector3 position)
    {
        GameObject tower = Instantiate(Resources.Load("Prefab/EnemyPrefabs/TowerLV" + LV)) as GameObject;
        mainGame.AddTower(tower);

        if (tower != null)
        {
            tower.transform.position = position;

            tower.transform.SetParent(GameObject.Find("Towers").transform);
        }
    }
}
