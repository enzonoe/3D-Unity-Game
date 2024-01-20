using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    public RectTransform playerIcon;
    public RectTransform objectiveIconPrefab; // Prefab for the objective icon
    public Transform groundTransform; // The ground object
    public Transform orientation; // Rotation of the Player/Camera
    public GameObject playerObject; // Player object to track

    GameObject[] objectiveObjects;
    RectTransform[] objectiveIcons;

    Vector3 groundScale;

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
            Debug.Log("Length: " + objectiveObjects.Length + " Number: " + i);
            objectiveIcons[i] = CreateObjectiveIcon(objectiveObjects[i]);
            objectiveIcons[i].name = "ObjectiveIcon" + i; // Append index to the name
            Debug.Log("Icons: " + objectiveIcons[i]);
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
                Debug.Log("number: " + i);
                UpdateIconPosition(objectiveIcons[i], GetObjectPosition(objectiveObjects[i]));
            }
        }
    }

    RectTransform CreateObjectiveIcon(GameObject objectiveObject)
    {
        // Instantiate an icon for the objective
        RectTransform objectiveIcon = Instantiate(objectiveIconPrefab, transform);

        // Update objective icon position and rotation based on its position in the game world
        UpdateIconPosition(objectiveIcon, GetObjectPosition(objectiveObject));

        return objectiveIcon;
    }

    void UpdateIconPosition(RectTransform icon, Vector3 playerPosition)
    {
        // Convert world position to radar panel space
        Vector2 radarPosition = new Vector2(playerPosition.x / groundScale.x, playerPosition.z / groundScale.z);

        // Scale the radar position based on the scale of the radar panel
        radarPosition.x *= playerIcon.parent.GetComponent<RectTransform>().sizeDelta.x / 2;
        radarPosition.y *= playerIcon.parent.GetComponent<RectTransform>().sizeDelta.y / 2;

        // Update icon position on the radar
        icon.anchoredPosition = radarPosition;
    }

    void UpdateIconRotation(RectTransform icon, Transform orientation)
    {
        // Check if the object reference is assigned
        if (orientation != null)
        {
            // Update icon rotation to match the rotation of the trackedObject
            icon.rotation = Quaternion.Euler(0f, 0f, -(orientation.eulerAngles.y + 180));
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
