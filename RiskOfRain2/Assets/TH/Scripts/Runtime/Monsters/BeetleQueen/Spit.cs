using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeetleQueenSkills
{
	public class Spit : MonoBehaviour
	{
		private const float SPEED = 800f;
		private Rigidbody _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		void Start()
		{
			_rigidbody.AddForce(transform.forward * SPEED);
		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Ground" || other.tag == "Player")
			{
				//int pixelSize = 100;

				//RaycastHit hit;
				//Vector3 rayVec = Vector3.down;
				//if (!Physics.Raycast(transform.position, rayVec, out hit))
				//{
				//	return;
				//}
				//Debug.Log($"[transform] {hit.transform.position}, [texCoord] {hit.textureCoord}");

				//Renderer rend = other.GetComponent<MeshRenderer>();

				//Texture2D tex = rend.material.mainTexture as Texture2D;
				//Vector2 pixelUV = hit.textureCoord;
				//pixelUV.x *= tex.width;
				//pixelUV.y *= tex.height;

				//Color[] colors = new Color[pixelSize * pixelSize];

				//for (var i = 0; i < pixelSize * pixelSize; i++)
				//{
				//	colors[i] = Color.black;
				//}

				//tex.SetPixels((int)pixelUV.x, (int)pixelUV.y, pixelSize, pixelSize, colors);

				//tex.Apply();

				Destroy(gameObject);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;

			if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100))
			{
				Gizmos.DrawRay(transform.position, Vector3.down * hit.distance);
			}
		}
	}
}