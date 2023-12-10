using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum selected{

}

public class ItemPlacer : MonoBehaviour
{
    [SerializeField] Button _rabbitButton;
    [SerializeField] Button _foxButton;
    [SerializeField] Button _plantButton;

    [SerializeField] GameObject _rabbit;
    [SerializeField] GameObject _fox;
    [SerializeField] GameObject _plant;

    [SerializeField] GameObject crosshair;
    Color crosshairColor = Color.green;

    GameObject _currentSelection;

    // Start is called before the first frame update
    void Start()
    {
        _currentSelection = null;
        _plantButton.onClick.AddListener(SelectPlant);
        _rabbitButton.onClick.AddListener(SelectRabbit);
        _foxButton.onClick.AddListener(SelectFox);

        _foxButton.gameObject.SetActive(false);
        _rabbitButton.gameObject.SetActive(false);
        _plantButton.gameObject.SetActive(false);

    }

    void SelectPlant()
    {
        _currentSelection = _plant;
        crosshairColor = Color.green;
    }

    void SelectRabbit()
    {
        _currentSelection = _rabbit;
        crosshairColor = new Color(0.7f, 0.7f, 0.7f);
    }

    void SelectFox()
    {
        _currentSelection = _fox;
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

    // Update is called once per frame
    void Update()
    {
        //RAYCAST FOR CROSSHAIRS
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit, 1000))
        {
            crosshair.transform.position = hit.point + new Vector3(0, 0.4f, 0);

            if (_currentSelection == null || hit.collider.gameObject.CompareTag(_currentSelection.tag))
            {
                crosshair.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                crosshair.GetComponent<SpriteRenderer>().color = crosshairColor;
            }
        }


        //RAYCAST FOR ITEM PLACEMENT
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentSelection != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000))
            {
                //don't place on top of other object of same type
                if (!hitData.collider.gameObject.CompareTag(_currentSelection.tag))
                {
                    Vector3 worldPosition = hitData.point;
                    Instantiate(_currentSelection, worldPosition + Vector3.up * 0.1f, Quaternion.identity);
                }
            }
        }

    }
}
