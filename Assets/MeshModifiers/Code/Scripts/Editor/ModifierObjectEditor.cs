﻿using UnityEngine;
using UnityEditor;
using MeshModifiers;

[CustomEditor (typeof (ModifierObject)), CanEditMultipleObjects]
public class ModifierObjectEditor : Editor
{
	private readonly string DEFAULT_PATH = "Assets/";

	private ModifierObject current;

	int delayedVPS;
	float vpsRefreshCounter = 0;
	float vpsRefreshDelay = 1f;
	float
		lastTime, 
		deltaTime;

	public override void OnInspectorGUI ()
	{
		if (current == null || current != target)
			current = target as ModifierObject;

		base.OnInspectorGUI ();

		deltaTime = (float)EditorApplication.timeSinceStartup - lastTime;

		EditorGUILayout.Space ();
		DrawGUI ();


		lastTime = (float)EditorApplication.timeSinceStartup;

		if (Application.isPlaying)
			Repaint ();
	}

	public void DrawGUI ()
	{
		EditorGUILayout.LabelField ("\tTotal Verts - " + current.GetVertCount ());
		EditorGUILayout.LabelField ("\tVerts Modded/frame - " + current.GetModifiedVertsPerFrame ());

		vpsRefreshCounter += deltaTime;
		if (Application.isPlaying)
		{
			if (GUILayout.Button ("Save Mesh"))
			{
				Mesh tempMesh = (Mesh)UnityEngine.Object.Instantiate (current.Mesh);
				AssetDatabase.CreateAsset (tempMesh, AssetDatabase.GenerateUniqueAssetPath (DEFAULT_PATH + current.name+ ".asset"));
			}

			if (vpsRefreshCounter > vpsRefreshDelay)
			{
				delayedVPS = current.GetModifiedVertsPerSecond ();
				vpsRefreshCounter = 0f;
			}

			EditorGUILayout.LabelField ("\tVerts Modded/second ~ " + delayedVPS);
		}
	}
}