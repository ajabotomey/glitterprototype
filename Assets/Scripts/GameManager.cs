using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Stores all behaviours for the various AI entities
    public Blackboard m_blackboard;

    void Awake() {
        instance = this;
        m_blackboard = new Blackboard();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
