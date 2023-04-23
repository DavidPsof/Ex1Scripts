using UnityEngine;
using UnityEngine.UIElements;

namespace classes
{
    public class ItemMenu : MonoBehaviour
    {
        private UIController _uiController;

        private VisualElement _root;
        private Label _describtion;
        private Label _modalTitle;
        private GroupBox _container;
        private Button _cancelBtn;
        private Button _closeBtn;

        private void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _uiController = FindObjectOfType<UIController>();

            _describtion = _root.Q<Label>("describe");
            _container = _root.Q<GroupBox>("ItemMenuContainer");
            _closeBtn = _root.Q<Button>("CloseBtn");
            _cancelBtn = _root.Q<Button>("CancelBtn");
            _modalTitle = _root.Q<Label>("modal_name");
            _cancelBtn.clicked += Hide;
            _closeBtn.clicked += Hide;
        }

        public void Show(string name, string text)
        {
            _container.style.display = DisplayStyle.Flex;
            _describtion.text = text;
            _modalTitle.text = name;
            _uiController.SetState(UIController.UIStateType.ItemReview);
        }

        public void Hide()
        {
            _describtion.text = "";
            _container.style.display = DisplayStyle.None;
            _uiController.SetState(UIController.UIStateType.Procces);
        }
    }
}