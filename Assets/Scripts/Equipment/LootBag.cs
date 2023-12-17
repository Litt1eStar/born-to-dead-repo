using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject expPrefab;
    public float popDuration = 0.5f; // Duration for the pop animation
    public float dropDuration = 1.0f; // Duration for the drop animation
    public float popHeight = 1.0f;
    public float dropSpeedMultiplier = 2.0f;
    public void InstantiateExpLootBag()
    {
        GameObject newObj = Instantiate(expPrefab, transform.position, Quaternion.identity);
        StartCoroutine(PopAndDropAnimation(newObj));
    }

    IEnumerator PopAndDropAnimation(GameObject obj)
    {
        // Randomly choose a side (0: Center, 1: Left, 2: Right)
        int side = Random.Range(0, 3);
        float popDirection = side == 0 ? 0f : (side == 1 ? -1f : 1f);

        // Pop animation
        float popElapsedTime = 0f;
        Vector3 popStart = obj.transform.position;
        Vector3 popEnd = popStart + new Vector3(popDirection * popHeight, popHeight, 0f);

        while (popElapsedTime < popDuration)
        {
            if(obj == null)
                yield break;

            obj.transform.position = Vector3.Lerp(popStart, popEnd, popElapsedTime / popDuration);
            popElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the item reaches the pop position
        obj.transform.position = popEnd;

        // Drop animation with increased speed
        float dropElapsedTime = 0f;
        Vector3 dropStart = obj.transform.position;
        Vector3 dropEnd = dropStart - new Vector3(0f, 1f, 0f); // Adjust the drop distance

        while (dropElapsedTime < dropDuration)
        {
            if (obj == null)
                yield break;

            float dropSpeed = dropSpeedMultiplier * (dropElapsedTime / dropDuration);
            obj.transform.position = Vector3.Lerp(dropStart, dropEnd, dropSpeed);
            dropElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the item reaches the final position
        obj.transform.position = dropEnd;
    }

}
