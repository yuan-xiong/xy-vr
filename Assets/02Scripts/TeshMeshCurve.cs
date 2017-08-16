﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeshMeshCurve : MonoBehaviour {

	private string LOG_TAG = "[TeshMeshCurve] ";

	public Material material;
	public int segments = 40;
	public int innerRadium = 5;
	public int radium = 10;

	public int height = 5;
	public int angle = 180;
	public Vector3 circleCenter = new Vector3 (0, 0, 0);

	// Use this for initialization
	void Start () {

//		TestVector ();
//		DrawTriangle ();
//		DrawSquare ();
//		DrawCircle (radium);
//		DrawRing(innerRadium, radium);
		DrawCurve(radium, height, angle);

	}

	void DrawCurve (float myRadium, float myHeight, float myAngle) {
		Debug.Log (LOG_TAG + "DrawCurve myRadium " + myRadium + ", myHeight " + myHeight + ", myAngle " + myAngle);

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;

		float deltaAngle = Mathf.Deg2Rad * myAngle / segments;
		float currentAngle = 0;

		int rows = segments + 1;
		int columes = segments + 1;
		float deltaHeighe = myHeight / (rows - 1);

		Vector3[] vertices = new Vector3[rows * columes];
		for (int i = 0; i < columes; i++) {
			
			float cosA = Mathf.Cos (currentAngle);
			float SinA = Mathf.Sin (currentAngle);
			float mX = circleCenter.x + cosA * myRadium;
			float mZ = circleCenter.z + SinA * myRadium;
			float mY = circleCenter.y;

			int index = rows * i;

			for (int j = 0; j < rows; j++) {
				vertices [index + j] = new Vector3 (mX, mY + deltaHeighe * j, mZ);

				Debug.Log (LOG_TAG + "vertices " + (index + j) + ": " + vertices [index + j]);
			}

			currentAngle += deltaAngle;
		}

		int pointsLength = rows * (columes - 1) - 1;

		int[] triangles = new int[pointsLength * 6];
		Debug.Log (LOG_TAG + "triangles Length ==> " + triangles.Length);

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < columes; j++) {

				int pointIndex = j * rows + i;
				int triIndex = pointIndex * 6;

				if (pointIndex < pointsLength) {
					triangles [triIndex] = pointIndex;
					triangles [triIndex + 1] = pointIndex + 1;
					triangles [triIndex + 2] = pointIndex + rows + 1;

					triangles [triIndex + 3] = pointIndex;
					triangles [triIndex + 4] = pointIndex + rows + 1;
					triangles [triIndex + 5] = pointIndex + rows;

//					Debug.Log (LOG_TAG + "triangles " + (triIndex) + ": " + triangles [triIndex]);
//					Debug.Log (LOG_TAG + "triangles " + (triIndex + 1) + ": " + triangles [triIndex + 1]);
//					Debug.Log (LOG_TAG + "triangles " + (triIndex + 2) + ": " + triangles [triIndex + 2]);
//					Debug.Log (LOG_TAG + "triangles " + (triIndex + 3) + ": " + triangles [triIndex + 3]);
//					Debug.Log (LOG_TAG + "triangles " + (triIndex + 4) + ": " + triangles [triIndex + 4]);
//					Debug.Log (LOG_TAG + "triangles " + (triIndex + 5) + ": " + triangles [triIndex + 5]);
				}
			}
		}

		Mesh mesh = filter.mesh;
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	void DrawRing (int innerRadio, int outerRadio) {
		Debug.Log (LOG_TAG + "DrawRing innerRadio " + innerRadio + ", outerRadio " + outerRadio);

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;

		Mesh mesh = filter.mesh;
		mesh.Clear();

		float deltaAngle = Mathf.Deg2Rad * 360f / segments;
		float currentAngle = 0;

		Vector3[] vertices = new Vector3[segments * 2];
		for (int i = 0; i < segments; i++) {
			float cosA = Mathf.Cos (currentAngle);
			float SinA = Mathf.Sin (currentAngle);

			vertices [i * 2] = new Vector3 (circleCenter.x + cosA * innerRadio,circleCenter.y + SinA * innerRadio,circleCenter.z);
			vertices [i * 2 + 1] = new Vector3 (circleCenter.x + cosA * outerRadio,circleCenter.y + SinA * outerRadio,circleCenter.z);
			currentAngle += deltaAngle;
		}

		int[] triangles = new int[segments * 6];

		for (int i = 0; i < segments; i++) {

			int index = i * 6;
			int begin = i * 2;

			// show ring
			triangles [index] = begin;
			triangles [index + 1] = (begin + 3) % vertices.Length;
			triangles [index + 2] = (begin + 1) % vertices.Length;

			triangles [index + 3] = begin;
			triangles [index + 4] = (begin + 2) % vertices.Length;
			triangles [index + 5] = (begin + 3) % vertices.Length;

			// show serrate 1
//			triangles [index] = begin;
//			triangles [index + 1] = (begin + 1) % vertices.Length;
//			triangles [index + 2] = (begin + 3) % vertices.Length;

			// show serrate 2
			triangles [index + 3] = begin;
			triangles [index + 4] = (begin + 3) % vertices.Length;
			triangles [index + 5] = (begin + 2) % vertices.Length;

//			Debug.Log (LOG_TAG + "triangles " + (index + 0) + ": " + triangles [index + 0]);
//			Debug.Log (LOG_TAG + "triangles " + (index + 1) + ": " + triangles [index + 1]);
//			Debug.Log (LOG_TAG + "triangles " + (index + 2) + ": " + triangles [index + 2]);
//			Debug.Log (LOG_TAG + "triangles " + (index + 3) + ": " + triangles [index + 3]);
//			Debug.Log (LOG_TAG + "triangles " + (index + 4) + ": " + triangles [index + 4]);
//			Debug.Log (LOG_TAG + "triangles " + (index + 5) + ": " + triangles [index + 5]);
		}

//		for (int i = 0, j = 0; i < segments * 6; i+=6, j+=2) {
//
//			triangles [i] = j;
//			triangles [i + 1] = (j+3) % vertices.Length;
//			triangles [i + 2] = (j+1) % vertices.Length;
//
//			triangles [i + 3] = j;
//			triangles [i + 4] = (j+2) % vertices.Length;
//			triangles [i + 5] = (j+3) % vertices.Length;
//
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 0]);
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 1]);
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 2]);
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 3]);
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 4]);
//			Debug.Log (LOG_TAG + "triangles " + (i) + ": " + triangles [i + 5]);
//		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	void DrawCircle (int circleRadium) {

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;

		Mesh mesh = filter.mesh;
		mesh.Clear();

		float deltaAngle = Mathf.Deg2Rad * 360f / segments;
		float currentAngle = 0;

		Vector3[] vertices = new Vector3[segments + 1];
		int[] triangles = new int[segments * 3];
	
		for (int i = 1; i <= segments; i++) {
			float cosA = Mathf.Cos (currentAngle);
			float SinA = Mathf.Sin (currentAngle);
			vertices [i] = new Vector3 (circleCenter.x + cosA * circleRadium,circleCenter.y + SinA * circleRadium,circleCenter.z);

			currentAngle += deltaAngle;
		}
		vertices [0] = circleCenter;

		for(int i = 0; i < segments - 1; i++) {
			triangles [3 * i] = 0;
			if (i % 2 == 0) {
				triangles [3 * i + 1] = i+1;
				triangles [3 * i + 2] = i+2;
			} else {
				triangles [3 * i + 1] = i+2;
				triangles [3 * i + 2] = i+1;
			}
		}
		triangles [3 * segments - 3] = 0;
		triangles [3 * segments - 2] = 1;
		triangles [3 * segments - 1] = segments;

		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}

	void DrawSquare () {

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;

		Mesh mesh = filter.mesh;
		mesh.Clear();

		mesh.vertices = new Vector3[] {
			new Vector3(0, 0, 0),	
			new Vector3(0, 1, 0),	
			new Vector3(1, 1, 0),	
			new Vector3(1, 0, 0),	
		};
		mesh.triangles = new int[]{ 
			0, 1, 2,
			0, 2, 3,
		};
	}

	void DrawTriangle () {

		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer> ();
		renderer.material = material;

		Mesh mesh = filter.mesh;
		mesh.Clear();

		mesh.vertices = new Vector3[] {
			new Vector3(0, 0, 0),	
			new Vector3(0, 1, 0),	
			new Vector3(1, 1, 0),	
		};
		mesh.triangles = new int[]{ 0, 1, 2 };
	}

	void TestVector() {
		Vector3 v = new Vector3(1, 2, 3);
		Debug.Log (LOG_TAG + "test Vector3: " + v.x);
		Debug.Log (LOG_TAG + "test Vector3: " + v.y);
		Debug.Log (LOG_TAG + "test Vector3: " + v.z);

		Vector3[] vs = new Vector3[3] {
			new Vector3(1,2,3),
			new Vector3(4,5,6),
			new Vector3(7,8,9),
		};

		vs [1] = new Vector3 (7,8,9);

		Debug.Log (LOG_TAG + "test Vector3: " + vs[1].x);
		Debug.Log (LOG_TAG + "test Vector3: " + vs[1].y);
		Debug.Log (LOG_TAG + "test Vector3: " + vs[1].z);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
