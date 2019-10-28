using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    // status text / GAME UI
    public Text CurrentLevelIndex;
    public Text elixitAmountText;
    public GameObject PokemonContainer;
    public bool life = true;
    public static GameManager Instance { set; get; }

    private Level currentLevel;
    public int Elixir {set; get; }


    private void Start()
    {
        Instance = this;

        currentLevel = DataHelper.Instance.Levels[DataHelper.Instance.CurrentLevel];
        Elixir = currentLevel.startingElixir;
        // Game UI
        CurrentLevelIndex.text = "Current Level: " + DataHelper.Instance.CurrentLevel.ToString();
        
        unlockPokemon();
        UpdateElixirText(); 
    }


    private void unlockPokemon()
    {
        
        int i = 0;
        foreach( Transform t in PokemonContainer.transform)
        {
           
            bool  activeButton = ((currentLevel.UnlockedPokemon) & (1 << i)) != 0;
            t.GetComponent<Button>().interactable = activeButton;
            i++;
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void UpdateElixirText()
    {
        elixitAmountText.text = Elixir.ToString();
    }


    public void UpdateLivesUI()
    {

    }

    public void removeLife()
    {
        life = false;
        SceneManager.LoadScene(3);
    }
}
