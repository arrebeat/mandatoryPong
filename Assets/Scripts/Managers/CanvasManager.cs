using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public List<GameObject> fileObjects = new List<GameObject>(); 
    public List<GameObject> gridSlotObjects = new List<GameObject>();
    public float delayBetweenGridSlots;

    private Coroutine _currentCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GridShow()
    {
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(GridEnter());
        }
        else return;
    }

    public IEnumerator GridEnter()
    {
        foreach (var obj in gridSlotObjects)
        {
            yield return new WaitForSeconds(delayBetweenGridSlots);
            obj.SetActive(true);
            obj.GetComponent<Animator>().SetTrigger("Enter");
        }
    }
}
