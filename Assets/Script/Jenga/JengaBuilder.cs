using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class JengaBuilder : MonoBehaviour
{
    Dictionary<int, JengaTower> towers;

    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject towerPrefab;

    [SerializeField] float gap = 10f; // gap between towers

    public static JengaBuilder Instance { get; private set; }

    private void Awake()
    {
        towers = new Dictionary<int, JengaTower>();
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void CreateTower(int grade)
    {
        float towerX = grade * JengaTower.TOWER_WIDTH + (grade - 1) * gap;
        float towerY = transform.position.y;
        float towerZ = transform.position.z;

        JengaTower tower = Instantiate(towerPrefab, new Vector3(towerX, towerY, towerZ), Quaternion.identity).GetComponent<JengaTower>();
        tower.SetText(grade);
        towers.Add(grade, tower);
    }

    public void CreateBlock(Data data)
    {
        int GetGrade(string grade)
        {
            string result = string.Empty;
            
            for (int i = 0; i < grade.Length; i++) if (Char.IsDigit(grade[i])) result += grade[i];
            int val = result.Length > 0 ? int.Parse(result) : -1;
            return val;
        }

        int grade = GetGrade(data.grade);
        if (grade == -1) return;

        if (!towers.Keys.Contains(grade)) CreateTower(grade);

        Block block = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity).GetComponent<Block>();
        block.SetData(data);

        towers[grade].AddBlock(ref block);
    }

    public Vector3 GetTowerPos(int grade) => towers[grade].transform.position;
    public bool IsThereTower(int grade) => towers.Keys.Contains(grade);
    
    public void TestMyStack()
    {
        foreach (int grade in towers.Keys)
        {
            towers[grade].RemoveGlassBlocks();
            towers[grade].ActivateGravity();
        }
    }
}
