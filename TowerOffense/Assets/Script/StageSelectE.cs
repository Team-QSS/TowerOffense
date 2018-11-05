using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectE : MonoBehaviour
{
    private void Start()
    {
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
}
