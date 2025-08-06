using UnityEngine;
using UnityEngine.UIElements;

public class ProgressBarIncrementer : MonoBehaviour
{
    public UIDocument uiDocument;
    public UIDocument gameOverUIDocument; // Reference to your GameOver UI document

    private ProgressBar temperatureBar;
    private VisualElement progressFill;
    private VisualElement gameOverRoot;

    private bool isGameOver = false;

    void OnEnable()
    {
        temperatureBar = uiDocument.rootVisualElement.Q<ProgressBar>("PlanetTemp");
        progressFill = temperatureBar.Q<VisualElement>(className: "unity-progress-bar__progress");
        progressFill.style.backgroundColor = new StyleColor(Color.red);

        // Rooting the Game Over UI and hiding it
        gameOverRoot = gameOverUIDocument.rootVisualElement;
        gameOverRoot.style.display = DisplayStyle.None;

        // Creating a 10 second delay before rising temperatures begin
        Invoke(nameof(RaiseTemperature), 10f);
    }

    void RaiseTemperature()
    {
        InvokeRepeating(nameof(IncrementProgress), 1f, 1f);
    }

    void IncrementProgress()
    {
        if (temperatureBar != null && temperatureBar.value < temperatureBar.highValue)
        {
            temperatureBar.value += 1;

            float t = temperatureBar.value / temperatureBar.highValue;
            progressFill.style.backgroundColor = new StyleColor(Color.Lerp(Color.green, Color.red, t));
        }
        else if (!isGameOver)
        {
            CancelInvoke(nameof(IncrementProgress));
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        // Displaying the game over UI
        gameOverRoot.style.display = DisplayStyle.Flex;
    }
}
