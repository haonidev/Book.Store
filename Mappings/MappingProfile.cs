using AutoMapper;
using Book.Store.Data.Entities;

using DomainAssunto = Book.Store.Business.Domain.Assunto;
using EntityAssunto = Book.Store.Data.Entities.Assunto;

using DomainCanal = Book.Store.Business.Domain.Canal;
using EntityCanal = Book.Store.Data.Entities.Canal;

using DomainAutor = Book.Store.Business.Domain.Autor;
using EntityAutor = Book.Store.Data.Entities.Autor;

using DomainLivro = Book.Store.Business.Domain.Livro;
using EntityLivro = Book.Store.Data.Entities.Livro;

using DomainVenda = Book.Store.Business.Domain.Venda;
using EntityVenda = Book.Store.Data.Entities.Venda;

namespace Book.Store.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DomainAssunto, EntityAssunto>()
                .ForMember(dest => dest.AssuntoCod, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AssuntoCod))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EntityCanal, DomainCanal>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CanalCod))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ReverseMap()
                .ForMember(dest => dest.CanalCod, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<EntityAutor, DomainAutor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CodAu))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ReverseMap()
                .ForMember(dest => dest.CodAu, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<EntityLivro, DomainLivro>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LivroCod))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Editora, opt => opt.MapFrom(src => src.Editora))
                .ForMember(dest => dest.Edicao, opt => opt.MapFrom(src => src.Edicao))
                .ForMember(dest => dest.AnoPublicacao, opt => opt.MapFrom(src => src.AnoPublicacao))
                .ForMember(dest => dest.PrecoSugerido, opt => opt.MapFrom(src => src.PrecoSugerido))
                .ForMember(dest => dest.Autores, opt => opt.MapFrom(src => src.LivroAutores.Select(la => la.Autor)))
                .ForMember(dest => dest.Assuntos, opt => opt.MapFrom(src => src.LivroAssuntos.Select(la => la.Assunto)))
                .ReverseMap()
                .ForMember(dest => dest.LivroCod, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Editora, opt => opt.MapFrom(src => src.Editora))
                .ForMember(dest => dest.Edicao, opt => opt.MapFrom(src => src.Edicao))
                .ForMember(dest => dest.AnoPublicacao, opt => opt.MapFrom(src => src.AnoPublicacao))
                .ForMember(dest => dest.PrecoSugerido, opt => opt.MapFrom(src => src.PrecoSugerido))
                .ForMember(dest => dest.LivroAutores, opt => opt.MapFrom(src => src.Autores.Select(a => new LivroAutor { LivroCod = src.Id, AutorCod = a.Id })))
                .ForMember(dest => dest.LivroAssuntos, opt => opt.MapFrom(src => src.Assuntos.Select(a => new LivroAssunto { LivroCod = src.Id, AssuntoCod = a.Id })));

            CreateMap<EntityVenda, DomainVenda>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Codv))
                .ForMember(dest => dest.Canal, opt => opt.MapFrom(src => src.Canal))
                .ForMember(dest => dest.Livros, opt => opt.MapFrom(src => src.VendaLivros.Select(vl => vl.Livro)))
                .ReverseMap()
                .ForMember(dest => dest.Codv, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CanalCod, opt => opt.MapFrom(src => src.Canal != null ? src.Canal.Id : (int?)null))
                .ForMember(dest => dest.Canal, opt => opt.Ignore())
                .ForMember(dest => dest.VendaLivros, opt => opt.MapFrom(src => src.Livros.Select(l => new VendaLivro { Codv = src.Id, LivroCod = l.Id })));
        }
    }
}