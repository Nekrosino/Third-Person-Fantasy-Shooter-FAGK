using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneSwitcher2 : MonoBehaviour
{
    // Nazwy scen do prze��czania
    public string sceneName1 = "Level 1";
    public string sceneName2 = "Level 2";

    // Klawisz do zmiany sceny 
    public KeyCode switchKey = KeyCode.P;

    // Zmienna przechowuj�ca informacj� o aktualnej scenie
    private bool isScene1Active = true;

    void Update()
    {
        // Sprawdzenie, czy zosta� wci�ni�ty klawisz
        if (Input.GetKeyDown(switchKey))
        {
            // Sprawdzenie, kt�ra scena jest aktualnie aktywna
            if (isScene1Active)
            {
                SceneManager.LoadScene(sceneName1); // �adowanie sceny 2
            }
            else
            {
                SceneManager.LoadScene(sceneName2); // �adowanie sceny 1
            }

            // Odwr�cenie warto�ci zmiennej, aby prze��cza� mi�dzy scenami
            isScene1Active = !isScene1Active;
        }
    }
}
