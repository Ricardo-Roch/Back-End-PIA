using System;
using AutoMapper;
using GestionTienda.DTOs;
using GestionTienda.Entidades;

namespace GestionTienda.Mapping
{

	public class Automapper: Profile
    {
		public Automapper()
		{
            CreateMap<Usuario, UsuarioDTO>();
                //.ForMember(dest => dest.id_usuario, opt => opt.MapFrom(src => src.id_usuario));
            CreateMap<GetUsuarioDTO, Usuario>();

            CreateMap<Compra, compraDTO>().ReverseMap();
            CreateMap<GetCompraDTO, Compra>();

            CreateMap<Productos, productoDTO>().ReverseMap();
            CreateMap<GetProducto, Productos>();

            CreateMap<Carrito, carritoDTO>().ReverseMap();
            CreateMap<GetCarritoDTO, Carrito>();

        }

        public static void Configure()
        {
            AutoMapper.Mapper.Reset();
            Mapper.Initialize(x =>
            {
                x.AddProfile<Automapper>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }


}

