using UnityEngine;
using TMPro;

public class TextUpdater : PlayerSystem
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointsText;

    [Header("Object References")]
    [SerializeField] private Health healthReference;
    [SerializeField] private PointSystem pointsReference;

    [SerializeField] private Player player;

    private void Awake()
    {
        // Ensure the base PlayerSystem Awake ran first
        if (player == null)
        {
            Debug.LogError("Player not found in parent hierarchy for TextUpdater!");
        }

        if (healthReference == null)
        {
            Debug.LogError("Health reference is not assigned in TextUpdater!");
        }

        if (healthText == null)
        {
            Debug.LogError("HealthText UI is not assigned in TextUpdater!");
        }
    }

    private void Start()
    {
        // Safety checks
        if (player == null || player.ID == null || healthReference == null || healthText == null || pointsReference == null || pointsText == null)
            return;

        // Initialize UI
        UpdateHealthText();
        UpdatePointsText();

        // Subscribe to health change events
        player.ID.events.OnHealthChange += UpdateHealthText;
        player.ID.events.OnPointsChange += UpdatePointsText;
    }

    private void OnDisable()
    {
        // Unsubscribe safely
        if (player != null && player.ID != null)
        {
            player.ID.events.OnHealthChange -= UpdateHealthText;
        }
    }

    private void UpdateHealthText()
    {
        if (healthReference != null && healthText != null)
        {
            healthText.text = "Lives: " + healthReference.GetHealth();
        }
    }

    private void UpdatePointsText()
    {
        if (pointsReference != null && pointsText != null)
        {
            pointsText.text = "Points: " + pointsReference.GetPoints();
        }
    }
}
