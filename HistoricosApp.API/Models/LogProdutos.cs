namespace HistoricosApp.API.Models
{
    public class LogProdutos
    {
        public string? Id { get; set; }
        public DateTime? DataHora { get; set; }
        public int? Tipo { get; set; }
        public Produto? Produto { get; set; }
    }

    public class Produto
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Total { get; set; }
        public Categoria? Categoria { get; set; }
    }

    public class Categoria
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
    }

}
