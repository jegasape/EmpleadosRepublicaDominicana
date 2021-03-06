using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Official.Core
{
    public class Data
    {
        public async Task<string> GetData()
        {
            var request = string.Empty;
            var endPoint = new EndPoint();
            var client = new HttpClient();
            var document = new HtmlDocument();
            for(var i = 1; i < 1130; i++){
            request += await client.GetStringAsync(endPoint.Url(i));
            }
            document.LoadHtml(HttpUtility.HtmlDecode(request));
            var nodes = document.DocumentNode.SelectNodes("//tbody/tr").ToList()
                        .Select(p => p.InnerText.Replace("\r\n", string.Empty).Trim()).ToList();
            return JsonConvert.SerializeObject(Final(nodes));
        }
        private List<InformationOfficial> Final(List<string> nodes)
        {
            var alfa = 0;
            var scope = string.Empty;
            var list = new List<InformationOfficial>();
            foreach (var item in nodes)
            {
                //á, é, í, ó, ú
                var value = LetterCorrection(item);
                var sequence = value.ToArray();
                for (var i = 0; i < sequence.Length; i++)
                {
                    if (sequence[i] == ':' || char.IsLetterOrDigit(sequence[i])){
                        alfa = 0;
                        scope += sequence[i];
                    }
                    else if (sequence[i] == ' '){
                        alfa++;
                        if (alfa <= 1)
                            scope += ' ';
                    }
                }
                var hero = HeroCorrection(scope).Split("\r\n");
                list.Add(item: new InformationOfficial
                {
                    Institution = Filter(hero[(int)Utils.Options.Institution]),
                    Official = Filter(hero[(int)Utils.Options.Official]),
                    Position = Filter(hero[(int)Utils.Options.Position]),
                    Area = Filter(hero[(int)Utils.Options.Area]),
                    Decree = Filter(hero[(int)Utils.Options.Decree]),
                    AdmissionDate = Filter(hero[(int)Utils.Options.AdmissionDate]),
                    Status = Filter(hero[(int)Utils.Options.Status])
                });
                scope = string.Empty;
            }
            return list;
        }
        private string Filter(string sender) => sender.Split(":")[1].TrimStart().TrimEnd();
        private string LetterCorrection(string sender) =>
            sender.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        private string HeroCorrection(string sender) =>
            sender.Replace("Institucion:", "\r\nInstitucion:").Replace("Funcionario:", "\r\nFuncionario:")
                  .Replace("Cargo:", "\r\nCargo:").Replace("Area:", "\r\nArea:").Replace("Decreto:", "\r\nDecreto:")
                  .Replace("Fecha ingreso:", "\r\nFecha ingreso:").Replace("Estado:", "\r\nEstado:");

    }
}