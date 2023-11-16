using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antiphysical_Jump : MonoBehaviour
{
    public float jumpforce = 10.0f;
    bool ifjumping;
    // 跳跃间隔时间
    public float jumpduration = 0.5f;

    void Start()
    {
        ifjumping = false;
    }

    void Update()
    {
        if (!ifjumping && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpCoroutine());
            ifjumping = true;
        }
    }
    // 协程实现平滑跳跃  
    IEnumerator JumpCoroutine()
    {
        float elapsedTime = 0; // 已过去的时间  
        float startpos = transform.position.y; // 初始y位置  
        float targetpos = startpos + jumpforce; // 目标y位置  

        while (elapsedTime < jumpduration)
        {
            // 插值计算当前垂直位置  
            float currentpos = Mathf.Lerp(startpos, targetpos, elapsedTime / jumpduration);
            // 更新Transform组件的垂直位置  
            transform.position = new Vector3(transform.position.x, currentpos, transform.position.z);
            // 协程等待下一帧继续执行  
            yield return null;
            // 累加已过去的时间  
            elapsedTime += Time.deltaTime;
        }

        // 跳跃结束，设置垂直位置为目标位置  
        transform.position = new Vector3(transform.position.x, targetpos, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ifjumping = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ifjumping = false;
        }
    }
}
