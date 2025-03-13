using AxGrid.Base;
using System;
using TMPro;
using UnityEngine;

namespace AxGrid.Tools.Binders
{
    [RequireComponent(typeof(TMP_InputField))]
    public class UITMPInputFieldBind : Binder
    {
        private TMP_InputField _inputField;

        /// <summary>
		/// Название поля ввода
		/// </summary>
		[SerializeField] private string _inputFieldName;
        public string InputFieldName => _inputFieldName;

        /// <summary>
		/// Название поля в модели, где будет храниться текст
		/// </summary>
		[SerializeField] private string _textModelField;
        public string TextModelField => _textModelField;

        /// <summary>
		/// Название поля в модели, где будет храниться текст
		/// </summary>
		[SerializeField] private string _finalTextModelField;
        public string FinalTextModelField => _finalTextModelField;

        public const string TEXT_PART = "Text";
        public const string FINAL_TEXT_PART = "FinalText";

        [OnAwake]
        private void OnAwake()
        {
            _inputField = GetComponent<TMP_InputField>();

            //Split-Join должен быть самым быстрым
            if (string.IsNullOrEmpty(_inputFieldName))
                _inputFieldName = string.Join("", name.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            if (string.IsNullOrEmpty(_textModelField))
                _textModelField = $"{_inputFieldName}{TEXT_PART}";

            if (string.IsNullOrEmpty(_finalTextModelField))
                _finalTextModelField = $"{_inputFieldName}{FINAL_TEXT_PART}";
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
            Model.Set(_textModelField, newValue);
        }

        private void OnEndEdit(string newValue)
        {
            Model.Set(_finalTextModelField, newValue);

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
