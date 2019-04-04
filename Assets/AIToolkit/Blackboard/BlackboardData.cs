using System.Runtime.InteropServices;
using UnityEngine;

public enum eBlackboardDataType {
    UNKNOWN = 0,
    INT,
    UINT,
    BOOL,
    FLOAT,
    VECTOR2,
    VECTOR3,
    VECTOR4,
    WANDERDATA,
    LIST,
}

public class BlackboardData {
    private eBlackboardDataType type;

    private float f = 0;
    private int i = 0;
    private uint ui = 0;
    private bool b = false;
    private Vector2 v2 = Vector2.zero;
    private Vector3 v3 = Vector3.zero;
    private Vector4 v4 = Vector4.zero;
    private WanderData wd = new WanderData();
    private Pathfinding.List list = new Pathfinding.List();

    public eBlackboardDataType GetDataType() {
        return type;
    }

    public void SetDataType(eBlackboardDataType newType) {
        type = newType;
    }

    public float GetFloat() {
        return f;
    }

    public void SetFloat(float value) {
        f = value;
    }

    public int GetInt() {
        return i;
    }

    public void SetInt(int value) {
        i = value;
    }

    public uint GetUInt() {
        return ui;
    }

    public void SetUInt(uint value) {
        ui = value;
    }

    public bool GetBool() {
        return b;
    }

    public void SetBool(bool value) {
        b = value;
    }

    public Vector2 GetVector2() {
        return v2;
    }

    public void SetVector2(Vector2 value) {
        v2 = value;
    }

    public Vector3 GetVector3() {
        return v3;
    }

    public void SetVector3(Vector3 value) {
        v3 = value;
    }

    public Vector4 GetVector4() {
        return v4;
    }

    public void SetVector4(Vector4 value) {
        v4 = value;
    }

    public WanderData GetWanderData() {
        return wd;
    }

    public void SetWanderData(WanderData value) {
        wd = value;
    }

    public Pathfinding.List GetList() {
        return list;
    }

    public void SetList(Pathfinding.List value) {
        list = value;
    }
}
