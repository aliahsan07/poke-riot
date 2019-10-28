using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private void Start()
    {

    }

    private void UnlockLevels()
    {

    }

    public void ToGame(int levelIndex)
    {
        DataHelper.Instance.CurrentLevel = levelIndex;
        SceneManager.LoadScene("Game");
    }
}
