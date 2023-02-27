using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    #region Variables
    // PARAMETERS: for tuning, typically set in the editor
    [Header("EditorHeader")]
    [SerializeField] private int levelSpeed;

    // CACHE: e.g. references for readability or speed
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject Player;

    // STATE: private instance (member) variables
    private bool hasMoved;
    private int levelIndex;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
