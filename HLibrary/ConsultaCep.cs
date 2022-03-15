using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLibrary
{
    public class ConsultaCep
    {
        private string _cep { get; set; }
        private string _logradouro { get; set; }
        private string _complemento { get; set; }
        private string _bairro { get; set; }
        private string _localidade { get; set; }
        private string _uf { get; set; }
        private bool _erro { get; set; } = true;
        public string Cep { get { return _cep; } }
        public string Logradouro { get { return _logradouro; } }
        public string Complemento { get { return _complemento; } }
        public string Bairro { get { return _bairro; } }
        public string Cidade { get { return _localidade; } }
        public string Uf { get { return _uf; } }
        public bool Erro { get { return _erro; } }



        public ConsultaCep(string Cep)
        {
            _cep = Cep;
            Consulta();
        }
        private void Consulta()
        {
            RestClient client = new RestClient("https://viacep.com.br");

            var request = new RestRequest("/ws/" + _cep + "/json/");

            var response = client.Execute(request);

            var responseLimpo = response.Content.ToString().Replace("\n", "");

            if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                dynamic dados = JsonConvert.DeserializeObject<dynamic>(responseLimpo);

                if (dados.erro == null)
                {
                    _cep = dados.cep;
                    _logradouro = dados.logradouro;
                    _complemento = dados.complemento;
                    _bairro = dados.bairro;
                    _localidade = dados.localidade;
                    _uf = dados.uf;
                    _erro = false;
                }
                
            }
                
        }

    }
    public class DadosCep
    {
        public string cep { get; set; }
        public string logradouro { get; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public bool erro { get; set; } = true;
    }
}
