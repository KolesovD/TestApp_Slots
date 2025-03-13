using AxGrid.Base;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AxGrid.Tools.Binders
{
    [RequireComponent(typeof(InputField))]
    public class UIInputFieldBind : Binder
    {
        private InputField _inputField;

        /// <summary>
		/// Название поля ввода
		/// </summary>
		[SerializeField] private string _inputFieldName;
        public string InputFieldName => _inputFieldName;

        /// <summary>
		/// Название поля в модели, где будет храниться текст
		/// </summary>
		[SerializeField] private string _IFTextModelField;
        public string IFTextModelField => _IFTextModelField;

        /// <summary>
		/// Название поля в модели, где будет храниться текст
		/// </summary>
		[SerializeField] private string _IFFinalTextModelField;
        public string IFFinalTextModelField => _IFFinalTextModelField;

        public const string TEXT_PART = "Text";
        public const string FINAL_TEXT_PART = "FinalText";

        [OnAwake]
        private void OnAwake()
        {
            _inputField = GetComponent<InputField>();

            //Split-Join должен быть самым быстрым
            if (string.IsNullOrEmpty(_inputFieldName))
                _inputFieldName = string.Join("", name.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrEmpty(_IFTextModelField))
                _IFTextModelField = $"{_inputFieldName}{TEXT_PART}";

            if (string.IsNullOrEmpty(_IFFinalTextModelField))
                _IFFinalTextModelField = $"{_inputFieldName}{FINAL_TEXT_PART}";
        }

        [OnStart]
        private void OnStart()
        {
            _inputField.onValueChanged.AddListener(OnInputChanged);
            OnInputChanged(_inputField.text);

            _inputField.onEndEdit.AddListener(OnEndEdit);
            OnEndEdit(_inputField.text);
        }

        private void OnInputChanged(string newValue)
        {
            Model.Set(_IFTextModelField, newValue);
        }

        private void OnEndEdit(string newValue)
        {
            Model.Set(_IFFinalTextModelField, newValue);

            Settings.Fsm?.Invoke("OnInputChanged", _inputFieldName);
        }

        [OnDestroy]
        private void OnDestroyInner()
        {
            _inputField.onValueChanged.RemoveAllListeners();
            _inputField.onEndEdit.RemoveAllListeners();
        }
    }
}
