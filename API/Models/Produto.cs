namespace API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Desc { get; set; }
        public bool Ativo { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoPromo { get; set; }
    }
}