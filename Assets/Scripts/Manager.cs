using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Manager : Singleton<Manager>
{
    [HideInInspector]public float CanvasScaleFactor;

    [SerializeField]Element[] _allElements;

    [SerializeField] float _timeToHideElements = 2f;

    private int selectionOrder = 0;

    [SerializeField]int elementsInColumn = 5;
    private int amountOfVisibleElements = 5;

    [SerializeField] int _scoreToAdd = 100;
    [SerializeField] float _timeToRepeat=20f;
    
    public Transform CompleteStructure;
    public Transform ChoosingPanal;

    List<Element> elementsToComplete = new List<Element>();
    List<Element> elementsAccessible = new List<Element>();

    public UnityEvent OnGameOver;

    

    private void Start()
    {
        CanvasScaleFactor = GetComponent<Canvas>().scaleFactor;

        Timer.Instance.SetTime(_timeToRepeat);
        SetupElements(elementsInColumn);        
    }
    
    public void SetupElements(int numberOfElements)
    {
        Clear();
        for (int i = 0; i < numberOfElements; i++)
        {           
            int randomIndex = Random.Range(0,_allElements.Length);

            Element newElement = Instantiate(_allElements[randomIndex], CompleteStructure);
            elementsToComplete.Add(newElement);
        }

        for (int i = 0; i < amountOfVisibleElements; i++)
        {          
            int randomIndex = Random.Range(0, _allElements.Length);

            Element newElement = Instantiate(_allElements[randomIndex], ChoosingPanal);
            elementsAccessible.Add(newElement);
        }


        StartCoroutine(Hide());
    }

    public void SetElement(Element element)
    {
        
        if (element.Index == elementsToComplete[selectionOrder].Index)
        {
            elementsToComplete[selectionOrder].gameObject.SetActive(true);
            selectionOrder++;
            if (selectionOrder == elementsInColumn)
            {
                Debug.Log("Yep");
                ScoreCounter.Instance.UpdateScore(_scoreToAdd);
                Clear();
                SetupElements(elementsInColumn);
                return;
            }
            

        }
        else
        {
            ResetElements();
        }
        DeleteElement(element);
        AddNewRandomElement();
    }

    void Clear()
    {

        selectionOrder = 0;

        for (int i = 0; i < elementsToComplete.Count; i++)
        {
            Destroy(elementsToComplete[i].gameObject);          
        }
        for (int i = 0; i < elementsAccessible.Count; i++)
        {
            Destroy(elementsAccessible[i].gameObject);
        }

        Timer.Instance.SetTime(_timeToRepeat);
        elementsAccessible.Clear();
        elementsToComplete.Clear();
    }

    void ResetElements()
    {
        foreach (Element element in elementsToComplete)
        {
            element.gameObject.SetActive(false);
            selectionOrder = 0;
        }
    }

    public void DeleteElement(Element element)
    {
        elementsAccessible.Remove(element);

        Destroy(element.gameObject);
    }

    public void AddNewRandomElement()
    {
        int randomIndex = Random.Range(0,_allElements.Length);
        Element newElement = Instantiate(_allElements[randomIndex],ChoosingPanal);
        elementsAccessible.Add(newElement);
    }

    IEnumerator Hide()
    {
        Timer.Instance.StopTimer();
        yield return new WaitForSeconds(_timeToHideElements);
        foreach (Element element in elementsToComplete)
        {
            element.gameObject.SetActive(false);
        }
        Timer.Instance.StartTimer();
    }

    public void GameOver()
    {
        OnGameOver.Invoke();
    }

    public void RaiseDifficulty()
    {

    }

}
