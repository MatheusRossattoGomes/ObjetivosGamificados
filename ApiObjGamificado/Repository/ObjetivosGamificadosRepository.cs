
using Domain;
using ContractContext;
using Contexts;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ObjetivosGamificadosRepository : IObjetivosGamificadosRepository
{
    private readonly ObjetivosGamificadosContext _context;
    public ObjetivosGamificadosRepository(ObjetivosGamificadosContext context)
    {
        _context = context;
    }
    public IList<TiposObjetivosEnum> GetTiposDeObjetivo()
    {
        var dados = Enum.GetValues(typeof(TiposObjetivosEnum)).Cast<TiposObjetivosEnum>().ToList();
        return dados;
    }
    public ObjetivoDto GetObjetivoDto(long id)
    {
        using (_context)
        {
            var objetivo = (from o in _context.Objetivos.Where(x => x.Id == id)
                            select new ObjetivoDto()
                            {
                                Id = o.Id,
                                DataEntrega = o.DataEntrega,
                                DataCriacao = o.DataCriacao,
                                Descricao = o.Descricao,
                                IdUsuario = o.IdUsuario,
                                Objetivo = o.Objetivo,
                                Quantidade = o.Quantidade,
                                TipoObjetivo = o.TipoObjetivo
                            }).FirstOrDefault();

            return objetivo;
        }
    }
    public string GetViewObjetivos(long id)
    {
        using (_context)
        {
            var objetivo = (from o in _context.Objetivos.Where(x => x.Id == id)
                            select o.Objetivo
                            ).FirstOrDefault();

            return objetivo;
        }
    }
    public List<ObjetivoDto> GetObjetivosDto(long idUsuario)
    {
        using (_context)
        {
            var objetivos = (from o in _context.Objetivos.Where(x => x.IdUsuario == idUsuario)
                             select new ObjetivoDto()
                             {
                                 Id = o.Id,
                                 DataEntrega = o.DataEntrega,
                                 DataCriacao = o.DataCriacao,
                                 Descricao = o.Descricao,
                                 IdUsuario = o.IdUsuario,
                                 Objetivo = o.Objetivo,
                                 Quantidade = o.Quantidade,
                                 TipoObjetivo = o.TipoObjetivo
                             }).ToList();

            return objetivos;
        }
    }
    public Objetivos GetObjetivo(long id)
    {
        var objetivo = (from o in _context.Objetivos.Where(x => x.Id == id)
                        select o).FirstOrDefault();

        return objetivo;
    }

    public long AddObjetivo(Objetivos Objetivo)
    {
        _context.Add(Objetivo);
        _context.SaveChanges();
        return Objetivo.Id;
    }

    public long AlterarObjetivo(Objetivos entity)
    {
        var objetivo = (from o in _context.Objetivos.Where(x => x.Id == 1)
                        select o).FirstOrDefault();
        _context.Update(entity);
        _context.SaveChanges();

        return entity.Id;
    }

    public void DeleteObjetivo(long id)
    {
        using (_context)
        {
            var Objetivo = (from o in _context.Objetivos.Where(x => x.Id == id)
                            select o).FirstOrDefault();
            _context.Remove(Objetivo);
            _context.SaveChanges();
        }
    }
}