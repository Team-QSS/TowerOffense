using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectE : MonoBehaviour
{
    KeyCode[] keycode;

    private void Start()
    {
        keycode = new KeyCode[10];
        keycode[0] = KeyCode.UpArrow;
        keycode[1] = KeyCode.UpArrow;
        keycode[2] = KeyCode.DownArrow;
        keycode[3] = KeyCode.DownArrow;
        keycode[4] = KeyCode.LeftArrow;
        keycode[5] = KeyCode.RightArrow;
        keycode[6] = KeyCode.LeftArrow;
        keycode[7] = KeyCode.RightArrow;
        keycode[8] = KeyCode.B;
        keycode[9] = KeyCode.A;

        Time.timeScale = 1;

        SaveData d = FileLoader.Load();
        FileLoader.Save(d);
    }

    public void StageSelect(int num)
    {
        SceneManager.LoadScene("Stage" + num.ToString());
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToStageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void Hidden()
    {
        StartCoroutine(Command());
    }

    IEnumerator Command()
    {
        int i = 0;
        while(true)
        {
            if(Input.GetKey(keycode[i]))
            {
                i++;
                if(i == 10)
                {
                    break;
                }
            }
            else
            {
                StopCoroutine(Command());
            }
            yield return null;
        }

        SceneManager.LoadScene("Shop");
    }
}
