using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject creditsScreen;
    private bool creditsActive = false;
    public GameObject tutorialScreen;
    private bool tutorialActive = false;
    [SerializeField] private AudioClip startGameSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip buttonSound;

    private void Start()
    {
        HookButtons();
        AudioManager.Instance.PlayMenuMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsActive)
            {
                Credits();
            }
            else if (tutorialActive)
            {
                Tutorial();
            }
        }
    }

    private void HookButtons()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            if (btn.name == "Button_Start") continue;

            btn.onClick.AddListener(() => {
                AudioManager.Instance.PlaySFX(buttonSound);
            });

            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((_) =>
            {
                AudioManager.Instance.PlaySFX(hoverSound);
            });

            trigger.triggers.Add(entry);
        }
    }
    
    public void PlayGame()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PlaySFX(startGameSound);      
        //Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I QUIT THE GAME");
    }

    public void Credits()
    {
        if (creditsActive)
        {
            creditsScreen.gameObject.SetActive(false);
            creditsActive = false;
        }
        else
        {
            tutorialScreen.gameObject.SetActive(false);
            tutorialActive = false;
            creditsScreen.gameObject.SetActive(true);
            creditsActive = true;
        }
    }

    public void Tutorial()
    {
        if (tutorialActive)
        {
            tutorialScreen.gameObject.SetActive(false);
            tutorialActive = false;
        }
        else
        {
            creditsScreen.gameObject.SetActive(false);
            creditsActive = false;
            tutorialScreen.gameObject.SetActive(true);
            tutorialActive = true;
        }
    }
}
