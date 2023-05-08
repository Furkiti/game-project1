using Gameplay.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]private LayerMask tileLayerMask;
        [SerializeField]private Button regenerateBoardButton;
        [SerializeField]private TMP_InputField boardSizeInputField;
      
        
        private void OnEnable()
        {
            EventManager.OnBoardCreated += BoardCreated;
            regenerateBoardButton.onClick.AddListener(RegenerateButtonClicked);
        }
        
        private void OnDisable()
        {
            EventManager.OnBoardCreated -= BoardCreated;
            regenerateBoardButton.onClick.RemoveListener(RegenerateButtonClicked);
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnMouseClick();
            }
        }
        
        private void OnMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray from the mouse position into the game world
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero,tileLayerMask);
            
                // check if the ray hits a object
                if (hit.collider != null)
                {
                    // get the clickable component of the hit object
                    IClickable clickable = hit.collider.GetComponent<IClickable>();

                    // if the hit object is clickable
                    if (clickable != null)
                    {
                        EventManager.OnTileClicked?.Invoke(clickable);
                        clickable.OnClick();
                    }
                }
            }
        }

       

        private void BoardCreated(int newBoardSize)
        {
            //int currentBoardSize = int.Parse(boardSizeInputField.text);
            boardSizeInputField.text = newBoardSize.ToString();
        }

        private void RegenerateButtonClicked()
        {
            int newboardSize = int.Parse(boardSizeInputField.text);
            newboardSize = Mathf.Clamp(newboardSize, 1, 20);
            EventManager.OnRegenerateButtonClicked?.Invoke(newboardSize);
        }
    }
}
