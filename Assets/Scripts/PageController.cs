using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    [SerializeField]
    GameObject openingMenu;
    [SerializeField]
    GameObject backgroundMenu;
    [SerializeField]
    GameObject instructionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        openOpeningMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openOpeningMenu()
    {
        openingMenu.SetActive(true);
        backgroundMenu.SetActive(false);
        instructionsMenu.SetActive(false);
    }

    public void openBackgroundMenu()
    {
        openingMenu.SetActive(false);
        backgroundMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }

    public void openInstructionsMenu()
    {
        openingMenu.SetActive(false);
        backgroundMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void startGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exit()
    {
        Application.Quit();
    }
}
