using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(WhistleControl))]
public class WhistleControlEditor : Editor
{
    void OnSceneGUI() {
        WhistleControl fov = (WhistleControl)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.back, Vector2.up, 360, fov.SoundRadius);
    }
}
