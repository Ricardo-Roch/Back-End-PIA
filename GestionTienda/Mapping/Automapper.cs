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
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<GetUsuarioDTO, Usuario>();
           

        }

        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<Automapper>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }


}

/*namespace WebApiAlumnosSeg.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AlumnoDTO, Alumno>();
            CreateMap<Alumno,GetAlumnoDTO>();
            CreateMap<Alumno, AlumnoDTOConClases>()
                .ForMember(alumnoDTO => alumnoDTO.Clases, opciones => opciones.MapFrom(MapAlumnoDTOClases));
            CreateMap<ClaseCreacionDTO, Clase>()
                .ForMember(clase => clase.AlumnoClase, opciones => opciones.MapFrom(MapAlumnoClase));
            CreateMap<Clase, ClaseDTO>();
            CreateMap<Clase, ClaseDTOConAlumnos>()
                .ForMember(claseDTO => claseDTO.Alumnos, opciones => opciones.MapFrom(MapClaseDTOAlumnos));
            CreateMap<ClasePatchDTO, Clase>().ReverseMap();
            CreateMap<CursoCreacionDTO, Cursos>();
            CreateMap<Cursos,CursoDTO>();
        }

 */