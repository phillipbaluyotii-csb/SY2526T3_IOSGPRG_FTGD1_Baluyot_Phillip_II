using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void SelectDefault()
    {
        CharacterManager.SelectedCharacter =
            CharacterType.Default;

        SceneManager.LoadScene("MainGame");
    }

    public void SelectTank()
    {
        CharacterManager.SelectedCharacter =
            CharacterType.Tank;

        SceneManager.LoadScene("MainGame");
    }

    public void SelectSpeed()
    {
        CharacterManager.SelectedCharacter =
            CharacterType.Speed;

        SceneManager.LoadScene("MainGame");
    }
}