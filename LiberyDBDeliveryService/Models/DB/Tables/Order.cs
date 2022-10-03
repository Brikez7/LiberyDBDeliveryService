using System;
using System.Collections.Generic;

namespace LiberyDBDeliveryService.Models.DB.Table
{
    public partial class Order
    {
        public long? IdOrder { get; set; }
        public string DateOrder { get; set; } = null!;
        public short StatusOrder { get; set; }
        public string TypeMovement { get; set; } = null!;
        public string Product { get; set; } = null!;
        public double Weight { get; set; }
        public string AddresWarehouse { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public string PhoneSenders { get; set; } = null!;
        public string PhoneClient { get; set; } = null!;
        public string AddresClient { get; set; } = null!;
        public string? FenceTime { get; set; }
        public double Deposit { get; set; }
        public double Price { get; set; }
        public string? Describe { get; set; }
        public long? IdTelegramDeliver { get; set; }
        public long IdTelegramShop { get; set; }

        public Order(long idOrder)
        {
            IdOrder = idOrder;
        }

        public Order(string dateOrder, short statusOrder, string typeMovement, string product, double weight, string addresWarehouse, string deliveryTime, string phoneSenders, string phoneClient, string addresClient, string? fenceTime, double deposit, double price, string? describe, long idTelegramShop)
        {
            DateOrder = dateOrder;
            StatusOrder = statusOrder;
            TypeMovement = typeMovement;
            Product = product;
            Weight = weight;
            AddresWarehouse = addresWarehouse;
            DeliveryTime = deliveryTime;
            PhoneSenders = phoneSenders;
            PhoneClient = phoneClient;
            AddresClient = addresClient;
            FenceTime = fenceTime;
            Deposit = deposit;
            Price = price;
            Describe = describe;
            IdTelegramShop = idTelegramShop;
        }

        public Order(long idOrder, string dateOrder, short statusOrder, string typeMovement, string product, double weight, string addresWarehouse, string deliveryTime, string phoneSenders, string phoneClient, string addresClient, string? fenceTime, double deposit, double price, string? describe, long idTelegramShop) : this(idOrder)
        {
            DateOrder = dateOrder;
            StatusOrder = statusOrder;
            TypeMovement = typeMovement;
            Product = product;
            Weight = weight;
            AddresWarehouse = addresWarehouse;
            DeliveryTime = deliveryTime;
            PhoneSenders = phoneSenders;
            PhoneClient = phoneClient;
            AddresClient = addresClient;
            FenceTime = fenceTime;
            Deposit = deposit;
            Price = price;
            Describe = describe;
            IdTelegramShop = idTelegramShop;
        }

        public Order(long idOrder, string dateOrder, short statusOrder, string typeMovement, string product, double weight, string addresWarehouse, string deliveryTime, string phoneSenders, string phoneClient, string addresClient, string? fenceTime, double deposit, double price, string? describe, long? idTelegramDeliver, long idTelegramShop) : this(idOrder)
        {
            DateOrder = dateOrder;
            StatusOrder = statusOrder;
            TypeMovement = typeMovement;
            Product = product;
            Weight = weight;
            AddresWarehouse = addresWarehouse;
            DeliveryTime = deliveryTime;
            PhoneSenders = phoneSenders;
            PhoneClient = phoneClient;
            AddresClient = addresClient;
            FenceTime = fenceTime;
            Deposit = deposit;
            Price = price;
            Describe = describe;
            IdTelegramDeliver = idTelegramDeliver;
            IdTelegramShop = idTelegramShop;
        }

        public Order(long idOrder, string dateOrder, short statusOrder, string typeMovement, string product, double weight, string addresWarehouse, string deliveryTime, string phoneSenders, string phoneClient, string addresClient, string? fenceTime, double deposit, double price, string? describe, long? idTelegramDeliver, long idTelegramShop, Account idTelegramShopNavigation) : this(idOrder, dateOrder, statusOrder, typeMovement, product, weight, addresWarehouse, deliveryTime, phoneSenders, phoneClient, addresClient, fenceTime, deposit, price, describe, idTelegramDeliver, idTelegramShop)
        {
            IdTelegramShopNavigation = idTelegramShopNavigation;
        }

        public Order(long idOrder, string dateOrder, short statusOrder, string typeMovement, string product, double weight, string addresWarehouse, string deliveryTime, string phoneSenders, string phoneClient, string addresClient, string? fenceTime, double deposit, double price, string? describe, long? idTelegramDeliver, long idTelegramShop, Account? idTelegramDeliverNavigation, Account idTelegramShopNavigation = null) : this(idOrder, dateOrder, statusOrder, typeMovement, product, weight, addresWarehouse, deliveryTime, phoneSenders, phoneClient, addresClient, fenceTime, deposit, price, describe, idTelegramDeliver, idTelegramShop, idTelegramDeliverNavigation)
        {
            if (IdTelegramShopNavigation != null) 
            {
                IdTelegramShopNavigation = idTelegramShopNavigation;
            }
            IdTelegramDeliverNavigation = idTelegramDeliverNavigation;
        }

        public virtual Account? IdTelegramDeliverNavigation { get; set; }
        public virtual Account IdTelegramShopNavigation { get; set; } = null!;
    }
}
