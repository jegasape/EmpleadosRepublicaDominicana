namespace Official.Core
{
    public class EndPoint
    {
        public string Url(int page) => $"http://map.gob.do:8282/DirectorioFuncionarios/Home/Busqueda?page={page}&searchEstado=Activo";
    }
}