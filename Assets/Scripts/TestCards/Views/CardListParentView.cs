using AxGrid.Base;
using UnityEngine;

namespace TestCards.Views
{
    public class CardListParentView : MonoBehaviourExt
    {
        [SerializeField] private string _listId;
        public string ListId => _listId;
    }
}
