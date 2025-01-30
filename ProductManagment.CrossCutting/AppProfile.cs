using AutoMapper;
using ProductManagment.Application.DTOs;
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
            CreateMap<ProductDTO, ProductDTO>();

            CreateMap<Client, ClientDTO>();
            CreateMap<ClientDTO, ClientDTO>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, OrderDTO>();

            CreateMap<Stock, StockDTO>();
            CreateMap<StockDTO, StockDTO>();

        }
    }
}
