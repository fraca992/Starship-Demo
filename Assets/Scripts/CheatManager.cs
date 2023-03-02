using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    #region VARIABLES
    //caches
    private FuelManager m_FuelManager;
    private CollisionHandler m_CollisionHandler;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_FuelManager = this.GetComponent<FuelManager>();
        m_CollisionHandler = this.GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_CollisionHandler.LoadNextLevel();
            Debug.Log("Cheat used: load next level");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_FuelManager.ReloadLevel();
            Debug.Log("Cheat used: reload level");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CollisionHandler.isEnabled = !m_CollisionHandler.isEnabled;
            Debug.Log("Cheat used: toggle Collisions");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            m_FuelManager.isEnabled = !m_FuelManager.isEnabled;
            Debug.Log("Cheat used: toggle Fuel bar");
        }
    }
}
