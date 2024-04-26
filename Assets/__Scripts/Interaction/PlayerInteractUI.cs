using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Image inputIconImage;
    [SerializeField] private Sprite[] inputIcons;
    

    public void Show(Target target, int iconIndex)
    {
        containerGameObject.SetActive(true);
        interactText.text = target.interactText;
        inputIconImage.sprite = inputIcons[iconIndex];
    }
    
    public void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
