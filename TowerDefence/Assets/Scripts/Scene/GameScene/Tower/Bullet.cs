using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Transform attackTarget;
    public Vector3 startPosition;
    float distance;
    [HideInInspector]
    public float bulletSpeed;
    bool isStop = false;
    float startTime;
    public float elapsedTime;
    public Tower m_tower;
    
    private void Start()
    {
        distance = Vector3.Distance(attackTarget.position,startPosition);
    }
    private void Update()
    {
        if(AttackTargetCheck())
        {
            AttackTargetMove();
        }
        else
        {
            attackTarget = null;   
            PoolManager.Ins.minipools[Global_Define.PoolType.Bullet].Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void AttackTargetMove()
    {
        elapsedTime += Time.deltaTime;
        
        float total =(elapsedTime*bulletSpeed)/distance;
        if(attackTarget!=null)
        {
            Vector3 prePosition = transform.position;
           transform.position = Vector3.Lerp(startPosition,attackTarget.position, total);
            Vector3 afterPosition = transform.position;
            transform.up = (afterPosition - prePosition).normalized;
        }
    }

    public bool AttackTargetCheck()
    {
        bool temp = false;

        if(attackTarget.gameObject.activeInHierarchy==true)
        {
            temp= true;
        }
        else
        {
            temp = false;
        }
        return temp;        
    }

}
