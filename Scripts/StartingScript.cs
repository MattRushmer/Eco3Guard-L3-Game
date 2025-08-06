using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class DelayedLabel : MonoBehaviour
{
    private Label warningIGLabel;
    private VisualElement warningIGContainer;
    private UIDocument uiDocument;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument Null");
            return;
        }

        var root = uiDocument.rootVisualElement;

        // Creating the yellow box the warning is contained within
        warningIGContainer = new VisualElement();
        warningIGContainer.style.display = DisplayStyle.None;
        warningIGContainer.style.backgroundColor = new StyleColor(new Color(1f, 1f, 0f, 0.4f));
        warningIGContainer.style.position = Position.Absolute;
        warningIGContainer.style.left = Length.Percent(50);
        warningIGContainer.style.top = Length.Percent(50);
        warningIGContainer.style.transformOrigin = new StyleTransformOrigin(
            new TransformOrigin(new Length(50, LengthUnit.Percent), new Length(50, LengthUnit.Percent)));
        warningIGContainer.style.translate = new StyleTranslate(
            new Translate(new Length(-50, LengthUnit.Percent), new Length(-50, LengthUnit.Percent)));
        warningIGContainer.style.paddingLeft = 20;
        warningIGContainer.style.paddingRight = 20;
        warningIGContainer.style.paddingTop = 10;
        warningIGContainer.style.paddingBottom = 10;
        warningIGContainer.style.alignItems = Align.Center;
        warningIGContainer.style.justifyContent = Justify.Center;
        warningIGContainer.style.opacity = 0f;

        // Creating the warning label itself
        warningIGLabel = new Label("⚠️ Pollution Rises... ⚠️");
        warningIGLabel.style.fontSize = 130;
        warningIGLabel.style.color = new StyleColor(new Color32(111, 0, 0, 255));
        warningIGLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
        warningIGLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        warningIGLabel.style.textShadow = new StyleTextShadow(new TextShadow
        {
            color = new Color(0, 0, 0, 0.8f),
            offset = new Vector2(4, 4),
            blurRadius = 2
        });

        warningIGContainer.Add(warningIGLabel);
        root.Add(warningIGContainer);
    }

    private void OnEnable()
    {
        if (warningIGContainer != null)
        {
            Invoke(nameof(PlayStart), 10f);
        }
    }

    private void PlayStart()
    {
        warningIGContainer.style.display = DisplayStyle.Flex;
        warningIGContainer.style.opacity = 0f;

        StartCoroutine(FadeElement(warningIGContainer, 0f, 1f, 0.2f)); // Fade the starting text in over 0.2 seconds

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource Null");
        }       

        Invoke(nameof(textFadeOut), 3f); // Wait 3 seconds before begenning the fade out
    }

    private void textFadeOut()
    {
        StartCoroutine(FadeElement(warningIGContainer, 1f, 0f, 0.5f)); // Fade the starting text out over 0.5 seconds 
    }

    private IEnumerator FadeElement(VisualElement element, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            element.style.opacity = Mathf.Lerp(from, to, t);
            yield return null;
        }

        element.style.opacity = to;

        if (to == 0f)
        {
            element.style.display = DisplayStyle.None;
        }
    }
}