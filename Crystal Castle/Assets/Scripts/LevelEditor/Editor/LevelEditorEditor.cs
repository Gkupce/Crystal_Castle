using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEditor))]
[CanEditMultipleObjects]
public class LevelEditorEditor : Editor {
	SerializedProperty levelGO;
	SerializedProperty currentTile;
	SerializedProperty EditProp;
	

	private void OnEnable()
	{
		levelGO = serializedObject.FindProperty("levelGO");
		currentTile = serializedObject.FindProperty("currentTile");
		EditProp = serializedObject.FindProperty("InEditMode");
		
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();
		serializedObject.Update();

		EditorGUILayout.PropertyField(levelGO);
		EditorGUILayout.Separator();
		//=====================================================
		EditorGUILayout.PropertyField(currentTile);
		if(currentTile.objectReferenceValue != null)
		{
			EditorGUILayout.LabelField(
				"Tile size",
				((GameObject)currentTile.objectReferenceValue)
					.GetComponent<SpriteRenderer>().bounds.size.ToString()
			);
		}
		EditorGUILayout.Separator();
		//=====================================================
		EditorGUILayout.PropertyField(EditProp);
		if(EditProp.boolValue)
		{
			EditorGUILayout.LabelField("Position", ((LevelEditor)target).pos.ToString());
		}
		serializedObject.ApplyModifiedProperties();
	}

	private void OnSceneGUI()
	{
		if(EditProp.boolValue)
		{
			//HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

			LevelEditor lvlEdit = (LevelEditor)target;
			if (Event.current.type == EventType.MouseMove)
			{
				Vector2 mousePos = Event.current.mousePosition;
				mousePos.y = Camera.current.pixelHeight - mousePos.y;
				Vector3 pos = Camera.current.ScreenPointToRay(mousePos).origin;
				pos.y = Truncate(pos.y);
				pos.x = Truncate(pos.x);
				pos.z = 0;
				Event.current.Use();
				lvlEdit.pos = pos;
			}
			Vector3 boxSize = Vector3.one;
			if(currentTile.objectReferenceValue != null)
			{
				boxSize = ((GameObject)currentTile.objectReferenceValue)
					.GetComponent<SpriteRenderer>().bounds.size;
				if(Event.current.type == EventType.MouseUp && Event.current.button == 1)
				{
					CreateTile(lvlEdit);
					Event.current.Use();
				}
			}

			Handles.color = Color.yellow;
			Handles.DrawWireCube(lvlEdit.pos, boxSize);
		}
		else
		{
			//HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Keyboard));
		}
	}

	private void CreateTile(LevelEditor lvlEdit)
	{
		GameObject go = GameObject.Instantiate((GameObject)currentTile.objectReferenceValue);
		go.transform.position = lvlEdit.pos;
		if (levelGO.objectReferenceValue != null)
		{
			go.transform.parent = ((GameObject)levelGO.objectReferenceValue).transform;
		}
		Undo.RegisterCreatedObjectUndo(go, "Added tile");
	}

	private float Truncate(float val)
	{
		float truncVal = 0.6f;
		return Mathf.RoundToInt(val / truncVal) * truncVal;
	}
}
