using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.Model.Model;

namespace Uwp.HCore.Service.Database
{

    public class DataService : IDataService
    {
        private static DataContext _db = new DataContext();
        public static DataContext Db { get { return _db; } }

        #region TopicData
        public void DeleteTopicData(Topic topic)
        {
            //Example
            //db.Remove(item);
            //db.SaveChanges();

            Db.Remove(topic);
            Db.SaveChanges();
        }
        public ObservableCollection<Topic> GetTopicData()
        {

            // return  new ObservableCollection<object>(db.Object.ToList());
            return new ObservableCollection<Topic>(Db.Topics.Include(t => t.Cards).ToList());
        }
        public void InsertTopicData(Topic topic)
        {
            //Example
            //db.Add(new NgayMua(item.Name, item.Money, item.AmountGuest, item.NameGuests, item.DateBuy));
            //db.SaveChanges();
            //-----

            Db.Add(topic);
            Db.SaveChanges();
        }

        public void UpdateTopicData(Topic topic)
        {

            //Example
            //Db.Update(topic);
            //Db.SaveChanges();

            Db.Update(topic);
            Db.SaveChanges();
        }
        public ObservableCollection<Topic> GetTopicDataByName(string name)
        {
            return new ObservableCollection<Topic>(Db.Topics.Where(t => t.Name == name).Include(t => t.Cards).ToList());
        }
        #endregion TopicData

        #region CardData
        public ObservableCollection<Card> GetCardData(int id)
        {
           return new ObservableCollection<Card>(Db.Cards.Where(c => c.TopicId == id).Include(c => c.Topic).ToList());
        }

        public void InsertCardData(Card card)
        {
            Db.Add(card);
            Db.SaveChanges();
        }

        public void UpdateCardData(Card card)
        {
            Db.Update(card);
            Db.SaveChanges();
        }

        public void DeleteCardData(Card card)
        {
            Db.Remove(card);
            Db.SaveChanges();
        }

        #endregion
        public void RemoveAllData()
        {
            //Example
            //db.Ngaymuas.RemoveRange(db.Ngaymuas);
            //db.SaveChanges();

            throw new NotImplementedException();
        }
    }
}
