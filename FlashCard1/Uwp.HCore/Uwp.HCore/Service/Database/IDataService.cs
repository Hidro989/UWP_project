using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.Model.Model;

namespace Uwp.HCore.Service.Database
{
    public interface IDataService
    {
        #region TopicData
        ObservableCollection<Topic> GetTopicData();
        void InsertTopicData(Topic topic);

        void UpdateTopicData(Topic topic);

        void DeleteTopicData(Topic topic);

        ObservableCollection<Topic> GetTopicDataByName(string name);

        #endregion

        #region CardData
        ObservableCollection<Card> GetCardData(int id);

        void InsertCardData(Card card);

        void UpdateCardData(Card card);

        void DeleteCardData(Card card);
        #endregion
        void RemoveAllData();
    }
}
