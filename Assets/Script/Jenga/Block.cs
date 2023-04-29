using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Data
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}

public class DataComparer : IComparer<Data>
{
    public int Compare(Data x, Data y)
    {
        int domainComparison = x.domain.CompareTo(y.domain);
        if (domainComparison != 0)
        {
            return domainComparison;
        }

        int clusterComparison = x.cluster.CompareTo(y.cluster);
        if (clusterComparison != 0)
        {
            return clusterComparison;
        }

        return x.standardid.CompareTo(y.standardid);
    }
}

public class Block : MonoBehaviour
{
    [SerializeField]
    Material glassMaterial;
    float glassMass = 10f;

    [SerializeField]
    Material woodMaterial;
    float woodMass = 100f;

    [SerializeField]
    Material stoneMaterial;
    float stoneMass = 1000f;
    
    public Data data { get; private set; }

    public static float BLOCK_WIDTH = 1f;
    public static float BLOCK_HEIGHT = 1f;

    static Color selectColor = Color.cyan;
    static Color deselectColor = Color.white;

    void SetMaterial(int mastery)
    {
        Renderer renderer = GetComponent<Renderer>();
        Rigidbody rigidbody= GetComponent<Rigidbody>();
        if (mastery == 2) { renderer.material = stoneMaterial; rigidbody.mass = stoneMass; }
        else if (mastery == 1) { renderer.material = woodMaterial; rigidbody.mass = woodMass; }
        else { renderer.material = glassMaterial; rigidbody.mass = glassMass; }
    }
    public void SetData(Data data)
    {
        this.data = data;
        SetMaterial(data.mastery);
    }

    public void Select() => GetComponent<Renderer>().material.color = selectColor;
    public void Deselect() => GetComponent<Renderer>().material.color = deselectColor;
    
}
