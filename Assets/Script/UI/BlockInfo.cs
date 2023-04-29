using UnityEngine;
using UnityEngine.UI;

public class BlockInfo : MonoBehaviour
{
    GameObject window;
    Block currentBlock;

    void Start()
    {
        window = transform.GetChild(0).gameObject;
        window.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Block"))
                {
                    if (currentBlock != null) CloseWindow();

                    currentBlock = hit.collider.GetComponent<Block>();
                    currentBlock.Select();

                    window.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = currentBlock.data.domain; // GradeLevel label
                    window.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().text = currentBlock.data.grade; // GradeLevel label
                    window.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = currentBlock.data.cluster; // Cluster label
                    window.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>().text = currentBlock.data.standardid; // Standard label
                    window.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>().text = currentBlock.data.standarddescription; // Standard label

                    window.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) CloseWindow();
    }

    void CloseWindow()
    {
        window.SetActive(false);
        currentBlock.Deselect();
        currentBlock = null;
    }
}
