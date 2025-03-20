using System.Collections;
using UnityEngine;
using TMPro;

public class BootUpSequence : MonoBehaviour
{
    public CutsceneData cutsceneData; // Text data (ScriptableObject)
    public TextMeshProUGUI displayText; // UI text for typing effect
    public GameObject startMenu; // Start Menu UI (Initially Hidden)
    public float typingSpeed = 0.05f; // Speed of typing effect
    public float loadingTime = 1.5f; // Time before replacing "/" with "[complete]"
    public float skipDisplayTime = 3f; // Time to show full text after skipping

    private bool isSkipping = false;
    private string[] loadingSymbols = { "/", "|", "-", "\\" }; // Loading animation
    private int loadingIndex = 0;

    private void Start()
    {
        startMenu.SetActive(false); // Hide start menu initially
        StartCoroutine(TypeTextAndShowMenu());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            isSkipping = true; // Skip typing effect
        }
    }

    IEnumerator TypeTextAndShowMenu()
    {
        displayText.text = "";
        string[] lines = cutsceneData.textToType.Split('\n'); // Split text into lines

        foreach (string line in lines)
        {
            if (isSkipping)
            {
                // Instantly show full text, replacing all "/" with "[complete]"
                displayText.text += line.Replace("/", "[complete]") + "\n";
                continue;
            }
            yield return StartCoroutine(TypeLine(line)); // Type each line
            yield return new WaitForSeconds(0.5f); // Small delay before next line
        }

        if (isSkipping)
        {
            yield return new WaitForSeconds(skipDisplayTime); // Wait for a few seconds before menu
        }
        else
        {
            yield return new WaitForSeconds(1f); // Normal delay before menu if not skipped
        }

        ShowStartMenu();
    }

    IEnumerator TypeLine(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (isSkipping)
            {
                displayText.text += line.Replace("/", "[complete]"); // Instantly complete the line
                yield return null;
                break;
            }

            if (line[i] == '/') // If the character is "/", start loading animation
            {
                displayText.text += "/"; // Append "/"
                yield return StartCoroutine(AnimateLoadingSymbol());
                displayText.text = displayText.text.Substring(0, displayText.text.Length - 1) + "[complete]"; // Replace "/" with "[complete]"
                continue;
            }

            displayText.text += line[i]; // Append character normally
            yield return new WaitForSeconds(typingSpeed);
        }

        displayText.text += "\n"; // Move to the next line
    }

    IEnumerator AnimateLoadingSymbol()
    {
        float elapsedTime = 0f;

        while (elapsedTime < loadingTime && !isSkipping) // Keep animating for `loadingTime`
        {
            string currentText = displayText.text;
            string withoutLast = currentText.Substring(0, currentText.Length - 1); // Remove last char

            displayText.text = withoutLast + loadingSymbols[loadingIndex]; // Append rotating symbol
            loadingIndex = (loadingIndex + 1) % loadingSymbols.Length; // Cycle animation

            yield return new WaitForSeconds(0.2f); // Animation speed
            elapsedTime += 0.2f;
        }
    }

    private void ShowStartMenu()
    {
        displayText.gameObject.SetActive(false); // Hide boot-up text
        startMenu.SetActive(true); // Show start menu UI
    }
}
