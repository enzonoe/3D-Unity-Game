using UnityEngine;
using UnityEngine.UI;

public class UpdateRadar : MonoBehaviour
{
    public RectTransform playerIcon; // The player sprite
    public RectTransform objectiveIconPrefab; // Prefab for the objective icon
    public Transform groundTransform; // The ground object
    public RectTransform backgroundTransform;
    public Transform orientation; // Rotation of the Player/Camera
    public GameObject playerObject; // Player object to track

    GameObject[] objectiveObjects;
    RectTransform[] objectiveIcons;

    Vector3 groundScale;

    //Vector3 backgroundTransform;
    float correctScaling;

    void Start()
    {
        // Get the scale of the ground
        groundScale = groundTransform.localScale;

        // Find all GameObjects with the "Objective" tag
        objectiveObjects = GameObject.FindGameObjectsWithTag("Objective");

        // Initialize the objectiveIcons array with the correct size
        objectiveIcons = new RectTransform[objectiveObjects.Length];

        // Create icons for each objective
        for (int i = 0; i < objectiveObjects.Length; i++)
        {
            objectiveIcons[i] = CreateObjectiveIcon(objectiveObjects[i], i);
            Debug.Log("Icons: " + objectiveIcons[i] + " Objective: " + objectiveObjects[i]);
        }
    }

    void Update()
    {
        // Update player icon position and rotation based on player's position and rotation in the game world
        UpdateIconPosition(playerIcon, GetObjectPosition(playerObject));
        UpdateIconRotation(playerIcon, orientation);

        if (objectiveIcons != null)
        {
            for (int i = 0; i < objectiveObjects.Length; i++)
            {
                // Debug.Log("number: " + i);
                UpdateIconPosition(objectiveIcons[i], GetObjectPosition(objectiveObjects[i]));
            }
        }
    }

    RectTransform CreateObjectiveIcon(GameObject objectiveObject, int index)
    {
        // Instantiate an icon for the objective
        RectTransform objectiveIcon = Instantiate(objectiveIconPrefab, transform);

        // Give the objective icon a unique name based on the provided index
        objectiveIcon.name = "ObjectiveIcon" + index;

        // Update objective icon position and rotation based on its position in the game world
        UpdateIconPosition(objectiveIcon, GetObjectPosition(objectiveObject));

        return objectiveIcon;
    }

    void UpdateIconPosition(RectTransform icon, Vector3 playerPosition)
    {
        // Convert world position to radar panel space
        Vector2 radarPosition = new Vector2(
            (playerPosition.x / groundScale.x) * backgroundTransform.rect.width,
            (playerPosition.z / groundScale.z) * backgroundTransform.rect.height
        );

        // Calculate offset based on the anchor point
        Vector2 anchorOffset = new Vector2(
            backgroundTransform.rect.width * (backgroundTransform.pivot.x - 0.5f),
            backgroundTransform.rect.height * (backgroundTransform.pivot.y - 0.5f)
        );

        // Update icon position on the radar
        icon.anchoredPosition = radarPosition - anchorOffset;
    }

    void UpdateIconRotation(RectTransform icon, Transform orientation)
    {
        // Check if the object reference is assigned
        if (orientation != null)
        {
            // Update icon rotation to match the rotation of the trackedObject
            icon.rotation = Quaternion.Euler(0f, 0f, -orientation.eulerAngles.y);
        }
        else
        {
            // Log a message if the reference is not assigned
            Debug.LogWarning("Orientation reference not assigned in the Inspector!");
        }
    }

    Vector3 GetObjectPosition(GameObject obj)
    {
        // Check if the object reference is assigned
        if (obj != null)
        {
            // Access the transform of the assigned GameObject
            return obj.transform.position;
        }
        else
        {
            // Log a message and return a default position if the reference is not assigned
            Debug.LogWarning("Object reference not assigned in the Inspector!");
            return Vector3.zero; // You may want to change this default position
        }
    }
}
