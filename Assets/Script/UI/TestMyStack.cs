using UnityEngine;

public class TestMyStack : MonoBehaviour
{
    bool isStackTested = false;

    public void Activate()
    {
        if (!isStackTested)
        {
            JengaBuilder.Instance.TestMyStack();
            isStackTested = true;
        }
    }
}
