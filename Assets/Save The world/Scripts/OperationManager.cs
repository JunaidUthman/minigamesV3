using UnityEngine;

public class OperationManager : MonoBehaviour
{
    [Header("Operation Canvases in order (Op1 -> Op2 -> Op3 -> Op4)")]
    public GameObject[] operationCanvases;

    private int currentOperationIndex = 0;

    // Call this method with isRight = true when the answer is correct
    public void TryNextOperation(bool isRight)
    {
        if (!isRight) return;

        // Deactivate current canvas
        if (currentOperationIndex < operationCanvases.Length)
        {
            operationCanvases[currentOperationIndex].SetActive(false);
        }

        currentOperationIndex++;

        // Activate next canvas if exists
        if (currentOperationIndex < operationCanvases.Length)
        {
            operationCanvases[currentOperationIndex].SetActive(true);
        }
        else
        {
            Debug.Log("All operations completed!");
            // Optional: trigger some game win logic here
        }
    }

    void Start()
    {
        // Activate only the first operation, deactivate all others
        for (int i = 0; i < operationCanvases.Length; i++)
        {
            operationCanvases[i].SetActive(i == 0);
        }
    }
}
