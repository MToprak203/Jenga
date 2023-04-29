using System.Collections.Generic;
using UnityEngine;

public class JengaTower : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro gradeText;
    List<Block> blocks;

    int height; // number of layers
    int width; // number of block at the top layer.
    public static float TOWER_WIDTH; // max tower width. 3 blocks.

    void Awake() { blocks = new List<Block>(); }

    void Start()
    {
        height = 1;
        width = 0;

        TOWER_WIDTH = 3 * Block.BLOCK_WIDTH;
    }

    public void AddBlock(ref Block block)
    {
        if (width == 3) // layer is full
        {
            height++;
            width = 0;
        }
        width++;

        float blockX = width * Block.BLOCK_WIDTH;
        float blockY = height * Block.BLOCK_HEIGHT;

        block.transform.parent = transform;
        if (height % 2 == 0)
        {
            block.transform.position = new Vector3(
                transform.position.x - Block.BLOCK_WIDTH + blockX, 
                transform.position.y + Block.BLOCK_HEIGHT / 2 + blockY, 
                transform.position.z
                );
            block.transform.Rotate(Vector3.zero);
        }
        else
        {
            block.transform.Rotate(new Vector3(0f, 90f, 0f));
            block.transform.position = new Vector3(
                transform.position.x + Block.BLOCK_WIDTH,
                transform.position.y + Block.BLOCK_HEIGHT / 2 + blockY, transform.position.z +
                blockX - 2*Block.BLOCK_WIDTH
                );
        }

        blocks.Add(block);
    }

    public void SetText(int grade) => gradeText.text = grade.ToString() + "th grade";
    
    public void RemoveGlassBlocks() { foreach (Block block in blocks) if (block.data.mastery == 0) block.gameObject.SetActive(false); }

    public void ActivateGravity() { foreach (Block block in blocks) if (block.gameObject.activeSelf) block.GetComponent<Rigidbody>().useGravity = true; }
}
