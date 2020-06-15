using hNext.IRepository;
using hNext.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hNext.WebApiRepository
{
    public class PatientsRepository : Repository<Patient>, IPatientsRepository
    {
        public PatientsRepository(HttpClient client) : base(client)
        {
        }

        public async Task<IEnumerable<PatientDiagnosys>> GetDiagnoses(long id)
        {
            return await ReadResponse<IEnumerable<PatientDiagnosys>>(await _httpClient.GetAsync($"patients/{id}/diagnoses"));
        }

        public async Task<IEnumerable<Patient>> SearchPatients(PatientSearchModel model)
        {
            return await ReadResponse<IEnumerable<Patient>>(await _httpClient.PostAsJsonAsync("patients/search", model));
        }
    }
}
