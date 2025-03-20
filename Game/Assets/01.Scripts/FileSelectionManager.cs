using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class FileSelectionManager : MonoBehaviour
{
    public TextMeshProUGUI[] fileOptions; // List of file names
    public Image glowingIndicator; // The UI Image for the glow effect
    public GameObject confirmationPopup; // The confirmation UI GameObject
    public TextMeshProUGUI confirmationText; // Text inside the confirmation popup

    private int currentIndex = 0;
    private bool isConfirming = false;
    private Coroutine glowCoroutine;

    private void Start()
    {
        confirmationPopup.SetActive(false); // Ensure popup is hidden at start
        glowCoroutine = StartCoroutine(GlowingEffect()); // Start the glowing effect
    }

    private void Update()
    {
        if (isConfirming)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                StartGame();
            }
            else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseConfirmation();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OpenConfirmation();
        }
    }

    private void OpenConfirmation()
    {
        isConfirming = true;
        confirmationPopup.SetActive(true);
    }

    private void CloseConfirmation()
    {
        isConfirming = false;
        confirmationPopup.SetActive(false);
    }

    private void StartGame()
    {
        Debug.Log($"Starting {fileOptions[currentIndex].text}...");
        // Load game scene or proceed with selection logic
    }


    private IEnumerator GlowingEffect()
    {
        while (true)
        {
            for (float alpha = 0.3f; alpha <= 1f; alpha += Time.deltaTime * 2)
            {
                SetIndicatorAlpha(alpha);
                yield return null;
            }

            for (float alpha = 1f; alpha >= 0.3f; alpha -= Time.deltaTime * 2)
            {
                SetIndicatorAlpha(alpha);
                yield return null;
            }
        }
    }

    private void SetIndicatorAlpha(float alpha)
    {
        if (glowingIndicator != null)
        {
            Color color = glowingIndicator.color;
            color.a = alpha; // Adjust alpha for transparency
            glowingIndicator.color = color;
        }
    }
}
