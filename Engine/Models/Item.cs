using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Item
    {
        private int id;
        private string name;
        private bool isKeyItem=false;
        private int buyPrice=0;
        private int sellPrice=0;
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public bool IsKeyItem
        {
            get { return isKeyItem; }
        }
        public int BuyPrice
        {
            get { return buyPrice; }
        }
        public int SellPrice
        {
            get { return sellPrice; }
        }
        public Item(int inId, string inName,bool isKey, int inBuy, int inSell)
        {
            id = inId;
            name = inName;
            isKeyItem = isKey;
            buyPrice = inBuy;
            sellPrice = inSell;
        }
        public Item(int inId, string inName, int inBuy, int inSell):
            this(inId, inName, false, inBuy, inSell)
        {
        }
        public Item(int inId, string inName, int inBuy) :
            this(inId, inName, inBuy, 0)
        {
        }
        public Item(int inId, string inName, bool isKey):
            this(inId, inName, isKey, 0,0)
        {
        }

        public virtual Item Clone()
        {
            return new Item(id, name, isKeyItem, buyPrice, sellPrice);
        }

    }
}
