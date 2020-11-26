using Newtonsoft.Json;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExpensaService
    {
        public ExpensaService()
        {
        }

        public async Task<List<ExpensaDTO>> GetExpensasByConsorcioIdAsync(int consorcioId)
        {
            string url = "https://localhost:44342/";
            List<ExpensaDTO> expensas = new List<ExpensaDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/ExpensasApi/" + consorcioId.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var expensasResponse = await response.Content.ReadAsStringAsync();
                    expensas = JsonConvert.DeserializeObject<List<ExpensaDTO>>(expensasResponse);
                }
                return expensas;
            }
        }
    }
}
