using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonEvent : MonoBehaviour {

    public Sprite MuteSprite;
    public Sprite SoundSprite;
    public UpgradeList upgradeList;
    MainGame mainGame;
    public bool UseSkill_Barrier = false;
    public bool UseSkill_SpeedUp = false;

    // Use this for initialization
    void Start () {
        mainGame = Camera.main.GetComponent<MainGame>();
        upgradeList = new UpgradeList(FileLoader.Load());
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Pause()
    {
        Camera.main.GetComponent<MainGame>().IsGamePaused = true;
    }

    public void KeepGoing()
    {
        Camera.main.GetComponent<MainGame>().IsGamePaused = false;
    }

    public void Mute()
    {
        if (!AudioListener.pause)
        {
            //볼륨 0 , UI 이미지 전환
            AudioListener.pause = true;

            Image image = GameObject.Find("Mute").GetComponent<Image>();

            image.sprite = MuteSprite;
        }
        else
        {
            AudioListener.pause = false;

            Image image = GameObject.Find("Mute").GetComponent<Image>();

            image.sprite = SoundSprite;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    public void Home()
    {
        KeepGoing();
        SceneManager.LoadScene("StageSelect");
    }

	public void Barrier(Image image)
    {
        if (image.color == Color.gray)
            return;
        if (UseSkill_Barrier || mainGame.Money < 500)
        {
            return;
        }
        mainGame.Money -= 500;
        UseSkill_Barrier = true;
        object[] param = new object[2] {image, 15.0f};
        StartCoroutine(MainGame.CoolDown(param));
        StartCoroutine(CoolDown((x) => { UseSkill_Barrier = x; }));
    }

    public void SpeedUp(Image image)
    {
        if (image.color == Color.gray)
            return;
        if (UseSkill_SpeedUp || mainGame.Money < 700)
            return;

        mainGame.Money -= 500;
        UseSkill_SpeedUp = true;
        object[] param = new object[2] { image, 15.0f };
        StartCoroutine(SpeedUp());
        StartCoroutine(MainGame.CoolDown(param));
        StartCoroutine(CoolDown((x) => { UseSkill_SpeedUp = x; }));
    }

    IEnumerator CoolDown(Action<bool> UseSkill)
    {

		yield return new WaitForSeconds(3f + upgradeList.Duration * 0.5f);

        UseSkill(false);
    }

    IEnumerator SpeedUp()
    {
        Dictionary<Movement, float> speed = new Dictionary<Movement, float>();
        mainGame.Units.ForEach(unit =>
        {
            if (unit)
            {
                Movement tmp = unit.transform.GetChild(0).GetComponent<Movement>();

                if (tmp.MoveSpeed != 0)
                {
                    speed.Add(tmp, tmp.MoveSpeed);
                    tmp.MoveSpeed += 2;
                }
                else
                {
                    speed.Add(tmp, 0);
                }
            }
        });
        yield return new WaitForSeconds(3f + upgradeList.Duration * 0.5f);

        mainGame.Units.ForEach(unit =>
        {
            if (unit)
            {
                float tmp2;
                Movement tmp = unit.transform.GetChild(0).GetComponent<Movement>();

                speed.TryGetValue(tmp, out tmp2);
                if (tmp2 != 0)
                {
                    tmp.MoveSpeed -= 2;
                }
            }
        });

        yield return null;
    }

    public void Shop()
    {
        SceneManager.LoadScene(1);
    }
    
}
