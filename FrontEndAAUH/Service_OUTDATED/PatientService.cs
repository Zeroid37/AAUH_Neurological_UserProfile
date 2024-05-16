using FrontEndAAUH.Model;
using FrontEndAAUH.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontEndAAUH.Service {
    public class PatientService : IPatientService {

        readonly IServiceConnection _connection;
        readonly string? _baseUrl = System.Configuration.ConfigurationManager.AppSettings.Get("baseUrl");
        
        public PatientService() {
            _connection = new ServiceConnection(_baseUrl);
        }

        public async Task<Patient> getPatientByPatientNo(string patientNo) {
            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += $"api/patient/{patientNo}";

            Patient? patient = new Patient();

            if (_connection != null) {
                try {
                    var response = await _connection.serviceGet();
                    if (response != null && response.IsSuccessStatusCode) {
                        var content = await response.Content.ReadAsStringAsync();
                        patient = JsonConvert.DeserializeObject<Patient>(content);
                    }
                } catch (Exception e) {

                    throw;
                }
            }
            return patient;
        }

        public async Task<bool> postPatient(Patient patient) {
            bool ok = false;

            _connection.useUrl = _connection.baseUrl;
            _connection.useUrl += $"api/Patient";
                            

            if (_connection != null) {
                try {
                    var s = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    var json = JsonConvert.SerializeObject(patient);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _connection.servicePost(content);

                    if (response != null && response.IsSuccessStatusCode) {
                        ok = true;
                    }
                } catch (Exception) {
                    throw;
                }
            }
            return ok;
        }
    }
}
