using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum selected{

}

public class ItemPlacer : MonoBehaviour
{
    [SerializeField] Button _rabbitButton;
    [SerializeField] Button _foxButton;
    [SerializeField] Button _plantButton;
    [SerializeField] Button _snakeButton;
    [SerializeField] Button _burrowButton;
    [SerializeField] Button _ratButton;

    [SerializeField] GameObject _rabbit;
    [SerializeField] GameObject _fox;
    [SerializeField] GameObject _plant;
    [SerializeField] GameObject _snake;
    [SerializeField] GameObject _burrow;
    [SerializeField] GameObject _rat;

    [SerializeField] GameObject crosshair;
    Color crosshairColor = Color.green;

    GameObject _currentSelection;

    public int numFoxesAvailable = 5;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelection = null;
        _plantButton.onClick.AddListener(SelectPlant);
        _rabbitButton.onClick.AddListener(SelectRabbit);
        _foxButton.onClick.AddListener(SelectFox);
        _snakeButton.onClick.AddListener(SelectSnake);
        _burrowButton.onClick.AddListener(SelectBurrow);
        _ratButton.onClick.AddListener(SelectRat);

        _foxButton.gameObject.SetActive(false);
        _rabbitButton.gameObject.SetActive(false);
        _plantButton.gameObject.SetActive(false);
        _snakeButton.gameObject.SetActive(false);
        _burrowButton.gameObject.SetActive(false);
        _ratButton.gameObject.SetActive(false);

    }

    void SelectPlant()
    {
        _currentSelection = _plant;
        crosshairColor = Color.green;
    }

    void SelectRabbit()
    {
        _currentSelection = _rabbit;
        crosshairColor = new Color(0.3f, 0.3f, 0.3f);
    }

    void SelectFox()
    {
        _currentSelection = _fox;
        crosshairColor = new Color(1.0f, 0.64f, 0.0f);
    }
    
    void SelectSnake()
    {
        _currentSelection = _snake;
        crosshairColor = new Color(1.0f, 0.27f, 0.0f);
    }
    
    void SelectBurrow()
    {
        _currentSelection = _burrow;
        crosshairColor = new Color(0.3f, 0.3f, 0.3f);
    }
    
    void SelectRat()
    {
        _currentSelection = _rat;
        crosshairColor = new Color(1.0f, 0.64f, 0.0f);
    }

    public void EnablePlantButton()
    {
        _plantButton.gameObject.SetActive(true);
    }
    public void EnableRabbitButton()
    {
        _rabbitButton.gameObject.SetActive(true);
    }
    public void EnableFoxButton()
    {
        _foxButton.gameObject.SetActive(true);
    }
    
    public void EnableSnakeButton()
    {
        _snakeButton.gameObject.SetActive(true);
    }
    
    public void EnableBurrowButton()
    {
        _burrowButton.gameObject.SetActive(true);
    }
    
    public void EnableRatButton()
    {
        _ratButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //RAYCAST FOR CROSSHAIRS
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, 1000))
        {
            crosshair.transform.position = hit.point + new Vector3(0, 0.4f, 0);

            if (_currentSelection == null || hit.collider.gameObject.CompareTag(_currentSelection.tag) || (_currentSelection.CompareTag("P") && hit.collider.CompareTag("Plant")))
            {
                crosshair.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                crosshair.GetComponent<SpriteRenderer>().color = crosshairColor;
            }
        }


        //RAYCAST FOR ITEM PLACEMENT
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentSelection != null && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000))
            {
                //don't place on top of other object of same type, with special check for plants
                if (hitData.collider.CompareTag(_currentSelection.tag)  || (_currentSelection.CompareTag("P") && hit.collider.CompareTag("Plant")))
                {
                    return;
                }
                
                if(_currentSelection == _fox && numFoxesAvailable <= 0)
                {
                    return;
                }

                Vector3 worldPosition = hitData.point;

                Instantiate(_currentSelection, worldPosition + Vector3.up * 0.7f, Quaternion.identity);

                if(_currentSelection == _fox)
                {
                    numFoxesAvailable -= 1;
                }
                
            }
        }

    }
}
