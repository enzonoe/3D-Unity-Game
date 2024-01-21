using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public GameObject betterTreePrefab;  // The prefab with improved collision
    public Transform groundTransform;  // The transform of the ground object
    public Vector3 areaSize = new Vector3(10f, 0f, 10f);  // The size of the rectangular area
    public int numberOfTrees = 3000;  // Number of trees to be placed

    public GameObject player;  // Reference to the player GameObject

    void Start()
    {
        // Ensure groundTransform and player are assigned
        if (groundTransform == null)
        {
            Debug.LogError("Ground transform is not assigned. Please assign the Ground object's transform in the inspector.");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player is not assigned. Please assign the Player GameObject in the inspector.");
            return;
        }

        // Ensure the betterTreePrefab is assigned
        if (betterTreePrefab == null)
        {
            Debug.LogError("Better tree prefab is not assigned. Please assign the prefab in the inspector.");
            return;
        }

        PlaceTrees();
    }

    void PlaceTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            // Generate random positions within the specified area
            float randomX = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float randomZ = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

            // Create a new position vector
            Vector3 treePosition = new Vector3(randomX, 0f, randomZ);

            // Instantiate the betterTreePrefab as a child of the groundTransform at the random position
            GameObject newTree = Instantiate(betterTreePrefab, treePosition, Quaternion.identity, groundTransform);
            newTree.name = "tree" + (i + 1);

            // Apply the desired rotation to the new tree
            newTree.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            // Make the tree kinematic to prevent it from falling
            Rigidbody treeRigidbody = newTree.GetComponent<Rigidbody>();
            if (treeRigidbody != null)
            {
                treeRigidbody.isKinematic = true;
            }

            // Optionally, you can disable the MeshRenderer if it's not needed for the trees
            // newTree.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.gameObject == player)
        {
            // Handle the collision with a tree
            Debug.Log("Player collided with a tree!");
            // You can add code here to stop player movement or trigger some other action.
        }
    }
}
