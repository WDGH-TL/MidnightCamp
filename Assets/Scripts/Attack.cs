using UnityEngine;

public class Attack : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
