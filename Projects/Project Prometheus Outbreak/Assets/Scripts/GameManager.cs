using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text narratorText;
    public TMP_InputField playerInput;
    public Button submitButton;

    [Header("Panels")]
    public GameObject inventoryPanel;
    public Transform inventoryContent;   // Content object under ScrollView
    public GameObject inventoryItemPrefab;

    public GameObject statsPanel;
    public TMP_Text[] statTexts;         // Assign 5 TMP_Text components (Health, Mana, etc.)

    [Header("Menu Buttons")]
    public Button viewInventoryButton;
    public Button viewStatsButton;
    public Button exitButton;
    public Button closeInventoryButton;
    public Button closeStatsButton;
}
