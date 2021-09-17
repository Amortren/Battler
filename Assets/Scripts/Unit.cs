using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int damage;
    public float atackSpeed;
    public Vector2Int Size = Vector2Int.one;
    public Transform hpMetka;
    [SerializeField]
    private Slider hpSlider;
    Slider myShowHP;
    public Camera myCamera;
    private Canvas myUI;
    public int currentHP;
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float distance;
    private int dmg;
    private Enemy[] enemies;
    private Enemy cloasest;
    public float enemyAS;
    private Enemy standart;


    private void Awake()
    {
        Canvas UI = (Canvas)FindObjectOfType(typeof(Canvas));
        myUI = UI;

        

    }




    private void Start()
    {
        currentHP = health;

        
        myShowHP = (Slider)Instantiate(hpSlider);
        myShowHP.transform.SetParent(myUI.transform, true);
        
        myShowHP.maxValue = health;
        myShowHP.value = currentHP;


       
        
        

    }

    
    void Update()
    {
        
        if (currentHP <= 0) 
        {
            Destroy(myShowHP.gameObject);
            Destroy(this.gameObject);
            
        }
        enemies = FindObjectsOfType<Enemy>();
        FindCloasestEnemy();

        enemyAS = cloasest.atackSpeed;

        if (Vector3.Distance(transform.position, cloasest.transform.position) > distance)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, cloasest.transform.position, speed * Time.deltaTime);
        }
        //
        //{
        //    if (Vector3.Distance(transform.position, ego.transform.position) <= distance)
        //    {
        //        dmg += ego.damage;
        //    }
        //    //else
        //    //{
        //    //    if (cloasest.currentHP == 0)
        //    //    {
        //    //        dmg -= cloasest.damage;
        //    //        Heal();

        //    //    }
        //    //}

        //}
        foreach (Enemy ego in enemies)
        {

            if (Vector3.Distance(transform.position, ego.transform.position) <= distance)
            {
                StartCoroutine(GetDamage());
                if (ego.currentHP <= 0)
                {
                    Heal();
                }
            }
        }


        if (myShowHP != null)
        {
            Vector3 screenPos = hpMetka.position;
            myShowHP.transform.position = screenPos;
            myShowHP.value = currentHP;
        }


    }
    IEnumerator GetDamage()
    {

        currentHP -= cloasest.damage;
        yield return new WaitForSeconds(enemyAS);
            
        


    }
    private void Heal()
    {
        currentHP = health;
    }
    public Enemy FindCloasestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Enemy go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance<distance)
            {
                cloasest = go;
                distance = curDistance;
            }

        }
        return cloasest;
        
    }
}
