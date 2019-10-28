using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DataHelper : MonoBehaviour {

	public static DataHelper Instance { set; get; }

    public BitArray UnlockedLevel { set; get; }
    public int CurrentLevel { set; get; }

    public List<Level> Levels; 

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // load the previous save
        Load();


        // load the menu scene
        SceneManager.LoadScene("Menu");
    }


    public void Save()
    {
        string saveString = "";

        for (int i = 0; i < UnlockedLevel.Count; ++i) {
            saveString = UnlockedLevel.Get(i).ToString();
                }

        //Debug.Log(saveString);
        PlayerPrefs.SetString("saveString", saveString);
    }

    public void Load()
    {
        string loadString = PlayerPrefs.GetString("saveString");
        int i = 0;
        foreach(char c in loadString)
        {
            if (c == '0')
                UnlockedLevel.Set(i, false);
            else
                UnlockedLevel.Set(i, true);
            i++;
        }
    }
}
