using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;

    [Header("제한 옵션")]
    [SerializeField] private int maxItems = 10;
    private int currentItemCount = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 1f, spawnInterval);
    }

    private void SpawnItem()
    {
        if (currentItemCount >= maxItems) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject newItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
        currentItemCount++;

        // 아이템이 파괴될 때 카운트 줄이기
        Item pickup = newItem.GetComponent<Item>();
        if (pickup != null)
        {
            pickup.onCollected += HandleItemCollected;
        }
    }

    private void HandleItemCollected()
    {
        currentItemCount--;
    }
}
