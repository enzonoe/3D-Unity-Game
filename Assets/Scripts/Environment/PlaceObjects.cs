using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public GameObject treePrefab;  // The tree prefab you want to instantiate
    public Transform groundTransform;  // The transform of the ground object
    public Vector3 areaSize = new Vector3(10f, 0f, 10f);  // The size of the rectangular area
    public int numberOfTrees = 4000;  // Number of trees to be placed

    void Start()
    {
        PlaceTrees();
    }

    void PlaceTrees()
    {
        // Ensure groundTransform is assigned
        if (groundTransform == null)
        {
            Debug.LogError("Ground transform is not assigned. Please assign the Ground object's transform in the inspector.");
            return;
        }

        for (int i = 0; i < numberOfTrees; i++)
        {
            // Generate random positions within the specified area
            float randomX = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float randomZ = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

            // Create a new position vector
            Vector3 treePosition = new Vector3(randomX, 0f, randomZ);

            // Instantiate the tree prefab as a child of the groundTransform at the random position
            GameObject newTree = Instantiate(treePrefab, treePosition, Quaternion.identity, groundTransform);
            newTree.name = "tree" + (i + 1);
            newTree.transform.Rotate(-90f, 0f, 0f);

            // Add Rigidbody component to the tree object
            Rigidbody treeRigidbody = newTree.AddComponent<Rigidbody>();

            // Set the Rigidbody to be kinematic (not affected by external forces)
            treeRigidbody.isKinematic = true;
        }
    }
}
