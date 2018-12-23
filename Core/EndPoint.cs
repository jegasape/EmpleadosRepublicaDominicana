namespace Official.Core
{
    public class EndPoint
    {
        public string Url(int page) => $"http://map.gob.do/DirectorioFuncionarios/Home/Busqueda?page={page}&searchEstado=Activo";
    }
}