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

    [SerializeField] LayerMask ground;

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
    }

    void SelectRabbit()
    {
        _currentSelection = _rabbit;
    }

    void SelectFox()
    {
        _currentSelection = _fox;
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentSelection != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;
            if (Physics.Raycast(ray, out hitData, 1000, ground))
            {
                Vector3 worldPosition = hitData.point;
                Instantiate(_currentSelection, worldPosition + Vector3.up * 0.1f, Quaternion.identity);

            }
        }
    }
}
