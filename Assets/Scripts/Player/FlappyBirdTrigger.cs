using UnityEngine;

public class FlappyBirdTrigger : MonoBehaviour
{
    [Tooltip("Optional visual indicator for the trigger zone")]
    [SerializeField] private SpriteRenderer zoneSpriteRenderer;

    [Tooltip("Color of the zone when FlappyBird mode is active")]
    [SerializeField] private Color activeColor = Color.green;

    [Tooltip("Color of the zone when FlappyBird mode is inactive")]
    [SerializeField] private Color inactiveColor = Color.red;

    private bool isFlappyBirdModeActive = false;

    private void Start()
    {
        // Set initial color of the zone
        UpdateZoneVisual();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleFlappyBirdMode(other.gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleFlappyBirdMode(other.gameObject, false);
        }
    }

    private void ToggleFlappyBirdMode(GameObject player, bool activate)
    {
        isFlappyBirdModeActive = activate;

        // Get the PlayerMovement component
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            // Switch to the appropriate state
            PlayerState newState = activate ? PlayerState.FlappyBird : PlayerState.Normal;
            playerMovement.SwitchState(newState);

            // Directly toggle FlappyBird mode on CharacterController2D
            CharacterController2D characterController = player.GetComponent<CharacterController2D>();
            if (characterController != null)
            {
                characterController.ToggleFlappyBirdMode(activate);
            }

            Debug.Log($"FlappyBird mode {(activate ? "activated" : "deactivated")}");
        }
        else
        {
            Debug.LogError("PlayerMovement component not found on the player object!");
        }

        // Update the visual indicator
        UpdateZoneVisual();
    }

    private void UpdateZoneVisual()
    {
        if (zoneSpriteRenderer != null)
        {
            zoneSpriteRenderer.color = isFlappyBirdModeActive ? activeColor : inactiveColor;
        }
    }
}