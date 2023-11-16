using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Some : MonoBehaviour
{
    
    public Rigidbody2D body;




    // Update is called once per frame
    private void Update()
    {
        NewGame();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trap")
        {
            gameObject.SetActive(false);
            AudioManger.Instance.PlaySFX("Died");
        }//要做到碰到collision之后彻底改变运动性质
    }
    private void NewGame()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Vector3 pos = new Vector3(-16, 4, 0);
            body.transform.position = pos;
            gameObject.SetActive(true);
            
            Debug.Log("666");
        }
    }
    
}
