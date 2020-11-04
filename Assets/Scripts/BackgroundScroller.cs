using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	[SerializeField] private float scrollSpeed, tileSizeZ;

	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
	}

	void FixedUpdate()
	{
        MoveBackground();
	}

    void MoveBackground()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
