using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }
    [SerializeField]
    public static bool Win2048 { get; private set; }
    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private GameObject continueBut;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Win2048 = false;
    }
    private void Start()
    {
        Win2048 = false;
        StartGame();
    }
    public void StartGame()
    {
        Win2048 = false;
        gameResult.text = "";
        SetPoints(0);
        GameStarted = true;
        Field.Instance.GenerateField();
    }
    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }
    private void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win!";
        continueBut.SetActive(true);
    }
    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "You Lose!";
    }
    public void Continue()
    {
        GameStarted = true;
        gameResult.text = "";
        Win2048 = true;
        continueBut.SetActive(false);
    }
    public bool getWin()
    {
        return Win2048;
    }

}
