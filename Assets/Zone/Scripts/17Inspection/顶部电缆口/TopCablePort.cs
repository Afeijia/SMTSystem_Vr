using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCablePort : MonoBehaviour
{
    /// <summary>
    /// 已被使用
    /// </summary>
    public bool inUsed;

    public TopCable Cable;

    public Transform cable_place;

    public LimitBuckle Buckle;

    public Transform buckle_place;
    public Vector3 release_in_pos,tighten_in_pos;

    // Start is called before the first frame update
    void Start()
    {
        Cable = GetComponentInChildren<TopCable>();
        inUsed = Cable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
