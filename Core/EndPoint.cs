namespace Official.Core
{
    public class EndPoint
    {
        public string Url(int page) => $"http://map.gob.do/DirectorioFuncionarios/consulta?page={page}&searchEstado=Activo";
    }
}