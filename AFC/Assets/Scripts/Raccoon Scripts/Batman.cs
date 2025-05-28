using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class Batman : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public GameObject chainLinkPrefab;
    public float segmentLength = 1f;
    private float lifeTime = 1;
    private List<GameObject> currentLinks = new List<GameObject>();
    private bool chainBuilt = false;

    void Start()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }
    void Update()
    {
        if (!chainBuilt && startPoint != null && endPoint != null)
        {
            BuildChain();
        }
        else
        {
            ClearChain();
        }
        
    }

    void BuildChain()
    {
        ClearChain();

        Vector3 direction = (endPoint.position - startPoint.position).normalized;
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
        int linkCount = Mathf.FloorToInt(distance / segmentLength);

        for (int i = 0; i < linkCount; i++)
        {
            Vector3 pos = startPoint.position + direction * 0.2f * i;
            GameObject link = Instantiate(chainLinkPrefab, pos, Quaternion.LookRotation(direction), transform);
            currentLinks.Add(link);
        }
    }

    void ClearChain()
    {
        foreach (var link in currentLinks)
        {
            Destroy(link);
        }
        currentLinks.Clear();
    }
    [Server]
    private void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}

