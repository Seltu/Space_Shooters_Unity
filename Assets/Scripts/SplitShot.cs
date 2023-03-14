using System;
using UnityEngine;

public class SplitShot : Shot
{
    public int split;
    public Shot splitPrefab;
    public float timeToDisappear;

    private void Start()
    {
        Destroy(gameObject, timeToDisappear);
    }
    private void Split()
    {
        if(splitPrefab == null) return;
        for (var i = 0; i < split; i++)
        {
            var angle = i * 2 * Math.PI / split + Math.PI / split;
            var instruction = new ShipShot(new Vector2( (float)(1 * Math.Cos(angle)), (float)(1 * Math.Sin(angle))),
                new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
            var transform1 = transform;
            var shot = Instantiate(splitPrefab, transform1.position, transform1.rotation);
            shot.SetShot(Ship, instruction);
        }
    }
    
    private void OnDestroy()
    {
        if (split > 0)
        {
            Split();
        }
    }
}
