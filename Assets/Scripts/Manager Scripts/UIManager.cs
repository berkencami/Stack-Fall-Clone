using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject homeUI, inGameUI, finishUI,gameOverUI;
    public GameObject buttons;

    private bool btns;

    [Header("PreGame")]
    public Button soundBtn;
    public Sprite soundOnSpr, SoundOffSpr;

    [Header("inGame")]
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;
    public Text currentLevelText, nextLevelText;

    [Header("Finish")]
    public Text finishLevelText;

    [Header("GameOver")]
    public Text gameOverScoreText;
    public Text gameOverBestText;

    private Player player;

    private Material playerMaterial;
    void Awake()
    {
        player = FindObjectOfType<Player>();
        playerMaterial = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;

        levelSlider.transform.parent.GetComponent<Image>().color = playerMaterial.color + Color.gray;
        levelSlider.color = playerMaterial.color;
        currentLevelImg.color = playerMaterial.color;
        nextLevelImg.color = playerMaterial.color;

        soundBtn.onClick.AddListener(() => SoundManager.Instance.SoundOnOff());
    }

    private void Start()
    {
        currentLevelText.text = FindObjectOfType<LevelSpawner>().level.ToString();
        nextLevelText.text = FindObjectOfType<LevelSpawner>().level + 1 + "";
    }


    void Update()
    {

        if(player.playerState == Player.PlayerState.Prepare)
        {
            if (SoundManager.Instance.sound && soundBtn.GetComponent<Image>().sprite != soundOnSpr)
                soundBtn.GetComponent<Image>().sprite = soundOnSpr;
            else if (!SoundManager.Instance.sound && soundBtn.GetComponent<Image>().sprite != SoundOffSpr)
                soundBtn.GetComponent<Image>().sprite = SoundOffSpr; 
        }
        if(Input.GetMouseButtonDown(0) && !IgnoreUI() &&player.playerState == Player.PlayerState.Prepare)
        {
            player.playerState = Player.PlayerState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
            finishUI.SetActive(false);
            gameOverUI.SetActive(false);
        }
        if (player.playerState == Player.PlayerState.Finish)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(true);
            gameOverUI.SetActive(false);

            finishLevelText.text = "Level " + FindObjectOfType<LevelSpawner>().level;
        }
        if (player.playerState == Player.PlayerState.Died)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(false);
            gameOverUI.SetActive(true);

            gameOverScoreText.text = ScoreManager.Instance.score.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("HighScore").ToString();

            if (Input.GetMouseButtonDown(0))
            {
                ScoreManager.Instance.ResetScore();
                SceneManager.LoadScene(0);
            }
        }
    }

    private bool IgnoreUI()
    {

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResults.RemoveAt(i);
                    i--;
            }
        }
        return raycastResults.Count > 0;
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void Settings()
    {
        btns = !btns;
        buttons.SetActive(btns);
    }
}
