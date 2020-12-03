using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GastoService : BaseService<GastoRepository, Gasto>
    {
        public GastoService(ConsortiumContext context) : base(context)
        {
        }

        public decimal GetGastosTotalCurrentMonthByConsorcio(int consorcioId)
        {
            return repo.GetGastosTotalCurrentMonthByConsorcio(consorcioId);
        }

        public List<ExpensaDTO> GetExpensasById(int consorcioId)
        {
            return repo.GetExpensasById(consorcioId);
        }

        public List<Gasto> GetAllByConsorcioId(int consorcioId)
        {
            return repo.GetAllByConsorcioId(consorcioId);
        }

        public string GetComprobanteAbsolutePath(string relativePath) 
        {
            return repo.GetComprobanteAbsolutePath(relativePath);
        }

        public string GetComprobanteFileName(string relativePath)
        {
            return repo.GetComprobanteFileName(relativePath);
        }

        public bool ValidateCreatorWithCurrentUser(string currentUserEmail, string creatorUserEmail)
        {
            return repo.ValidateCreatorWithCurrentUser(currentUserEmail, creatorUserEmail);
        }
    }
}
