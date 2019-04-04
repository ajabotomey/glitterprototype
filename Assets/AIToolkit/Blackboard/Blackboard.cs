using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard {

    private SortedDictionary<string, BlackboardData>    m_data;
    private List<BlackboardQuestion>                    m_questions;

    public Blackboard() {
        m_data = new SortedDictionary<string, BlackboardData>();
        m_questions = new List<BlackboardQuestion>();
    }

    ~Blackboard() {
        m_data.Clear();
        m_questions.Clear();
    }

    public void Remove(string name) {
        if (m_data.ContainsKey(name)) {
            m_data.Remove(name);
        }
    }

    public eBlackboardDataType GetType(string name) {
        if (m_data.ContainsKey(name)) {
            return m_data[name].GetDataType();
        }

        return eBlackboardDataType.UNKNOWN;
    }

    public bool Contains(string name) {
        return m_data.ContainsKey(name);
    }

    #region Add Data

    public bool Set(string name, int value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.INT);
            data.SetInt(value);

            m_data.Add(name, data);
        } else {
            if (m_data[name].GetDataType() != eBlackboardDataType.INT) {
                return false;
            }

            m_data[name].SetInt(value);
        }

        return true;
    }

    public bool Set(string name, uint value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.UINT);
            data.SetUInt(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.UINT) {
                return false;
            }

            m_data[name].SetUInt(value);
        }

        return true;
    }

    public bool Set(string name, float value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.FLOAT);
            data.SetFloat(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.FLOAT) {
                return false;
            }

            m_data[name].SetFloat(value);
        }

        return true;
    }

    public bool Set(string name, bool value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.BOOL);
            data.SetBool(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.BOOL) {
                return false;
            }

            m_data[name].SetBool(value);
        }

        return true;
    }

    public bool Set(string name, Vector2 value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.VECTOR2);
            data.SetVector2(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.VECTOR2) {
                return false;
            }

            m_data[name].SetVector2(value);
        }

        return true;
    }

    public bool Set(string name, Vector3 value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.VECTOR3);
            data.SetVector3(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.VECTOR3) {
                return false;
            }

            m_data[name].SetVector3(value);
        }

        return true;
    }

    public bool Set(string name, Vector4 value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.VECTOR4);
            data.SetVector4(value);

            m_data.Add(name, data);
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.VECTOR4) {
                return false;
            }

            m_data[name].SetVector4(value);
        }

        return true;
    }

    public bool Set(string name, WanderData value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.WANDERDATA);
            data.SetWanderData(value);

            m_data.Add(name, data); // Why is the data type corrupted here?
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.WANDERDATA) {
                return false;
            }

            m_data[name].SetWanderData(value);
        }

        return true;
    }

    public bool Set(string name, Pathfinding.List value) {
        if (!m_data.ContainsKey(name)) {
            // Add new data
            BlackboardData data = new BlackboardData();
            data.SetDataType(eBlackboardDataType.LIST);
            data.SetList(value);

            m_data.Add(name, data); // Why is the data type corrupted here?
        }
        else {
            if (m_data[name].GetDataType() != eBlackboardDataType.LIST) {
                return false;
            }

            m_data[name].SetList(value);
        }

        return true;
    }

    #endregion

    #region Retrieve Data

    public bool Get(string name, ref int value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.INT) {
            return false;
        }

        value = m_data[name].GetInt();
        return true;
    }

    public bool Get(string name, ref uint value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.UINT) {
            return false;
        }

        value = m_data[name].GetUInt();
        return true;
    }

    public bool Get(string name, ref float value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.FLOAT) {
            return false;
        }

        value = m_data[name].GetFloat();
        return true;
    }

    public bool Get(string name, ref bool value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.BOOL) {
            return false;
        }

        value = m_data[name].GetBool();
        return true;
    }

    public bool Get(string name, ref Vector2 value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.VECTOR2) {
            return false;
        }

        value = m_data[name].GetVector2();
        return true;
    }

    public bool Get(string name, ref Vector3 value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.VECTOR3) {
            return false;
        }

        value = m_data[name].GetVector3();
        return true;
    }

    public bool Get(string name, ref Vector4 value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.VECTOR4) {
            return false;
        }

        value = m_data[name].GetVector4();
        return true;
    }

    public bool Get(string name, ref WanderData value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.WANDERDATA) {
            return false;
        }

        value = m_data[name].GetWanderData();
        return true;
    }

    public bool Get(string name, ref Pathfinding.List value) {
        if (!m_data.ContainsKey(name) || m_data[name].GetDataType() != eBlackboardDataType.LIST) {
            return false;
        }

        value = m_data[name].GetList();
        return true;
    }

    #endregion

    #region Questions

    public void AddQuestion(BlackboardQuestion question) {
        m_questions.Add(question);
    }

    public void RemoveQuestion(BlackboardQuestion question) {
        m_questions.Remove(question);
    }

    public void ClearQuestions() {
        m_questions.Clear();
    }

    public void RunArbitration() {
        List<BlackboardQuestion> removeList = new List<BlackboardQuestion>();

        foreach (var question in m_questions) {
            if (question.Arbitrate(this)) {
                removeList.Remove(question);
            }
        }

        foreach (var question in removeList) {
            m_questions.Remove(question);
        }
    }

    #endregion
}
