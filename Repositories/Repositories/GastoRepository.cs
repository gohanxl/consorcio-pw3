using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace Repositories
{
    public class GastoRepository : BaseRepository<Gasto>
    {
        public GastoRepository(ConsortiumContext context) : base(context)
        { 
        }

        public decimal GetGastosTotalCurrentMonthByConsorcio(int consorcioId)
        {
            decimal total;
            int currentYear = int.Parse(DateTime.Now.Year.ToString());
            int currentMonth = int.Parse(DateTime.Now.Month.ToString());

            total = ctx.Gasto
                .Where(g => g.IdConsorcio == consorcioId
                && g.AnioExpensa == currentYear
                && g.MesExpensa == currentMonth)
                .GroupBy(g => g.IdConsorcio == consorcioId)
                .Select(g => g.Sum(t => t.Monto))
                .FirstOrDefault();

            return total;
        }

        public List<ExpensaDTO> GetExpensasById(int consorcioId)
        {
            return ctx.Gasto
                .Where(g => g.IdConsorcio == consorcioId)
                .GroupBy(g => new { g.IdConsorcio, g.AnioExpensa, g.MesExpensa })
                .Select(g => new ExpensaDTO()
                {
                    AnioExpensa = g.Key.AnioExpensa,
                    MesExpensa = g.Key.MesExpensa,
                    GastoTotal = g.Sum(t => t.Monto),
                    ExpensasPorUnidad = g.Sum(t => t.Monto) / g.Count()
                })
                .OrderByDescending(g => new { g.AnioExpensa, g.MesExpensa })
                .ToList();
        }

        public List<Gasto> GetAllByConsorcioId(int consorcioId)
        {
            List<Gasto> gastos = ctx.Gasto
                .Where(u => u.IdConsorcio == consorcioId)
                .OrderBy(u => u.Nombre)
                .ToList();
            return gastos;
        }

        public string GetComprobanteAbsolutePath(string relativePath) 
        {
            var _PathAplicacion = HttpContext.Current.Request.PhysicalApplicationPath;
            relativePath = "Assets" + relativePath;
            
            return Path.Combine(_PathAplicacion, relativePath);
        }

        public string GetComprobanteFileName(string relativePath)
        {
            String[] splitedPath = relativePath.Split('/');

            return splitedPath.Last();
        }

        public bool ValidateCreatorWithCurrentUser(string currentUserEmail, string creatorUserEmail)
        {
            return currentUserEmail.Equals(creatorUserEmail);
        }
    }
}
