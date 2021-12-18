using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockRandomizer : MonoBehaviour
{
    private int[] rotations = new[]{0, -90, 90, 180};
    private SpriteRenderer sRenderer;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 3);
        Quaternion block = transform.rotation;
        block.eulerAngles = new Vector3 (0, 0, rotations[r]);
        transform.rotation = block;
        r = Random.Range(0, 1);
        sRenderer = GetComponentInParent<SpriteRenderer>();
        sRenderer.flipX = Convert.ToBoolean(r);
        r = Random.Range(0, 1);
        sRenderer.flipY = Convert.ToBoolean(r);
    }
}
