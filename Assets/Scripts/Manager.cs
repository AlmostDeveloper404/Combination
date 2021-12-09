using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    [HideInInspector] public float CanvasScaleFactor;

    private int _mixRate = 0;


    [SerializeField] Element[] _allElements;
    private Element[] _elementsToTwig;
    private int _elementsInConstruction = 5;

    private int _selectionOrder = 0;
    private const int _maxElementsInChoosingPanal = 5;

    [Header("Start Values")]
    [SerializeField] float _timeToHideElements = 5f;
    [SerializeField] int _scoreToAdd = 100;
    [SerializeField] float _timeToRepeat = 20f;
    [SerializeField] int _reduseCellSizeOfElement = 10;

    [Header("Adjust Difficulty")]
    [SerializeField] float _timeDecreasing = 1f;
    [SerializeField] int _extraScoreForLevelOfDifficulty = 50;
    [SerializeField] float _timeReducePercentForEachDifficulty = 0.9f;
    [SerializeField] int _extraCellReduceEachLevel = 5;


    [Header("Reference Panals")]
    [SerializeField] Transform _completeStructure;
    [SerializeField] Transform _choosingPanal;
    [SerializeField] GridLayoutGroup _gridLayoutGroup;

    List<Element> _elementsToComplete = new List<Element>();
    List<Element> _elementsAccessible = new List<Element>();

    public UnityEvent OnGameOver;



    private void Start()
    {
        CanvasScaleFactor = GetComponent<Canvas>().scaleFactor;

        Timer.Instance.SetTime(_timeToRepeat);
        SetupElements(_elementsInConstruction);
    }

    public void SetupElements(int numberOfElements)
    {
        Clear();
        LockTypesOfElements();

        //MixArraySystem.MixArray(ref _elementsToTwig);
        for (int i = 0; i < numberOfElements; i++)
        {
            int randomIndex = Random.Range(0, _elementsToTwig.Length);

            Element newElement = Instantiate(_elementsToTwig[randomIndex], _completeStructure);
            _elementsToComplete.Add(newElement);
        }


        MixArraySystem.MixArray(ref _elementsToTwig);
        for (int i = 0; i < _maxElementsInChoosingPanal; i++)
        {
            Element newElement = Instantiate(_elementsToTwig[i], _choosingPanal);
            _elementsAccessible.Add(newElement);
        }


        StartCoroutine(Hide());
    }

    private void LockTypesOfElements()
    {
        _elementsToTwig = new Element[_elementsInConstruction];
        for (int i = 0; i < _elementsToTwig.Length; i++)
        {
            _elementsToTwig[i] = _allElements[i];
        }
    }

    public void SetElement(Element element)
    {

        if (element.Index == _elementsToComplete[_selectionOrder].Index)
        {
            _elementsToComplete[_selectionOrder].gameObject.SetActive(true);
            _selectionOrder++;
            if (_selectionOrder == _elementsInConstruction)
            {
                ScoreCounter.Instance.UpdateScore(_scoreToAdd);
                Clear();
                SetupElements(_elementsInConstruction);
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

        _selectionOrder = 0;

        for (int i = 0; i < _elementsToComplete.Count; i++)
        {
            Destroy(_elementsToComplete[i].gameObject);
        }
        for (int i = 0; i < _elementsAccessible.Count; i++)
        {
            Destroy(_elementsAccessible[i].gameObject);
        }

        Timer.Instance.SetTime(_timeToRepeat);
        _elementsAccessible.Clear();
        _elementsToComplete.Clear();
    }

    void ResetElements()
    {
        foreach (Element element in _elementsToComplete)
        {
            element.gameObject.SetActive(false);
            _selectionOrder = 0;
        }
    }

    public void DeleteElement(Element element)
    {
        _elementsAccessible.Remove(element);

        Destroy(element.gameObject);
    }

    public void AddNewRandomElement()
    {
        MixArraySystem.MixArray(ref _elementsToTwig);

        Element newElement = Instantiate(_elementsToTwig[0], _choosingPanal);
        _elementsAccessible.Add(newElement);
    }

    IEnumerator Hide()
    {
        Timer.Instance.StopTimer();
        yield return new WaitForSeconds(_timeToHideElements);
        foreach (Element element in _elementsToComplete)
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
        _reduseCellSizeOfElement += _extraCellReduceEachLevel;
        _elementsInConstruction++;
        _timeToRepeat *= _timeReducePercentForEachDifficulty;
        _scoreToAdd += _extraScoreForLevelOfDifficulty;
        _timeToHideElements -= _timeDecreasing;

        Vector2 cellSize = _gridLayoutGroup.cellSize;
        cellSize = new Vector2(cellSize.x - _reduseCellSizeOfElement, cellSize.y - _reduseCellSizeOfElement);

        _gridLayoutGroup.cellSize = cellSize;



    }

}
