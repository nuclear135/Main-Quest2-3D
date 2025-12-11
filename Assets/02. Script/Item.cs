using UnityEngine;
using System;

public class Item: MonoBehaviour
{
    public Action onCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            onCollected?.Invoke(); // 스폰 카운트 차감
            Destroy(gameObject);
        }
    }
}
