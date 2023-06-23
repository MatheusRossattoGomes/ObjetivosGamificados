
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
            var objetivos = (from o in _context.Objetivos.Where(x => x.IdUsuario == idUsuario && x.IdAula == null)
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
        _context.Update(entity);
        _context.SaveChanges();

        return entity.Id;
    }

    public void AlterarObjetivosAula(List<Objetivos> entities)
    {
        entities.ForEach(x => _context.Update(x));
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

    public long AddAula(Aula Aula)
    {
        _context.Add(Aula);
        _context.SaveChanges();
        return Aula.Id;
    }

    public List<AulaGridDto> GetAulasDto(long idUsuario)
    {
        using (_context)
        {
            var aulas = (from a in _context.Aulas.Where(x => x.IdUsuario == idUsuario)
                         select new AulaGridDto()
                         {
                             Id = a.Id,
                             DataAula = a.DataAula,
                             Descricao = a.Descricao,
                             Resumo = a.Resumo
                         }).ToList();

            return aulas;
        }
    }

    public List<ObjetivoDto> GetObjetivosAula(long idAula)
    {
        using (_context)
        {
            var objetivos = (from o in _context.Objetivos.Where(x => x.IdAula == idAula)
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

    public Aula GetAula(long idAula)
    {
        var aula = (from a in _context.Aulas.Where(x => x.Id == idAula)
                    select a).FirstOrDefault();

        aula.Objetivos = (from o in _context.Objetivos.Where(x => x.IdAula == aula.Id)
                          select o).ToList();

        return aula;
    }

    public AulaDto GetAulaDto(long idAula)
    {
        using (_context)
        {
            var aula = (from a in _context.Aulas.Where(x => x.Id == idAula)
                        select new AulaDto()
                        {
                            DataAula = a.DataAula,
                            Descricao = a.Descricao,
                            Id = a.Id,
                            IdUsuario = a.IdUsuario,
                            Resumo = a.Resumo
                        }).FirstOrDefault();

            aula.Objetivos = (from o in _context.Objetivos.Where(x => x.IdAula == aula.Id)
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

            return aula;
        }
    }

    public long AlterarAula(Aula entity)
    {
        _context.Update(entity);
        _context.SaveChanges();

        return entity.Id;
    }

    public void DeleteAula(long id)
    {
        using (_context)
        {
            var aula = (from a in _context.Aulas.Where(x => x.Id == id)
                        select a).FirstOrDefault();

            var objetivos = (from o in _context.Objetivos.Where(x => x.IdAula == id)
                             select o).ToList();
            objetivos.ForEach(x => _context.Remove(x));
            _context.Remove(aula);
            _context.SaveChanges();
        }
    }
}