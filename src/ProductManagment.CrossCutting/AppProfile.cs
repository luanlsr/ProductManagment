using AutoMapper;
using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.CrossCutting
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, Client>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();

            CreateMap<Stock, StockDTO>();
            CreateMap<StockDTO, Stock>();

        }
    }
}
